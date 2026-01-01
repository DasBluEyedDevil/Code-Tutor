---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== ServiceDefaults/Extensions.cs =====
// This is included in Aspire templates!

public static IHostApplicationBuilder ConfigureOpenTelemetry(
    this IHostApplicationBuilder builder)
{
    builder.Logging.AddOpenTelemetry(logging =>
    {
        logging.IncludeFormattedMessage = true;
        logging.IncludeScopes = true;
    });
    
    builder.Services.AddOpenTelemetry()
        .WithMetrics(metrics =>
        {
            // Built-in metrics from ASP.NET Core, HttpClient, etc.
            metrics.AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddRuntimeInstrumentation();
        })
        .WithTracing(tracing =>
        {
            // Trace requests through your services
            tracing.AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddEntityFrameworkCoreInstrumentation();
        });
    
    // Export to Aspire Dashboard (OTLP protocol)
    builder.AddOpenTelemetryExporters();
    
    return builder;
}

// ===== Using Structured Logging =====
public class OrderService
{
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }
    
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        // STRUCTURED logging - properties are indexed and searchable!
        _logger.LogInformation(
            "Creating order for customer {CustomerId} with {ItemCount} items",
            request.CustomerId,
            request.Items.Count);
        
        try
        {
            var order = new Order { /* ... */ };
            
            _logger.LogInformation(
                "Order {OrderId} created successfully. Total: {Total:C}",
                order.Id,
                order.Total);
            
            return order;
        }
        catch (Exception ex)
        {
            // Log exception with full details
            _logger.LogError(ex,
                "Failed to create order for customer {CustomerId}",
                request.CustomerId);
            throw;
        }
    }
}

// ===== Custom Metrics =====
using System.Diagnostics.Metrics;

public class OrderMetrics
{
    private readonly Counter<int> _ordersCreated;
    private readonly Histogram<double> _orderProcessingTime;
    private readonly UpDownCounter<int> _activeOrders;
    
    public OrderMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("MyApp.Orders");
        
        _ordersCreated = meter.CreateCounter<int>(
            "orders.created",
            description: "Number of orders created");
        
        _orderProcessingTime = meter.CreateHistogram<double>(
            "orders.processing_time",
            unit: "ms",
            description: "Order processing time in milliseconds");
        
        _activeOrders = meter.CreateUpDownCounter<int>(
            "orders.active",
            description: "Currently processing orders");
    }
    
    public void OrderCreated(string region)
    {
        // Tags add dimensions to metrics
        _ordersCreated.Add(1, new KeyValuePair<string, object?>("region", region));
    }
    
    public void RecordProcessingTime(double milliseconds)
    {
        _orderProcessingTime.Record(milliseconds);
    }
    
    public void OrderStarted() => _activeOrders.Add(1);
    public void OrderCompleted() => _activeOrders.Add(-1);
}

// ===== Custom Tracing (ActivitySource) =====
using System.Diagnostics;

public class PaymentService
{
    private static readonly ActivitySource ActivitySource = 
        new("MyApp.Payments");
    
    public async Task<PaymentResult> ProcessPaymentAsync(Payment payment)
    {
        // Start a new span (trace segment)
        using var activity = ActivitySource.StartActivity("ProcessPayment");
        
        // Add attributes to the span
        activity?.SetTag("payment.amount", payment.Amount);
        activity?.SetTag("payment.method", payment.Method);
        
        try
        {
            var result = await CallPaymentGatewayAsync(payment);
            activity?.SetTag("payment.success", true);
            return result;
        }
        catch (Exception ex)
        {
            activity?.SetTag("payment.success", false);
            activity?.SetTag("error.message", ex.Message);
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            throw;
        }
    }
}

Console.WriteLine("OpenTelemetry configured!");
Console.WriteLine("View in Aspire Dashboard: http://localhost:18888");
Console.WriteLine("Logs, Metrics, and Traces all in one place!");
```
