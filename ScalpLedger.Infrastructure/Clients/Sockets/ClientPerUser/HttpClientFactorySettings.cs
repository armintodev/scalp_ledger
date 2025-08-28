using System.Diagnostics.CodeAnalysis;

namespace ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser
{
    public class HttpClientFactorySettings
    {
        private const int TimeoutInMinutes = 2;

        public string ExchangeUrl { get; set; }
        public int Timeout { get; set; } = TimeoutInMinutes;

        public ProxyConfiguration? Proxy { get; set; }
        public int MaxConnectionsPerServer { get; set; }
        public int PooledConnectionLifetimeInSeconds { get; set; }
        public int PooledConnectionIdleTimeoutInMinutes { get; set; }

        [MemberNotNullWhen(true, nameof(Proxy))]
        public bool UseProxy { get; }

        public HttpClientFactorySettings(
            string exchangeUrl,
            ProxyConfiguration? proxy,
            int? timeout,
            int maxConnectionsPerServer,
            int pooledConnectionLifetimeInSeconds,
            int pooledConnectionIdleTimeoutInMinutes)
        {
            ExchangeUrl = exchangeUrl;
            Proxy = proxy;
            UseProxy = Proxy != null;

            if (timeout.HasValue && Timeout != timeout)
                Timeout = timeout.Value;

            MaxConnectionsPerServer = maxConnectionsPerServer;
            PooledConnectionLifetimeInSeconds = pooledConnectionLifetimeInSeconds;
            PooledConnectionIdleTimeoutInMinutes = pooledConnectionIdleTimeoutInMinutes;
        }

#pragma warning disable CS8618, CS9264
        public HttpClientFactorySettings()
#pragma warning restore CS8618, CS9264
        {
        }
    }
}