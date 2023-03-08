using Microsoft.Extensions.DependencyInjection;
using OrderApiApp.Model;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["DbConnectionString"];

builder.Services.AddDbContext<YguckjysContext>();



var app = builder .Build();

app.MapGet("/", () => connectionString);

app.Run();
