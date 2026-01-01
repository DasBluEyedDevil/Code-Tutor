---
type: "DEEP_DIVE"
title: "Loop Performance: What Actually Happens"
---

## Understanding Loop Performance at the Hardware Level

When you write a loop in C#, there's a lot happening behind the scenes. The JIT (Just-In-Time) compiler optimizes your code, and different loop constructs have different performance characteristics. Let's explore what actually happens when you iterate over collections in your ShopFlow e-commerce application.

### Array vs List Iteration

Arrays offer the fastest iteration because they provide direct memory access. When you access `products[i]`, the runtime calculates the exact memory address using pointer arithmetic: `baseAddress + (i * elementSize)`. This is a single CPU operation.

Lists (`List<T>`) are backed by an internal array, but accessing elements requires an additional bounds check and method call overhead. However, the JIT compiler is smart enough to eliminate most of this overhead in tight loops through a process called "bounds check elimination."

```csharp
// Direct array access - fastest for hot paths
Product[] products = GetProductArray();
for (int i = 0; i < products.Length; i++)
{
    ProcessProduct(products[i]);
}

// List iteration - nearly identical after JIT optimization
List<Product> productList = GetProductList();
for (int i = 0; i < productList.Count; i++)
{
    ProcessProduct(productList[i]);
}
```

### for vs foreach: When to Use Each

The `for` loop gives you index-based access and complete control over iteration. The `foreach` loop uses an enumerator pattern, which traditionally added overhead. However, modern C# heavily optimizes foreach:

**For arrays**: The compiler transforms `foreach` into an indexed `for` loop automatically. There's zero performance difference.

**For List<T>**: The compiler uses a struct-based enumerator that avoids heap allocations. Performance is nearly identical to a `for` loop.

**For IEnumerable<T>**: This requires interface dispatch and potentially boxing. Use sparingly in performance-critical code.

```csharp
// Both compile to nearly identical IL for arrays
foreach (var item in cartItems) { Process(item); }
for (int i = 0; i < cartItems.Length; i++) { Process(cartItems[i]); }

// When you need the index, for is cleaner
for (int i = 0; i < orders.Count; i++)
{
    Console.WriteLine($"Order {i + 1}: {orders[i].Total}");
}
```

### LINQ vs Loops: The Readability-Performance Trade-off

LINQ provides elegant, declarative code but comes with costs: delegate allocations, iterator overhead, and captured variable closures. For most code, this doesn't matter. For hot paths processing thousands of items per second, it can.

```csharp
// LINQ - readable, but allocates
var expensiveItems = products.Where(p => p.Price > 100).ToList();

// Loop - more verbose, but zero allocations
var expensiveItems = new List<Product>();
foreach (var p in products)
{
    if (p.Price > 100)
        expensiveItems.Add(p);
}
```

**Rule of thumb**: Use LINQ for clarity in business logic. Switch to loops only when profiling shows a bottleneck.

### Span<T> for Ultimate Performance

`Span<T>` provides array-like performance with added safety and flexibility. It's a stack-only type that can reference arrays, stack memory, or native buffers without allocations.

```csharp
// Process a slice of inventory without copying
ReadOnlySpan<Product> topProducts = products.AsSpan(0, 10);
foreach (var product in topProducts)
{
    DisplayProduct(product);
}

// Parse CSV without string allocations
ReadOnlySpan<char> line = csvLine.AsSpan();
int commaIndex = line.IndexOf(',');
ReadOnlySpan<char> productName = line.Slice(0, commaIndex);
```

### Practical Guidance for E-commerce Code

1. **Product catalog browsing**: Use LINQ for filtering and sorting. The readability benefit outweighs the small performance cost.

2. **Bulk order processing**: Consider switching to for loops with arrays when processing thousands of orders in batch jobs.

3. **Real-time inventory updates**: Profile first, then optimize. `Span<T>` can help when parsing high-frequency data feeds.

4. **General rule**: Write clear code first. Optimize only what benchmarks prove is slow.