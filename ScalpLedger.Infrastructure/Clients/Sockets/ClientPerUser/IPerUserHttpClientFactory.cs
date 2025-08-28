namespace ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser;

public interface IPerUserHttpClientFactory : IDisposable
{
    // TODO: Later, the user can get the 'userConnectionId' from current-context
    HttpClient GetClientFor(long userConnectionId,  HttpClientFactorySettings setting);
    void RemoveClient(long userConnectionId);
}