using ScalpLedger.Application.Common.Databases;
using ScalpLedger.Application.Common.Models;
using ScalpLedger.Application.Common.Models.Common;
using ScalpLedger.Application.Exchanges.Adapters;
using ScalpLedger.Domain.CandleSticks;
using ScalpLedger.Domain.Repositories;

namespace ScalpLedger.Application.Candles.Fetch;

public class FetchCandleHandler(
    IExchangeAdapter exchange,
    ICandleRepository candleRepository,
    IUnitOfWork unitOfWork
)
    : IBaseHandler<FetchCandleRequest>
{
    public async Task<BaseResult<long>> Handle(FetchCandleRequest request, CT ct)
    {
        var trimSymbol = request.Symbol.ToUpper().Trim();

        var klines = await exchange.GetMarketKLine(
            trimSymbol,
            request.StartDate,
            request.EndDate,
            request.Interval,
            request.Limit,
            ct
        );

        List<Candle> candles = new(klines.Data.Length);

        foreach (var k in klines.Data)
        {
            var date = DateTimeOffset.FromUnixTimeMilliseconds(k.Time).UtcDateTime;

            candles.Add(
                new(
                    trimSymbol,
                    request.Interval,
                    date,
                    k.Open,
                    k.Close,
                    k.Low,
                    k.High,
                    k.QuoteVol
                )
            );
        }

        await candleRepository.AddRangeAsync(candles, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new BaseResult<long>(1);
    }
}