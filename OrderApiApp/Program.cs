using Microsoft.Extensions.DependencyInjection;
using OrderApiApp.Model;
using OrderApiApp.Model.Entity;
using System;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["DbConnectionString"];

builder.Services.AddTransient<IGenericRepository<Client>, EFGenericRepository<Client>>();
builder.Services.AddTransient<IGenericRepository<Order>, EFGenericRepository<Order>>();
builder.Services.AddTransient<IGenericRepository<OrderInfo>, EFGenericRepository<OrderInfo>>();
builder.Services.AddTransient<IGenericRepository<Product>, EFGenericRepository<Product>>();

builder.Services.AddDbContext<YguckjysContext>();



var app = builder .Build();

app.MapGet("/", () => connectionString);

app.Run();
