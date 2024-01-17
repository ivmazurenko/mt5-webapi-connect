using System.Text.Json.Serialization;

namespace MetaquotesApi;

public class AuthStep1Dto
{
    [JsonPropertyName("retcode")] public string RetCode { get; set; }
    [JsonPropertyName("version_access")] public string VersionAccess { get; set; }
    [JsonPropertyName("srv_rand")] public string RrvRand { get; set; }
}