using System.Text.Json.Serialization;

namespace MetaquotesApi;

public record AuthStep1Dto
{
    [JsonPropertyName("retcode")] public string? RetCode { get; set; }
    [JsonPropertyName("version_access")] public string? VersionAccess { get; set; }
    [JsonPropertyName("srv_rand")] public string RrvRand { get; set; } = null!;
}

public record AuthStep2Dto
{
    [JsonPropertyName("retcode")] public string? RetCode { get; set; }
    [JsonPropertyName("cli_rand_answer")] public string? CliRandAnswer { get; set; }
}

public record RootResultDto<T>
{
    [JsonPropertyName("retcode")] public string RetCode { get; set; } = null!;
    [JsonPropertyName("answer")] public T? Answer { get; set; }
}

public record TotalDto
{
    [JsonPropertyName("total")] public string? Total { get; set; }
}

public record GroupDto
{
    [JsonPropertyName("Group")] public string Name { get; set; }
    [JsonPropertyName("Currency")] public string Currency { get; set; }
    [JsonPropertyName("PermissionsFlags")] public string PermissionsFlags { get; set; }
}

public readonly record struct GroupDtp(string Name, string Currency, string Enabled);

public enum EnPermissionsFlags
{
    PERMISSION_NONE = 0, // default
    PERMISSION_CERT_CONFIRM = 1, // certificate confirmation neccessary
    PERMISSION_ENABLE_CONNECTION = 2, // clients connections allowed
    PERMISSION_RESET_PASSWORD = 4, // reset password after first logon
    PERMISSION_FORCED_OTP_USAGE = 8, // forced usage OTP
    PERMISSION_RISK_WARNING = 16, // show risk warning window on start
    PERMISSION_REGULATION_PROTECT = 32, // country-specific regulatory protection

    //--- enumeration borders
    PERMISSION_ALL = PERMISSION_CERT_CONFIRM | PERMISSION_ENABLE_CONNECTION | PERMISSION_RESET_PASSWORD |
                     PERMISSION_FORCED_OTP_USAGE | PERMISSION_RISK_WARNING | PERMISSION_REGULATION_PROTECT
};