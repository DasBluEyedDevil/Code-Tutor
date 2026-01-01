// E-Commerce AppHost Configuration
// This orchestrates our entire e-commerce system

var builder = DistributedApplication.CreateBuilder(args);

// ===== INFRASTRUCTURE =====
// TODO: Add Redis cache for products
var cache = builder.AddRedis("productcache");

// TODO: Add Postgres for orders

// TODO: Add RabbitMQ for messaging

// ===== SERVICES =====
// TODO: Add ProductApi with cache reference

// TODO: Add OrderApi with database and messaging references

// TODO: Add WebStore with references to both APIs

builder.Build().Run();

// Print what we configured
Console.WriteLine("E-Commerce system configured!");