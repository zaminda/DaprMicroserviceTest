using Dapr.Client;

using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();



var app = builder.Build();

app.MapGet("/hello", async (DaprClient daprClient) =>
{
    var httpClinet = DaprClient.CreateInvokeHttpClient();
    var result = await httpClinet.GetAsync("http://myapp/test/ninja");
    var text = await result.Content.ReadAsStringAsync();
    return $"Content from ninja = {text}";
});

app.MapPost("/message", async (ILogger<Program> logger, HttpRequest request) =>
{
    var body = new StreamReader(request.Body);
    logger.LogInformation(await body.ReadToEndAsync());
    return Results.NoContent();
}).WithTopic("pubsub", "newtest");

app.UseCloudEvents(); // used for dapr pubsub

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

app.Run();

