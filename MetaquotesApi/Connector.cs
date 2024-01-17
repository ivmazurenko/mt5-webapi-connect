using System.Net.Security;

namespace MetaquotesApi;

public class Connector
{
    private readonly string _host;
    private readonly HttpClient _httpClient;

    public Connector(string server)
    {
        _host = "https://" + server;
        var socketHttpHandler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            SslOptions = new SslClientAuthenticationOptions
                { RemoteCertificateValidationCallback = (_, _, _, _) => true }
        };

        _httpClient = new HttpClient(socketHttpHandler);
    }

    public async ValueTask<bool> SendAuth(string login, string password, string agent)
    {
        var path1 = $"/api/auth/start?version=484&agent={agent}&login={login}&type=manager";

        var (is1Ok, response1) = await SendGet(path1);

        var jsonObj = System.Text.Json.JsonSerializer.Deserialize<AuthStep1Dto>(response1);

        if (!jsonObj.RetCode.Equals("0 Done"))
            return false;

        var cliRand = CliRand.Calculate();

        var srvRandAnswer = SrvRandAnswer.Calculate(password, jsonObj.RrvRand);

        var path2 = $"/api/auth/answer?srv_rand_answer={srvRandAnswer}&cli_rand={cliRand}";

        var (is2Ok, response2) = await SendGet(path2);

        return is2Ok;
    }

    public async ValueTask<(bool, string? content)> SendGet(string path)
    {
        var url = _host + path;

        var httpResponseMessage = _httpClient.GetAsync(new Uri(url)).Result;
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        Console.WriteLine($"{httpResponseMessage.IsSuccessStatusCode} {content}");
        return (httpResponseMessage.IsSuccessStatusCode, content);
    }
}