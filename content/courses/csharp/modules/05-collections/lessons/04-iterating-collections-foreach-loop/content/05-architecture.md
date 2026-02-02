---
type: "THEORY"
title: "Choosing the Right Collection"
---

## Collection Decision Guide for ShopFlow

Now that you know how to iterate collections with foreach, let's discuss how to choose the right collection for your needs.

Selecting the appropriate collection type is a critical architectural decision that impacts performance, memory usage, and code clarity. In our ShopFlow e-commerce platform, different scenarios call for different collection types. Here is a comprehensive decision table to guide your choices:

| Scenario | Collection | Why |
|----------|------------|-----|
| Product catalog by SKU | `Dictionary<string, Product>` | O(1) lookup by unique product identifier enables instant product retrieval |
| Shopping cart items | `List<CartItem>` | Ordered collection that allows duplicates and easy iteration for display |
| Unique category tags | `HashSet<string>` | Automatic deduplication ensures no duplicate tags, fast membership testing |
| Order processing queue | `Queue<Order>` | FIFO (First-In-First-Out) ensures orders are processed in submission order |
| Undo/redo cart operations | `Stack<CartAction>` | LIFO (Last-In-First-Out) naturally supports undo operations |
| Sorted price ranges | `SortedSet<decimal>` | Maintains automatic ascending order for price tier displays |

```csharp
// Example: Choosing the right collection for ShopFlow
var productCatalog = new Dictionary<string, Product>();
var cartItems = new List<CartItem>();
var uniqueTags = new HashSet<string>();
var orderQueue = new Queue<Order>();
```

### **Advanced:** Immutable Collections (System.Collections.Immutable)

For thread-safe, read-only data that should never change after creation, use immutable collections. In ShopFlow, these are ideal for static configuration data like shipping regions or tax rates. The `ImmutableList<T>`, `ImmutableDictionary<TKey, TValue>`, and `ImmutableHashSet<T>` types create new instances for any modification, ensuring the original remains unchanged. This is particularly valuable for caching product catalogs that multiple threads read simultaneously without locking concerns.

### **Advanced:** Concurrent Collections (System.Collections.Concurrent)

When multiple threads need to modify collections simultaneously, concurrent collections provide thread-safe operations without explicit locking. The `ConcurrentDictionary<TKey, TValue>` is perfect for ShopFlow's real-time inventory tracking where multiple order processors update stock counts. `ConcurrentQueue<T>` handles the order processing pipeline where web requests enqueue orders while background workers dequeue them. `ConcurrentBag<T>` works well for collecting analytics events from multiple sources.

### ShopFlow-Specific Patterns

In ShopFlow, the product service uses `Dictionary<string, Product>` for the main catalog, enabling instant SKU lookups during checkout. The search feature uses `HashSet<string>` to collect unique matching product IDs before fetching full details. The checkout flow uses `Queue<ValidationStep>` to process validation rules in sequence. Customer wishlists use `List<Product>` since order matters and customers may want to see items in the order they were added.

### Performance Considerations

Choose `Dictionary` when you need fast key-based access (O(1) average). Choose `List` when you need indexed access and ordering. Choose `HashSet` when you only care about membership testing and uniqueness. Choose `SortedSet` or `SortedDictionary` when you need automatic sorting but can accept O(log n) operations. Always profile your specific use case, as the best theoretical choice may not always be the best practical choice for your data size and access patterns.