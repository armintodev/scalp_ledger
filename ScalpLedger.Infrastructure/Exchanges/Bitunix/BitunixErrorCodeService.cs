using ScalpLedger.Application.Exchanges.Adapters.ErrorCodes;

namespace ScalpLedger.Infrastructure.Exchanges.Bitunix;

public class BitunixErrorCodeService : IErrorCodeService
{
    public Dictionary<string, string> GetErrors()
    {
        return new()
        {
            { "0", "Success" },
            { "2", "Parameter error" },
            { "500", "System error" },
            { "100004", "app-key not found" },
            { "10016", "User does not exist" },
            { "70003", "Missing Parameters" },
            { "100008", "Request expired" },
            { "100005", "Parameter signature error" },
            { "100011", "Trigger IP risk control rules" }
        };
    }

    public string GetError(string code)
    {
        return GetErrors().GetValueOrDefault(code, "Unknown error");
    }
}