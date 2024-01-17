using System.Security.Cryptography;

namespace MetaquotesApi;

public static class CliRand
{
    public static string Calculate() => GetRandomHex().ToHexString();

    private static byte[] GetRandomHex()
    {
        var byteArray = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(byteArray);
        return byteArray;
    }
}