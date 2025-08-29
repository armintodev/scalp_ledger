using ScalpLedger.Application.Common.Models;
using ScalpLedger.Application.Common.Models.Common;
using ScalpLedger.Domain.Repositories;
using Skender.Stock.Indicators;

namespace ScalpLedger.Application.Candles.Indicator;

public class DoIndicatorHandler(
    ICandleRepository candleRepository
) : IBaseQueryHandler<DoIndicatorRequest, DoIndicatorResponse>
{
    public Task<BaseResult<DoIndicatorResponse>> Handle(DoIndicatorRequest request, CT ct)
    {
        var candles = candleRepository.Find().Select(
            k => new Quote(
                k.Timestamp,
                k.Open,
                k.High,
                k.Low,
                k.Close,
                k.Volume
            )
        ).ToArray();

        // var rsi = candles.ToRsi();
        // var macd = candles.ToMacd();
        var adx = candles.ToAdx();

        return Task.FromResult(new BaseResult<DoIndicatorResponse>(null!));
    }
}