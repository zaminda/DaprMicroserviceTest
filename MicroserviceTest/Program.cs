using System.Net.Mime;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/test", (DaprClient dc,  MyData data) =>
{
    var x = new MyData(Text: data.Text + Environment.TickCount);
    dc.SaveStateAsync("mystore", data.Text, x.Text);
    return Results.Ok(x);
});

app.MapGet("/test/{id}", (DaprClient dc, string id) =>
{
    var r = dc.GetStateAsync<string>("mystore", id);
    return r;
});

app.Run();


record MyData (string Text);