using wasmHosted.Server.Handlers;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using System.Reflection;
using Newtonsoft.Json;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMediatR(typeof(ForecastQueryHandler).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapPost("/rpc",
    async context =>
    {

        StreamReader reader = new StreamReader(context.Request.Body);
        string requestJson = await reader.ReadToEndAsync();

        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            TypeNameHandling = TypeNameHandling.All
        };

        object? requestObject = JsonConvert.DeserializeObject(requestJson, settings);

        IMediator? mediator = context.RequestServices.GetService<IMediator>();

        Debug.Assert(mediator != null, nameof(mediator) + " != null");
        object? commandQueryResponse = await mediator.Send(requestObject);

        string responseJson = JsonConvert.SerializeObject(commandQueryResponse, settings);

        await context.Response.WriteAsync(responseJson);
    });

app.Run();
