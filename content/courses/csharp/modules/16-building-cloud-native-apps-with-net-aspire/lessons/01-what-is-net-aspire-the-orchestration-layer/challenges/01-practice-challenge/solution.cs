// E-Commerce AppHost Configuration
// This orchestrates our entire e-commerce system

var builder = DistributedApplication.CreateBuilder(args);

// ===== INFRASTRUCTURE =====
// Redis for caching product data (fast reads)
var cache = builder.AddRedis("productcache");

// Postgres for persistent order storage
var orderDb = builder.AddPostgres("postgres")
    .AddDatabase("orderdb");

// RabbitMQ for async communication between services
var messaging = builder.AddRabbitMQ("messaging");

// ===== SERVICES =====
// Product API - handles product catalog, uses cache
var productApi = builder.AddProject<Projects.ProductApi>("productapi")
    .WithReference(cache);

// Order API - handles orders, needs DB and messaging
var orderApi = builder.AddProject<Projects.OrderApi>("orderapi")
    .WithReference(orderDb)
    .WithReference(messaging);

// Web storefront - talks to both APIs
builder.AddProject<Projects.WebStore>("webstore")
    .WithReference(productApi)
    .WithReference(orderApi);

builder.Build().Run();

// Print what we configured
Console.WriteLine("E-Commerce system configured!");
Console.WriteLine("Infrastructure: Redis, Postgres, RabbitMQ");
Console.WriteLine("Services: ProductApi, OrderApi, WebStore");
Console.WriteLine("Dashboard: http://localhost:18888");