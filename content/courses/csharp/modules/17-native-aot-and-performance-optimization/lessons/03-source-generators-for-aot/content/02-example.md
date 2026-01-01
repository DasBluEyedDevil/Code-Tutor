---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

// ===== JSON SOURCE GENERATOR =====
// Generates serialization code at compile time

public record Product(int Id, string Name, decimal Price, string[] Tags);
public record Order(int OrderId, List<Product> Items, DateTime Created);

[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(Order))]
[JsonSerializable(typeof(List<Product>))]
[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class AppJsonContext : JsonSerializerContext { }

// Usage - NO reflection at runtime!
var product = new Product(1, "Widget", 29.99m, new[] { "tools", "gadgets" });
var json = JsonSerializer.Serialize(product, AppJsonContext.Default.Product);
Console.WriteLine($"JSON: {json}");

var parsed = JsonSerializer.Deserialize(json, AppJsonContext.Default.Product);
Console.WriteLine($"Parsed: {parsed?.Name}");

// ===== REGEX SOURCE GENERATOR =====
// Compiles regex at build time for better performance

public partial class Validators
{
    // Email validation - compiled at build time!
    [GeneratedRegex(@"^[\w.-]+@[\w.-]+\.\w+$", RegexOptions.IgnoreCase)]
    public static partial Regex EmailRegex();
    
    // Phone number - US format
    [GeneratedRegex(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$")]
    public static partial Regex PhoneRegex();
    
    // URL validation
    [GeneratedRegex(@"^https?://[\w.-]+(/[\w./]*)?$", RegexOptions.IgnoreCase)]
    public static partial Regex UrlRegex();
}

// Usage - instant, no runtime compilation
Console.WriteLine($"\nEmail valid: {Validators.EmailRegex().IsMatch("test@example.com")}");
Console.WriteLine($"Phone valid: {Validators.PhoneRegex().IsMatch("(555) 123-4567")}");
Console.WriteLine($"URL valid: {Validators.UrlRegex().IsMatch("https://example.com/path")}");

// ===== LOGGING SOURCE GENERATOR =====
// High-performance, zero-allocation logging

public static partial class LogMessages
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Processing order {OrderId} with {ItemCount} items")]
    public static partial void LogOrderProcessing(
        ILogger logger, int orderId, int itemCount);
    
    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Order {OrderId} total {Total:C} exceeds limit")]
    public static partial void LogOrderLimitExceeded(
        ILogger logger, int orderId, decimal total);
    
    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Failed to process order {OrderId}")]
    public static partial void LogOrderFailed(
        ILogger logger, int orderId, Exception ex);
}

// Usage with ILogger
// LogMessages.LogOrderProcessing(logger, 12345, 5);
// LogMessages.LogOrderLimitExceeded(logger, 12345, 10000m);
// LogMessages.LogOrderFailed(logger, 12345, exception);

Console.WriteLine("\nSource generators active:");
Console.WriteLine("- JSON: Serialization without reflection");
Console.WriteLine("- Regex: Patterns compiled at build time");
Console.WriteLine("- Logging: Zero-allocation log messages");
Console.WriteLine("\nAll code generated at compile time = AOT ready!");
```
