Console.WriteLine("=== HYBRIDCACHE (.NET 9 - MODERN CACHING) ===");
Console.WriteLine("Two-level caching: L1 (memory) + L2 (distributed)");

Console.WriteLine("\n--- SETUP (Program.cs) ---");
Console.WriteLine(@"
// Install: dotnet add package Microsoft.Extensions.Caching.Hybrid

var builder = WebApplication.CreateBuilder(args);

// Add HybridCache
builder.Services.AddHybridCache(options =>
{
    options.MaximumPayloadBytes = 1024 * 1024; // 1MB
    options.MaximumKeyLength = 1024; // Default
});

// Add Redis as L2 (distributed) cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = ""localhost:6379"";
});
");

Console.WriteLine("\n--- USAGE (Service) ---");
Console.WriteLine(@"
public class ProductService(HybridCache cache, AppDbContext db)
{
    public async Task<Product?> GetProductAsync(int id)
    {
        return await cache.GetOrCreateAsync(
            $""product:{id}"",  // Key
            async token => await db.Products.FindAsync(id, token),
            new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(5),      // Total TTL
                LocalCacheExpiration = TimeSpan.FromMinutes(1) // L1 TTL
            }
        );
    }
}
");

Console.WriteLine("\n--- TAG-BASED INVALIDATION (.NET 9 GA) ---");
Console.WriteLine(@"
// Add with tags
await cache.GetOrCreateAsync(
    ""products:electronics"",
    async token => await db.Products.Where(p => p.Category == ""Electronics"").ToListAsync(),
    options,
    tags: new[] { ""products"", ""category:electronics"" }
);

// Invalidate ALL products at once!
await cache.RemoveByTagAsync(""products"");

// Or just one category
await cache.RemoveByTagAsync(""category:electronics"");
");

Console.WriteLine("\n--- STAMPEDE PROTECTION ---");
Console.WriteLine("Scenario: Cache miss, 1000 concurrent requests");
Console.WriteLine("");
Console.WriteLine("WITHOUT HybridCache:");
Console.WriteLine("  1000 database queries simultaneously! (stampede)");
Console.WriteLine("");
Console.WriteLine("WITH HybridCache:");
Console.WriteLine("  1 database query, 999 requests wait for result");
Console.WriteLine("  Automatic! No extra code needed.");

Console.WriteLine("\n--- COMPARISON ---");
Console.WriteLine("+---------------------+--------+-----------+----------+");
Console.WriteLine("| Feature             | Memory | Distrib.  | Hybrid   |");
Console.WriteLine("+---------------------+--------+-----------+----------+");
Console.WriteLine("| Speed               | Fast   | Slower    | Fast     |");
Console.WriteLine("| Shared across pods  | No     | Yes       | Yes      |");
Console.WriteLine("| Survives restart    | No     | Yes       | Yes (L2) |");
Console.WriteLine("| Stampede protection | No     | No        | YES!     |");
Console.WriteLine("| Tag invalidation    | No     | No        | YES!     |");
Console.WriteLine("+---------------------+--------+-----------+----------+");

Console.WriteLine("\n--- WHEN TO USE ---");
Console.WriteLine("IMemoryCache: Single server, simple caching");
Console.WriteLine("IDistributedCache: Multi-server, need sharing");
Console.WriteLine("HybridCache: The modern default! Best of both worlds.");