 
using OrderApiApp.Model;
using Microsoft.Extensions.DependencyInjection;
using OrderApiApp.Model.Entity;
using System;
using OrderApiApp.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
 
using OrderApiApp.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOption.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOption.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOption.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });


builder.Services.AddDbContext<FmjnwaqeContext>();
builder.Services.AddTransient<IGenericRepository<Client>, EFGenericRepository<Client>>();
builder.Services.AddTransient<IGenericRepository<Order>, EFGenericRepository<Order>>();
builder.Services.AddTransient<IGenericRepository<Cart>, EFGenericRepository<Cart>>();
builder.Services.AddTransient<IGenericRepository<Product>, EFGenericRepository<Product>>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();   

 
var app = builder .Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "" +
"/client/all\n" +
"/client/add\n" +
"/client/update\n" +
"/client/del/{id}\n" +
"/product/all\n" +
"/product/add\n" +
"/product/update\n" +
"/product/del/{id}\n" +
"/order/all\n" +
"/order/{clientId}/add\n" +
"/order/update\n" +
"/order/del/{id}\n" +
"/cart/all\n" +
"/cart/add\n" +
"/cart/update\n" +
"/cart/del/{id}\n" +
"/cheque/{clientId}\n" +
"/order/{orderId}\n"

);

//Client
app.MapGet("/client/all", async (HttpContext context,  IGenericRepository<Client> client) =>
{
    return await client.GetAllAsync();
});
app.MapPost("/client/add", async (HttpContext context, IGenericRepository<Client> repository, Client client) => {

    if (client is null) return new();
    return await repository.CreateAsync(client);
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
app.MapGet("/order/all",  (HttpContext context, IOrderRepository repository) =>
{
    return repository.GetAllOrders();
});
app.MapPost("/order/{clientId}/add", async (HttpContext context, IGenericRepository<Order> repository, int clientId, Order order) => {

    if (order is null) return new();
    var newOrder = await repository.CreateAsync(order);
    repository.CreateCart(clientId, newOrder);
    return order;
});
app.MapPost("/order/update", (HttpContext context, IGenericRepository<Order> repository, Order order) =>
{
    repository.Update(order);
    return repository.FindById(order.Id);
   
});
app.MapDelete("/order/del/{id}", (HttpContext context, int id, IOrderRepository repository) =>
{
    repository.DeleteOrder(id);

});
//OrderInfo
app.MapGet("/cart/all", async (HttpContext context, IGenericRepository<Cart> orderInfo) =>
{
    return await orderInfo.GetAllAsync();
});
app.MapPost("/cart/add", async (HttpContext context, IGenericRepository<Cart> repository, Cart client) => {

    if (client is null) return new();
    return await repository.CreateAsync(client);
});
app.MapPost("/cart/update", (HttpContext context, IGenericRepository<Cart> repository, Cart cart) =>
{
    repository.Update(cart);
    return repository.FindById(cart.Id);
    

});
app.MapDelete("/cart/del/{id}", (HttpContext context, int id, IGenericRepository<Cart> repository) =>
{
    var orderInfo = repository.FindById(id);
    if (orderInfo is not null)
        repository.Remove(orderInfo);

});

//cheque
app.MapGet("/cheque/{clientId}", (HttpContext context, int clientId, IGenericRepository<Cart> repository) => {
    return repository.GetCheque(0);
});
//client order
app.MapGet("/order/{orderId}", (HttpContext context, int orderId, IGenericRepository<Order> repository) => {

   return repository.GetFullOrderInfo(orderId);
});
//Auth
app.Map("/login/{username}", (string username) => {
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOption.ISSUER,
            audience: AuthOption.AUDIENCE,
    claims: claims,
    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwt);
});
app.Map("/data", [Authorize] (HttpContext context) => $"Hello World!");
app.Run();
          