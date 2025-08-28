namespace ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser
{
    public class ProxyConfiguration
    {
        public string Host { get; private set; }
        public int Port { get; private set; }

        public string? Username { get; private set; }
        public string? Password { get; private set; }

        public bool UseCredentials { get; }

        public ProxyConfiguration(string host, int port, string? username = null, string? password = null)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;

            // TODO: needs some checks
            UseCredentials = !(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password));
        }
    }
}