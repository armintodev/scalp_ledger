namespace ScalpLedger.Application.Exchanges.Adapters.ErrorCodes;

public interface IErrorCodeService
{
    Dictionary<string, string> GetErrors();
    string GetError(string code);
}