using System.Collections.Frozen;

namespace ScalpLedger.Application.Exchanges.Adapters.ErrorCodes;

public class ExchangeErrorCodeFactory
{
    private readonly FrozenDictionary<string, IErrorCodeService> _errorCodeServices;

    public ExchangeErrorCodeFactory(IEnumerable<IErrorCodeService> errorCodeServices)
    {
        _errorCodeServices = errorCodeServices.ToFrozenDictionary(
            s => s.GetType().Name.Replace("ErrorCodeService", string.Empty),
            StringComparer.OrdinalIgnoreCase
        );
    }

    public IErrorCodeService GetErrorCodeService(string exchange)
    {
        return _errorCodeServices.TryGetValue(exchange, out var errorCodeService)
            ? errorCodeService
            : throw new ArgumentOutOfRangeException(nameof(exchange), exchange, "Exchange is not supported");
    }
}