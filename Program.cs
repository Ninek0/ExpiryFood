using ExpiryFood.Database.DatabaseProviders;
using ExpiryFood.Database;
using ExpiryFood.Repositories;
using ExpiryFood.Services;

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

// Register DB factory
builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<ExpiryFood.Repositories.Interface.IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

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

// TODO
// Add FoodService