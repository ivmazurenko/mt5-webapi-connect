using System.Security.Cryptography;
using System.Text;

namespace MetaquotesApi;

public static class SrvRandAnswer
{
    public static string Calculate(string password, string srvRand)
    {
        byte[] passwordBytesUnicode = Encoding.Unicode.GetBytes(password);

        byte[] md5OfPassword = MD5.HashData(passwordBytesUnicode);

        byte[] apiWord = "WebAPI"u8.ToArray();

        byte[] md5PasswordConcatWebApi = md5OfPassword.Concat(apiWord).ToArray();

        byte[] md5md5PasswordConcatWebApi = MD5.HashData(md5PasswordConcatWebApi);

        var srvcRand = StringToByteArray(srvRand);

        var md5md5PasswordConcatWebApiConcatSrvRand = md5md5PasswordConcatWebApi.Concat(srvcRand).ToArray();

        byte[] md5md5md5PasswordConcatWebApiConcatSrvRand = MD5.HashData(md5md5PasswordConcatWebApiConcatSrvRand);

        return ToHexString(md5md5md5PasswordConcatWebApiConcatSrvRand);
    }

    private static string ToHexString(byte[] byteArray)
    {
        return BitConverter.ToString(byteArray).Replace("-", "").ToLower();
    }

    private static byte[] StringToByteArray(string hex)
    {
        int length = hex.Length / 2;
        byte[] result = new byte[length];
        for (int i = 0; i < length; i++)
            result[i] = Convert.ToByte(hex.Substring(2 * i, 2), 16);

        return result;
    }
}