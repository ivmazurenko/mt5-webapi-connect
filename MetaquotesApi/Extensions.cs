namespace MetaquotesApi;

public static class Extensions
{
    public static string ToHexString(this byte[] byteArray)
    {
        return BitConverter.ToString(byteArray).Replace("-", "").ToLower();
    }
}