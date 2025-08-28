using ScalpLedger.Application.Common.Models.Common;

namespace ScalpLedger.Application.Candles.Fetch;

public record FetchCandleRequest(
    string Symbol,
    string Interval,
    DateTime StartDate,
    DateTime EndDate,
    int? Limit
) : IBaseCommand;