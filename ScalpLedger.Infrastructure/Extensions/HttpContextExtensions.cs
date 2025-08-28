using System.Net.Http.Headers;

namespace ScalpLedger.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static void AddRange(this HttpRequestHeaders request, IEnumerable<KeyValuePair<string, object>> values)
    {
        foreach (var value in values)
        {
            request.Add(value.Key, value.Value.ToString());
        }
    }
}