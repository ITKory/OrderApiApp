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

//Client
app.MapGet("/client/all", async (HttpContext context,  IGenericRepository<Client> client) =>
{
    return await client.GetAllAsync();
});
app.MapPost("/client/add", async (HttpContext context, IGenericRepository<OrderInfo> repository, OrderInfo orderInfo) => {

    var result = await repository.CreateAsync(orderInfo);
    if (result is null)
        return new();
    return result;
});
app.MapPost("/client/update", (HttpContext context, IGenericRepository<Client> repository, Client client) =>
{
    repository.Update(client);
    var student = repository.FindById(client.Id);
    return student;

});
app.MapDelete("/client/del/{id}", (HttpContext context, int id, IGenericRepository<Client> repository) =>
{
    var student = repository.FindById(id);
    if (student is not null)
        repository.Remove(student);

});
//Product
app.MapGet("/product/all", async (HttpContext context, IGenericRepository<Product> product) =>
{
    return await product.GetAllAsync();
});
app.MapPost("/product/add", async (HttpContext context, IGenericRepository<Product> repository, Product product) => {

    var result = await repository.CreateAsync(product);
    if (result is null)
        return new();
    return result;
});
app.MapPost("/product/update", (HttpContext context, IGenericRepository<Product> repository, Product product) =>
{
    repository.Update(product);
    return  repository.FindById(product.Id);
 

});
app.MapDelete("/product/del/{id}", (HttpContext context, int id, IGenericRepository<Product> repository) =>
{
    var product = repository.FindById(id);
    if (product is not null)
        repository.Remove(product);

});
//Order
app.MapGet("/order/all", async (HttpContext context, IGenericRepository<Order> order) =>
{
    return await order.GetAllAsync();
});
app.MapPost("/order/add", async (HttpContext context, IGenericRepository<Order> repository, Order order) => {

    var result = await repository.CreateAsync(order);
    if (result is null)
        return new();
    return result;
});
app.MapPost("/order/update", (HttpContext context, IGenericRepository<Order> repository, Order order) =>
{
    repository.Update(order);
    var student = repository.FindById(order.Id);
    return student;

});
app.MapDelete("/order/del/{id}", (HttpContext context, int id, IGenericRepository<Order> repository) =>
{
    var order = repository.FindById(id);
    if (order is not null)
        repository.Remove(order);

});
//OrderInfo
app.MapGet("/orderinfo/all", async (HttpContext context, IGenericRepository<OrderInfo> orderInfo) =>
{
    return await orderInfo.GetAllAsync();
});
app.MapPost("/orderinfo/add", async (HttpContext context, IGenericRepository<OrderInfo> repository, OrderInfo client) => {

    var result = await repository.CreateAsync(client);
    if (result is null)
        return new();
    return result;
});
app.MapPost("/orderinfo/update", (HttpContext context, IGenericRepository<OrderInfo> repository, OrderInfo orderInfo) =>
{
    repository.Update(orderInfo);
    return repository.FindById(orderInfo.Id);
    

});
app.MapDelete("/orderinfo/del/{id}", (HttpContext context, int id, IGenericRepository<OrderInfo> repository) =>
{
    var orderInfo = repository.FindById(id);
    if (orderInfo is not null)
        repository.Remove(orderInfo);

});

app.Run();
