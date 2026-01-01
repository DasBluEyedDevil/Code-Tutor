---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== BENCHMARKDOTNET SETUP =====
// Install: dotnet add package BenchmarkDotNet

// Run benchmarks:
// dotnet run -c Release
// IMPORTANT: Must be Release mode for accurate results!

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

// Entry point
BenchmarkRunner.Run<StringConcatBenchmarks>();
// BenchmarkRunner.Run<JsonBenchmarks>();
// BenchmarkRunner.Run<CollectionBenchmarks>();

// ===== STRING CONCATENATION BENCHMARKS =====
[MemoryDiagnoser]  // Track memory allocations
[RankColumn]       // Show ranking
public class StringConcatBenchmarks
{
    private readonly string[] _items = Enumerable.Range(1, 100)
        .Select(i => $"Item{i}")
        .ToArray();
    
    [Benchmark(Baseline = true)]  // This is the baseline for comparison
    public string StringConcat()
    {
        var result = "";
        foreach (var item in _items)
            result += item + ", ";  // Creates new string each time!
        return result;
    }
    
    [Benchmark]
    public string StringBuilder_Approach()
    {
        var sb = new StringBuilder();
        foreach (var item in _items)
            sb.Append(item).Append(", ");
        return sb.ToString();
    }
    
    [Benchmark]
    public string StringJoin()
    {
        return string.Join(", ", _items);
    }
}

// ===== JSON SERIALIZATION BENCHMARKS =====
[MemoryDiagnoser]
public class JsonBenchmarks
{
    private readonly Product _product = new(1, "Widget", 29.99m);
    private readonly string _json = "{\"Id\":1,\"Name\":\"Widget\",\"Price\":29.99}";
    
    // Reflection-based (traditional)
    [Benchmark(Baseline = true)]
    public string Serialize_Reflection()
    {
        return JsonSerializer.Serialize(_product);
    }
    
    // Source-generated (AOT-compatible)
    [Benchmark]
    public string Serialize_SourceGen()
    {
        return JsonSerializer.Serialize(_product, BenchJsonContext.Default.Product);
    }
    
    [Benchmark]
    public Product? Deserialize_Reflection()
    {
        return JsonSerializer.Deserialize<Product>(_json);
    }
    
    [Benchmark]
    public Product? Deserialize_SourceGen()
    {
        return JsonSerializer.Deserialize(_json, BenchJsonContext.Default.Product);
    }
}

[JsonSerializable(typeof(Product))]
internal partial class BenchJsonContext : JsonSerializerContext { }

public record Product(int Id, string Name, decimal Price);

// ===== COLLECTION BENCHMARKS =====
[MemoryDiagnoser]
public class CollectionBenchmarks
{
    private readonly int[] _numbers = Enumerable.Range(1, 1000).ToArray();
    
    [Benchmark(Baseline = true)]
    public int LinqSum()
    {
        return _numbers.Where(n => n % 2 == 0).Sum();
    }
    
    [Benchmark]
    public int ForLoopSum()
    {
        var sum = 0;
        for (var i = 0; i < _numbers.Length; i++)
        {
            if (_numbers[i] % 2 == 0)
                sum += _numbers[i];
        }
        return sum;
    }
    
    [Benchmark]
    public int SpanSum()
    {
        var sum = 0;
        var span = _numbers.AsSpan();
        foreach (var n in span)
        {
            if (n % 2 == 0)
                sum += n;
        }
        return sum;
    }
}

// ===== SAMPLE OUTPUT =====
// |             Method |        Mean |    Allocated |
// |------------------- |------------:|-------------:|
// |       StringConcat | 45,234.5 ns |    123,456 B |
// | StringBuilder_     |    567.8 ns |      1,024 B |
// |         StringJoin |    234.5 ns |        512 B |

Console.WriteLine("Benchmark complete!");
Console.WriteLine("Key insights:");
Console.WriteLine("- String += in loop is VERY slow (creates new strings)");
Console.WriteLine("- StringBuilder is ~100x faster for many concatenations");
Console.WriteLine("- string.Join is even faster for simple cases");
Console.WriteLine("- Source-generated JSON is faster than reflection");
Console.WriteLine("- For loops often beat LINQ for raw performance");
```
