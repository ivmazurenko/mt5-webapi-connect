# MetaQuotes Web API / Manager Interface (Rest API)

This repository contains a basic implementation of MetaQuotes Web API / Manager Interface (Rest API) for connecting to
MetaQuotes servers and performing authentication.

## Usage Example

```c#
using MetaquotesApi;

var connector = new Connector("127.0.0.1:443");

await connector.SendAuth("9999", "MY_PASSWORD", "AGENT");

await connector.SendGet("/api/group/total");
```

## Authentication Process

1. Request to Start Authentication: Send a request to start authentication.
2. Start of Authentication on the Access Server: Receive a response containing a random sequence from the access server.
3. Response to the Server: Apply a password hash to the random sequence and send it back to the server.
4. End of Authentication: Receive confirmation of successful authentication.

## Documentation

Refer to the [MetaQuotes API Documentation](https://support.metaquotes.net/en/docs/mt5/api/webapi_rest_authentication)
for detailed information on the authentication process.

Feel free to contribute and expand upon this implementation as needed.
