 
using OrderApiApp.Model;
using Microsoft.Extensions.DependencyInjection;
using OrderApiApp.Model.Entity;
using System;
using OrderApiApp.Service;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration["ConnectionStrings:DbConnectionString"];
builder.Services.AddDbContext<YguckjysContext>();
builder.Services.AddTransient<IGenericRepository<Client>, EFGenericRepository<Client>>();
builder.Services.AddTransient<IGenericRepository<Order>, EFGenericRepository<Order>>();
builder.Services.AddTransient<IGenericRepository<OrderInfo>, EFGenericRepository<OrderInfo>>();
builder.Services.AddTransient<IGenericRepository<Product>, EFGenericRepository<Product>>();

 



var app = builder .Build();

app.MapGet("/", () => "constr");

//Client
app.MapGet("/client/all", async (HttpContext context,  IGenericRepository<Client> client) =>
{
    return await client.GetAllAsync();
});
app.MapPost("/client/add", async (HttpContext context, IGenericRepository<OrderInfo> repository, OrderInfo orderInfo) => {

    if (orderInfo is null) return new();
    return await repository.CreateAsync(orderInfo);
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

    if(product is null) return new();
    return  await repository.CreateAsync(product);
    
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

    if (order is null) return new();
    return await repository.CreateAsync(order);
});
app.MapPost("/order/update", (HttpContext context, IGenericRepository<Order> repository, Order order) =>
{
    repository.Update(order);
    return repository.FindById(order.Id);
    

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

    if (client is null) return new();
    return await repository.CreateAsync(client);
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
