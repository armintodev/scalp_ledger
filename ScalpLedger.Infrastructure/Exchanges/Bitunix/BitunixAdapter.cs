using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using ScalpLedger.Application.Exchanges.Adapters;
using ScalpLedger.Application.Exchanges.Adapters.Contracts.Responses.Account;
using ScalpLedger.Application.Exchanges.Adapters.Models;
using ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser;
using ScalpLedger.Infrastructure.Exchanges.Bitunix.Models;
using ScalpLedger.Infrastructure.Extensions;

namespace ScalpLedger.Infrastructure.Exchanges.Bitunix;

public class BitunixAdapter : IExchangeAdapter
{
    private readonly IPerUserHttpClientFactory _userHttpClientFactory;
    private readonly HttpClientFactorySettings _options;

    public BitunixAdapter(IPerUserHttpClientFactory userHttpClientFactory,
        IOptionsSnapshot<HttpClientFactorySettings> options)
    {
        _userHttpClientFactory = userHttpClientFactory;
        _options = options.Value;
    }

    public void SignRequest(HttpRequestMessage request, SignRequestModel signModel)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string nonce = Guid.CreateVersion7().ToString("N");

        var rawData = nonce + timestamp + signModel.ApiKey;
        var levelOneHash = rawData.Sha256ToHex();

        var levelSecondHash = (levelOneHash + signModel.ApiSecret).Sha256ToHex();

        request.Headers.AddRange(
            [
                new("api-key", signModel.ApiKey),
                new("nonce", nonce),
                new("timestamp", timestamp.ToString()),
                new("sign", levelSecondHash)
            ]
        );
    }

    public async Task<BaseResponse<GetMarketKLineResponse[]>> GetMarketKLine(string symbol, DateTime startDate,
        DateTime endDate, string interval, int? limit, CT ct)
    {
        long startUnixTime = new DateTimeOffset(startDate).ToUnixTimeMilliseconds();
        long endUnixTime = new DateTimeOffset(endDate).ToUnixTimeMilliseconds();

        var requestUrl = BitunixConstants.Endpoint.Future.MARKET_KLINE +
                         "?" +
                         $"symbol={symbol}" +
                         $"&startTime={startUnixTime}" +
                         $"&endTime={endUnixTime}" +
                         $"&interval={interval}" +
                         $"&limit={limit}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        // string key = "2e4388ecab660c3100f9adb5522ec231";
        // string secret = "04ba11bc11e495cf4d4db8d98d0fdbf0";
        //
        // SignRequest(request, new(key, secret));

        var client = _userHttpClientFactory.GetClientFor(1, _options);

        var response = await client.SendAsync(request, ct);

        var content = await response.Content.ReadFromJsonAsync<BitunixBaseResponse<GetMarketKLineResponse[]>>(ct);

        if (content == null)
            return new BaseResponse<GetMarketKLineResponse[]>
            {
                Success = false
            };

        // TODO: fix the additional in response's json showing!!! 
        // GetMarketKLineResponse[] data =
        // [
        //     .. content.Data.Select(
        //         c => new GetMarketKLineResponse(c.Coin, c.Balance)
        //         {
        //             Additional = new Dictionary<string, object>
        //             {
        //                 [nameof(UserAccountResponse.BalanceLocked)] = c.BalanceLocked
        //             }
        //         }
        //     )
        // ];

        return new BaseResponse<GetMarketKLineResponse[]>
        {
            Success = content.Success,
            Data = content.Data,
            Message = content.Msg,
            Code = content.Code.ToString(),
        };
    }
}