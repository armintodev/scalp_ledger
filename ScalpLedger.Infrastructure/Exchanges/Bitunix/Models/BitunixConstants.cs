namespace ScalpLedger.Infrastructure.Exchanges.Bitunix.Models;

public class BitunixConstants
{
    private const string BASE_URL = "https://openapi.bitunix.com";
    public const string BASE_URL_FUTURE = $"{BASE_URL}/api/v1/futures/";
    public const string BASE_URL_SPOT = $"{BASE_URL}/api/spot/v1/";

    public class Endpoint
    {
        public class Spot
        {
            public const string USER_ACCOUNT = "user/account";
        }

        public class Future
        {
            public const string MARKET_KLINE = "futures/market/kline/";
        }
    }
}