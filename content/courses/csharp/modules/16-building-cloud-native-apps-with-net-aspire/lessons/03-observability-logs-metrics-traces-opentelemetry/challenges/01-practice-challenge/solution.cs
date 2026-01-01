using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Logging;

public class CartMetrics
{
    private readonly Counter<int> _itemsAdded;
    private readonly Counter<int> _itemsRemoved;
    private readonly Histogram<double> _cartValue;
    private readonly UpDownCounter<int> _activeCarts;
    
    public CartMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("MyApp.Cart");
        
        _itemsAdded = meter.CreateCounter<int>(
            "cart.items_added",
            description: "Number of items added to carts");
        
        _itemsRemoved = meter.CreateCounter<int>(
            "cart.items_removed",
            description: "Number of items removed from carts");
        
        _cartValue = meter.CreateHistogram<double>(
            "cart.value",
            unit: "USD",
            description: "Shopping cart total values");
        
        _activeCarts = meter.CreateUpDownCounter<int>(
            "cart.active",
            description: "Number of active shopping carts");
    }
    
    public void ItemAdded(string category)
    {
        _itemsAdded.Add(1, new KeyValuePair<string, object?>("product_category", category));
    }
    
    public void ItemRemoved()
    {
        _itemsRemoved.Add(1);
    }
    
    public void RecordCartValue(double value)
    {
        _cartValue.Record(value);
    }
    
    public void CartCreated() => _activeCarts.Add(1);
    public void CartCompleted() => _activeCarts.Add(-1);
}

public class CartService
{
    private static readonly ActivitySource ActivitySource = new("MyApp.Cart");
    
    private readonly ILogger<CartService> _logger;
    private readonly CartMetrics _metrics;
    private decimal _cartTotal = 0;
    
    public CartService(ILogger<CartService> logger, CartMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }
    
    public void AddToCart(string cartId, string productId, int quantity, string category, decimal price)
    {
        using var activity = ActivitySource.StartActivity("AddToCart");
        activity?.SetTag("cart.id", cartId);
        activity?.SetTag("product.id", productId);
        activity?.SetTag("product.category", category);
        activity?.SetTag("quantity", quantity);
        
        _logger.LogInformation(
            "Adding to cart {CartId}: Product {ProductId}, Quantity {Quantity}, Category {Category}",
            cartId, productId, quantity, category);
        
        _cartTotal += price * quantity;
        
        _metrics.ItemAdded(category);
        _metrics.RecordCartValue((double)_cartTotal);
        
        if (_cartTotal > 1000)
        {
            _logger.LogWarning(
                "Cart {CartId} exceeds $1000! Current total: {CartTotal:C}",
                cartId, _cartTotal);
        }
        
        activity?.SetTag("cart.total", _cartTotal);
    }
}

Console.WriteLine("CartService with full observability!");
Console.WriteLine("Metrics: items_added (with category), items_removed, cart_value, active_carts");
Console.WriteLine("Logging: Structured with CartId, ProductId, Quantity");
Console.WriteLine("Tracing: AddToCart spans with product details");