using MetaquotesApi;

var connector = new Connector("127.0.0.1:443");

await connector.SendAuth("9999", "MY_PASSWORD", "AGENT");

await connector.SendGet("/api/group/total");