namespace ScalpLedger.Application.Exchanges.Adapters.Contracts.Responses.Account;

public record GetAccountResponse(string Coin, string Balance) : BaseData;