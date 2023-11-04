using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapGet("/hello", async (DaprClient daprClient) =>
{
    var httpClinet = DaprClient.CreateInvokeHttpClient();
    var result = await httpClinet.GetAsync("http://myapp/test/ninja");
    var text = await result.Content.ReadAsStringAsync();
    return $"Content from ninja = {text}";
});

app.Run();