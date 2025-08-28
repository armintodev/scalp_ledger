namespace ScalpLedger.Application.Exchanges.Adapters.Contracts.Responses.Account;

public record GetMarketKLineResponse(
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal QuoteVol,
    decimal BaseVol,
    long Time
) : BaseData;