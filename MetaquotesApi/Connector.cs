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

        var (is1Ok, response1) = await SendGet<AuthStep1Dto>(path1);

        if (response1 is null)
            return false;
        if (response1.RetCode != "0 Done")
            return false;

        var cliRand = CliRand.Calculate();

        var srvRandAnswer = SrvRandAnswer.Calculate(password, response1.RrvRand);

        var path2 = $"/api/auth/answer?srv_rand_answer={srvRandAnswer}&cli_rand={cliRand}";

        var (is2Ok, _) = await SendGet<AuthStep2Dto>(path2);

        return is2Ok;
    }

    public async ValueTask<(bool, T? content)> SendGet<T>(string path)
    {
        var url = _host + path;

        var httpResponseMessage = _httpClient.GetAsync(new Uri(url)).Result;

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return (true, await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream));
        }

        return (false, default);
    }

    public async ValueTask SendDebug(string path)
    {
        var url = _host + path;

        var httpResponseMessage = _httpClient.GetAsync(new Uri(url)).Result;

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            Console.WriteLine(responseContent);
        }
    }
}