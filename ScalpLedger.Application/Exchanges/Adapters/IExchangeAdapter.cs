using ScalpLedger.Application.Exchanges.Adapters.Contracts.Responses.Account;
using ScalpLedger.Application.Exchanges.Adapters.Models;

namespace ScalpLedger.Application.Exchanges.Adapters;

public interface IExchangeAdapter
{
    void SignRequest(HttpRequestMessage request, SignRequestModel signModel);

    Task<BaseResponse<GetMarketKLineResponse[]>> GetMarketKLine(string symbol, DateTime startDate, DateTime endDate,
        string interval, int? limit, CT ct);
}