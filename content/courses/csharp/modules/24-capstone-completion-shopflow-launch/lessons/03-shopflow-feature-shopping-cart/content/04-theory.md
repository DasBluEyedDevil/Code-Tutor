---
type: "THEORY"
title: "Cart Persistence Strategies"
---

Choosing where and how to persist shopping cart data involves tradeoffs between performance, durability, and complexity. Each strategy has appropriate use cases.

## Database-Only Persistence

Storing carts directly in the relational database provides strong consistency and durability. Every cart operation executes immediately against the database, ensuring no data loss even if the application restarts. This approach works well for low-to-medium traffic applications where cart operations are infrequent compared to product browsing.

```csharp
public class DatabaseCartRepository : ICartRepository
{
    private readonly ShopFlowDbContext _context;

    public async Task<Cart?> GetByUserIdAsync(int userId, CancellationToken ct)
    {
        return await _context.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId, ct);
    }

    public async Task SaveAsync(Cart cart, CancellationToken ct)
    {
        if (cart.Id == 0)
            _context.Carts.Add(cart);
        else
            _context.Carts.Update(cart);

        await _context.SaveChangesAsync(ct);
    }
}
```

## Distributed Cache with Write-Through

For high-traffic sites, a distributed cache like Redis provides sub-millisecond access to cart data. Write-through caching updates both cache and database on every write, maintaining consistency while improving read performance.

```csharp
public class CachedCartRepository : ICartRepository
{
    private readonly IDistributedCache _cache;
    private readonly ICartRepository _inner;
    private readonly TimeSpan _cacheExpiry = TimeSpan.FromHours(24);

    public async Task<Cart?> GetByUserIdAsync(int userId, CancellationToken ct)
    {
        var cacheKey = $"cart:user:{userId}";
        var cached = await _cache.GetStringAsync(cacheKey, ct);

        if (cached is not null)
        {
            return JsonSerializer.Deserialize<Cart>(cached);
        }

        var cart = await _inner.GetByUserIdAsync(userId, ct);
        
        if (cart is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(cart),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry },
                ct);
        }

        return cart;
    }

    public async Task SaveAsync(Cart cart, CancellationToken ct)
    {
        // Write to database first
        await _inner.SaveAsync(cart, ct);

        // Then update cache
        var cacheKey = cart.UserId.HasValue 
            ? $"cart:user:{cart.UserId}" 
            : $"cart:session:{cart.SessionId}";

        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(cart),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry },
            ct);
    }
}
```

## Hybrid Strategy with Lazy Persistence

The most sophisticated approach keeps cart modifications in cache with periodic background persistence. This minimizes database writes during active shopping sessions while ensuring eventual durability. Cart changes are flushed to the database when the user proceeds to checkout, logs out, or after a configurable idle period.

```csharp
public class HybridCartRepository : ICartRepository
{
    private readonly IDistributedCache _cache;
    private readonly ShopFlowDbContext _context;
    private readonly IBackgroundJobClient _jobs;

    public async Task SaveAsync(Cart cart, CancellationToken ct)
    {
        // Immediate cache update for fast reads
        var cacheKey = GetCacheKey(cart);
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(cart), ct);

        // Mark cart as dirty and schedule background persistence
        await _cache.SetStringAsync($"{cacheKey}:dirty", "true", ct);
        
        // Debounced background job - only persists after 30 seconds of inactivity
        _jobs.Schedule<CartPersistenceJob>(
            job => job.PersistCart(cart.Id),
            TimeSpan.FromSeconds(30));
    }
}
```

The hybrid approach adds complexity but provides the best user experience for high-traffic e-commerce sites. Cart additions feel instantaneous because they only touch Redis, while background jobs ensure database consistency without blocking the user.