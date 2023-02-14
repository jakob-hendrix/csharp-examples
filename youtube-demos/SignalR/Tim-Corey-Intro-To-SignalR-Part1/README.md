# Tim Corey - Signal R Part 1

Video: https://www.youtube.com/watch?v=RaXx_f3bIRU&t=2950s

The Hub
- your web server can handle 1000s of connections
- `SendMessage`
- configure compression in the hub comms

```c#
builder.Services.AddResponseCompression(o =>
{
    o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
```

`@implements IAsyncDisposable` - allows to disconnect from the hub

Client side - captures message from the server

```c#
_hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
{
    var formattedMessage = $"{user}: {message}";
    _messages.Add(formattedMessage);
    InvokeAsync(StateHasChanged); // send event that some change occurred
});
```
