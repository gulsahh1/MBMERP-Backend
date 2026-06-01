using Application.Features.Commands.Categories;
using Application.Features.Commands.Customers;
using Application.Features.Commands.Payments;
using Application.Features.Commands.Products;
using Application.Features.Commands.SaleDetails;
using Application.Features.Commands.Sales;
using Application.Features.Commands.Stocks;
using Application.Features.Commands.StockTransactions;
using Application.Interfaces;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Seed;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using WebAPI.Hubs;
using WebAPI.Services;
using System.Text.Json;
using WebAPI.RealTime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ERPDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateProductCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateCustomerCommand).Assembly);
builder.Services.AddMediatR(typeof(CreatePaymentCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateSaleCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateSaleDetailCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateStockCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateStockTransactionCommand).Assembly);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddTransient<IDashboardHubService, DashboardHubService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


builder.Services.AddControllers().AddJsonOptions(options=>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(key))
    throw new Exception("JWT Key bulunamadý");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ValidateIssuer = true,
        ValidateAudience = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        RoleClaimType = ClaimTypes.Role
    };

    //options.Events = new JwtBearerEvents
    //{
    //    OnAuthenticationFailed = context =>
    //    {
    //        Console.WriteLine("?? JWT ERROR:");
    //        Console.WriteLine(context.Exception.Message);
    //        return Task.CompletedTask;
    //    },

    //    OnMessageReceived = context =>
    //    {
    //        Console.WriteLine("?? AUTH HEADER:");
    //        Console.WriteLine(context.Request.Headers["Authorization"]);
    //        return Task.CompletedTask;
    //    }
    //};
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(RoleType.Admin.ToString()));

    options.AddPolicy("ManagerOrAdmin", policy =>
        policy.RequireRole(RoleType.Admin.ToString(), RoleType.Manager.ToString()));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ERPDbContext>();

    //CategorySeeder.Seed(context);
    //CustomerSeeder.Seed(context);
    //ProductSeeder.Seed(context);

    //OrderSeeder.Seed(context);
    //OrderDetailSeeder.Seed(context);

    //StockSeeder.Seed(context);
    //StockTransSeeder.Seed(context);
    //SalesSeeder.Seed(context);
    //SaleDetailSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("cors");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<DashboardHub>("/hubs/dashboard");
app.MapHub<NotificationHub>("/hubs/notification");

app.Run();
