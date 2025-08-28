using System.Security.Cryptography;
using System.Text;

namespace ScalpLedger.Infrastructure.Extensions;

public static class SecurityHelpers
{
    public static string Sha256ToHex(this string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        byte[] hashBytes = SHA256.HashData(bytes);

        return Convert.ToHexStringLower(hashBytes);
    }
}