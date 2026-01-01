---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates HybridCache in .NET 9.

```csharp
// === SETUP (.NET 9) ===
// Install: dotnet add package Microsoft.Extensions.Caching.Hybrid

// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add HybridCache with Redis as L2
builder.Services.AddHybridCache(options =>
{
    options.MaximumPayloadBytes = 1024 * 1024; // 1MB max
    options.MaximumKeyLength = 1024; // Default is 1024
});

// Add Redis as distributed cache backend
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// === USAGE ===
public class ProductService
{
    private readonly HybridCache _cache;
    private readonly AppDbContext _db;
    
    public ProductService(HybridCache cache, AppDbContext db)
    {
        _cache = cache;
        _db = db;
    }
    
    public async Task<Product?> GetProductAsync(int id)
    {
        // GetOrCreateAsync: check cache, or run factory
        return await _cache.GetOrCreateAsync(
            $"product:{id}",  // Cache key
            async token => await _db.Products.FindAsync(id, token),
            new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(5),
                LocalCacheExpiration = TimeSpan.FromMinutes(1)
            }
        );
    }
}

// === TAG-BASED INVALIDATION (.NET 9 GA) ===
public async Task<List<Product>> GetCategoryProductsAsync(string category)
{
    return await _cache.GetOrCreateAsync(
        $"products:category:{category}",
        async token => await _db.Products
            .Where(p => p.Category == category)
            .ToListAsync(token),
        new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(10)
        },
        tags: new[] { "products", $"category:{category}" }  // Tags!
    );
}

// Invalidate all products when catalog changes
public async Task InvalidateProductCacheAsync()
{
    await _cache.RemoveByTagAsync("products");  // Remove ALL tagged entries!
}

// === STAMPEDE PROTECTION (Automatic!) ===
// 1000 requests hit cache miss simultaneously?
// OLD: 1000 database queries (stampede!)
// HybridCache: Only 1 query, 999 wait for result

// === COMPARISON ===
// IMemoryCache: Fast, local only, no sharing
// IDistributedCache: Shared, slower, no local tier
// HybridCache: BOTH! Fast local + shared distributed
```
