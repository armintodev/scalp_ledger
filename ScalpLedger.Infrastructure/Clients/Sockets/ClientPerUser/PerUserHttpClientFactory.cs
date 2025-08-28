using System.Collections.Concurrent;
using System.Net;

namespace ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser;

public sealed class PerUserHttpClientFactory : IPerUserHttpClientFactory
{
    private readonly ConcurrentDictionary<long, HttpClient> _clients = new();

    public HttpClient GetClientFor(long userConnectionId, HttpClientFactorySettings setting)
    {
        return _clients.GetOrAdd(
            userConnectionId,
            _ =>
            {
                var socketHandler = new SocketsHttpHandler
                {
                    Proxy = setting.UseProxy ? new WebProxy(setting.Proxy.Host, setting.Proxy.Port) : null,
                    UseProxy = setting.UseProxy,
                    MaxConnectionsPerServer = setting.MaxConnectionsPerServer,
                    PooledConnectionLifetime = TimeSpan.FromSeconds(setting.PooledConnectionLifetimeInSeconds),
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(setting.PooledConnectionIdleTimeoutInMinutes),
                    DefaultProxyCredentials = setting.UseProxy && setting.Proxy.UseCredentials
                        ? new NetworkCredential(
                            setting.Proxy.Username,
                            setting.Proxy.Password
                        )
                        : null,
                    KeepAlivePingDelay = TimeSpan.FromMinutes(1), // TODO: check this
                    KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always, // TODO: check this
                    KeepAlivePingTimeout = TimeSpan.FromMinutes(0.5) // TODO: check this
                };

                return new HttpClient(socketHandler, disposeHandler: true)
                {
                    BaseAddress = new Uri(setting.ExchangeUrl),
                    Timeout = TimeSpan.FromMinutes(setting.Timeout)
                };
            }
        );
    }

    public void RemoveClient(long userConnectionId)
    {
        if (_clients.TryRemove(userConnectionId, out var client))
        {
            client.Dispose();
        }
    }

    public void Dispose()
    {
        foreach (var kv in _clients)
            kv.Value.Dispose();
    }
}