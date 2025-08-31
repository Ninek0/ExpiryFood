using ExpiryFood.Database.DatabaseProviders;
using ExpiryFood.Database;
using ExpiryFood.Repositories;
using ExpiryFood.Services;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using ExpiryFood.Notification;
using ExpiryFood.Notification.NotificationProvider;
using Telegram.Bot;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add database
var dbConfig = builder.Configuration.GetSection("DatabaseConfig").Get<DatabaseConfig>();

switch (dbConfig!.Type)
{
    case "MongoDB":
        builder.Services.AddSingleton<IDatabaseProvider>(_ =>
            new MongoDbProvider(dbConfig.ConnectionString));
        break;
    case "LiteDB":
        builder.Services.AddSingleton<IDatabaseProvider>(_ =>
            new LiteDbProvider(dbConfig.ConnectionString));
        break;
    default:
        throw new Exception("Unsupported database type");
}

// Add notification
var notificationConfig = builder.Configuration.GetSection("NotificationConfig");
var notificationType = notificationConfig["Type"];

var providers = new List<INotificationProvider>();

if (notificationType == "Telegram" || notificationType == "Both")
{
    providers.Add(NotificationProviderFactory.CreateTelegramProvider(builder.Configuration));
}

//if (notificationType == "Email" || notificationType == "Both")
//{
//    providers.Add(NotificationProviderFactory.CreateEmailProvider(builder.Configuration));
//}

builder.Services.AddSingleton<List<INotificationProvider>>(providers);

builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<ExpiryFood.Repositories.Interface.IProductRepository, ProductRepository>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddHostedService<ExpiryNotificationService>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
