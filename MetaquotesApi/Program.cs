using MetaquotesApi;

var connector = new Connector("127.0.0.1:443");

await connector.SendAuth("9999", "MY_PASSWORD", "AGENT");


var time = DateTime.Now;

var (_, totalDto) = await connector.SendGet<RootResultDto<TotalDto>>("/api/group/total");

Console.WriteLine(totalDto);

var total = int.Parse(totalDto!.Answer!.Total!);

var results = new List<string>();

const uint chunkSize = 100;
for (uint i = 0; i < total; i += chunkSize)
{
    var j = chunkSize;
    if (i + chunkSize > total)
        j = (uint)total - i;

    var (_, rootResultDto) = await connector.SendGet<RootResultDto<GroupDto[]>>($"/api/group/next?index={0}&count={j}");

    results.AddRange(rootResultDto!.Answer!.Select(s => s.Name));
}

foreach (var item in results)
{
    Console.WriteLine(item);
}

var diff = DateTime.Now - time;

Console.WriteLine(results.Count);

Console.WriteLine(diff);