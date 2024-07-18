using System.Security.Cryptography;
using System.Text;

namespace ArbitraX.Infrastructure.Utils;

public class Utilities
{
    public static string CreateSignature(string message, string apiSecret)
    {
        var key = Encoding.UTF8.GetBytes(apiSecret);
        string stringHash;
        using (var hmac = new HMACSHA256(key))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            stringHash = BitConverter.ToString(hash).Replace("-", "");
        }

        return stringHash.ToLower();
    }

    public static string GenerateTimeStamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}

