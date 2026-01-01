using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Logging;

public class CartMetrics
{
    // TODO: Define metrics
    // - Counter for items added (with category tag)
    // - Counter for items removed
    // - Histogram for cart values
    // - UpDownCounter for active carts
    
    public CartMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("MyApp.Cart");
        
        // TODO: Create metrics
    }
    
    public void ItemAdded(string category)
    {
        // TODO: Increment counter with category tag
    }
    
    public void ItemRemoved()
    {
        // TODO: Increment removed counter
    }
    
    public void RecordCartValue(double value)
    {
        // TODO: Record histogram value
    }
}

public class CartService
{
    private static readonly ActivitySource ActivitySource = new("MyApp.Cart");
    
    private readonly ILogger<CartService> _logger;
    private readonly CartMetrics _metrics;
    
    public CartService(ILogger<CartService> logger, CartMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }
    
    public void AddToCart(string cartId, string productId, int quantity, string category, decimal price)
    {
        // TODO: Start activity/span with tags
        // TODO: Log structured message
        // TODO: Record metrics
        // TODO: Warn if cart > $1000
    }
}

Console.WriteLine("Implement observability for CartService!");