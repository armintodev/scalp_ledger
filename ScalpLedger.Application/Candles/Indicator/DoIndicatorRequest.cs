using ScalpLedger.Application.Common.Models.Common;

namespace ScalpLedger.Application.Candles.Indicator;

public record DoIndicatorRequest() : IBaseQuery<DoIndicatorResponse>;

public record DoIndicatorResponse();