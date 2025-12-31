# C# Full-Stack Course Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Restructure the C# course from 18 modules to 26 modules with progressive ShopFlow e-commerce capstone, TDD integration, and enhanced content sections.

**Architecture:** Modify existing course.json incrementally. Create ShopFlow solution repository for capstone code. Each module enhancement includes: (1) domain example updates, (2) new section types, (3) capstone integration, (4) challenge updates.

**Tech Stack:** .NET 9, ASP.NET Core 9, Blazor United, EF Core 9, xUnit, Docker, GitHub Actions, Azure Container Apps

---

## Pre-Implementation Setup

### Task 0: Create ShopFlow Solution Repository

**Files:**
- Create: `content/courses/csharp/capstone/ShopFlow.sln`
- Create: `content/courses/csharp/capstone/src/ShopFlow.Core/ShopFlow.Core.csproj`
- Create: `content/courses/csharp/capstone/src/ShopFlow.Api/ShopFlow.Api.csproj`
- Create: `content/courses/csharp/capstone/src/ShopFlow.Web/ShopFlow.Web.csproj`
- Create: `content/courses/csharp/capstone/tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj`
- Create: `content/courses/csharp/capstone/tests/ShopFlow.Tests.Integration/ShopFlow.Tests.Integration.csproj`

**Step 1: Create solution structure**

```bash
cd content/courses/csharp
mkdir -p capstone
cd capstone
dotnet new sln -n ShopFlow
mkdir -p src/ShopFlow.Core src/ShopFlow.Api src/ShopFlow.Web
mkdir -p tests/ShopFlow.Tests.Unit tests/ShopFlow.Tests.Integration
```

**Step 2: Create Core project (domain models)**

```bash
cd src/ShopFlow.Core
dotnet new classlib -f net9.0
```

Create `src/ShopFlow.Core/Entities/Product.cs`:
```csharp
namespace ShopFlow.Core.Entities;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
```

Create `src/ShopFlow.Core/Entities/Category.cs`:
```csharp
namespace ShopFlow.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
```

**Step 3: Create remaining projects**

```bash
cd ../ShopFlow.Api
dotnet new webapi -f net9.0 --use-minimal-apis

cd ../ShopFlow.Web
dotnet new blazor -f net9.0 --interactivity Auto

cd ../../tests/ShopFlow.Tests.Unit
dotnet new xunit -f net9.0

cd ../ShopFlow.Tests.Integration
dotnet new xunit -f net9.0
```

**Step 4: Add projects to solution and references**

```bash
cd ../..
dotnet sln add src/ShopFlow.Core/ShopFlow.Core.csproj
dotnet sln add src/ShopFlow.Api/ShopFlow.Api.csproj
dotnet sln add src/ShopFlow.Web/ShopFlow.Web.csproj
dotnet sln add tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj
dotnet sln add tests/ShopFlow.Tests.Integration/ShopFlow.Tests.Integration.csproj

dotnet add src/ShopFlow.Api reference src/ShopFlow.Core
dotnet add src/ShopFlow.Web reference src/ShopFlow.Core
dotnet add tests/ShopFlow.Tests.Unit reference src/ShopFlow.Core
dotnet add tests/ShopFlow.Tests.Integration reference src/ShopFlow.Api
```

**Step 5: Verify solution builds**

```bash
dotnet build
```
Expected: Build succeeded with 0 errors.

**Step 6: Commit**

```bash
git add .
git commit -m "feat: add ShopFlow capstone solution structure"
```

---

## Phase 1: Foundation Restructure (Modules 1-7)

### Task 1: Create Content Enhancement Script

**Files:**
- Create: `scripts/enhance-lesson.js`

**Purpose:** Script to add new section types (DEEP_DIVE, REAL_WORLD, ARCHITECTURE) to existing lessons.

**Step 1: Create the enhancement script**

```javascript
// scripts/enhance-lesson.js
const fs = require('fs');

function enhanceLesson(coursePath, moduleId, lessonId, newSections) {
  const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

  const module = course.modules.find(m => m.id === moduleId);
  if (!module) throw new Error(`Module ${moduleId} not found`);

  const lesson = module.lessons.find(l => l.id === lessonId);
  if (!lesson) throw new Error(`Lesson ${lessonId} not found`);

  // Add new sections
  newSections.forEach(section => {
    const exists = lesson.contentSections.find(s => s.type === section.type);
    if (!exists) {
      lesson.contentSections.push(section);
    }
  });

  fs.writeFileSync(coursePath, JSON.stringify(course, null, 2));
  console.log(`Enhanced ${lessonId} with ${newSections.length} sections`);
}

module.exports = { enhanceLesson };
```

**Step 2: Commit**

```bash
git add scripts/enhance-lesson.js
git commit -m "feat: add lesson enhancement script"
```

---

### Task 2: Update Module 1 - Getting Started (Domain Examples)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-01 lessons)

**Step 1: Read current Module 1 content**

Use script to analyze:
```bash
node -e "
const fs = require('fs');
const c = JSON.parse(fs.readFileSync('content/courses/csharp/course.json'));
const m = c.modules.find(x => x.id === 'module-01');
m.lessons.forEach(l => {
  console.log(l.id + ': ' + l.title);
  l.contentSections.forEach(s => console.log('  - ' + s.type));
});
"
```

**Step 2: Add REAL_WORLD section to lesson-01-01 (What is Programming?)**

Add to contentSections:
```json
{
  "type": "REAL_WORLD",
  "title": "Where C# is Used in Production",
  "content": "## Real-World C# Applications\n\nC# powers some of the world's most demanding systems:\n\n**E-Commerce:** Major online retailers use ASP.NET Core for their backend APIs, handling millions of transactions daily. Companies like Stack Overflow run entirely on .NET.\n\n**Gaming:** Unity, the world's most popular game engine, uses C# as its primary scripting language. Games from indie titles to AAA productions are built with C#.\n\n**Enterprise Software:** Banks, healthcare systems, and government agencies rely on C# for mission-critical applications where reliability and security are paramount.\n\n**Cloud Services:** Microsoft Azure's own services are built with .NET, and Azure provides first-class support for deploying C# applications.\n\n**In this course**, you'll build ShopFlow—a complete e-commerce platform—using the same technologies and patterns used by professional developers at companies worldwide."
}
```

**Step 3: Add DEEP_DIVE section to lesson-01-02 (What is .NET and CLR?)**

Add to contentSections:
```json
{
  "type": "DEEP_DIVE",
  "title": "Inside the CLR: How Your Code Actually Runs",
  "content": "## The Compilation Pipeline\n\nWhen you run `dotnet build`, your C# code goes through multiple transformations:\n\n**1. Roslyn Compiler (C# → IL)**\nThe Roslyn compiler parses your C# code and converts it to Intermediate Language (IL), also called MSIL or CIL. IL is a platform-independent bytecode stored in .dll files.\n\n**2. Just-In-Time Compilation (IL → Machine Code)**\nWhen your app runs, the CLR's JIT compiler converts IL to native machine code for your specific CPU. .NET 9's Dynamic PGO (Profile-Guided Optimization) makes this smarter by optimizing based on actual runtime behavior.\n\n**3. Native AOT (Alternative Path)**\n.NET 9 also supports Ahead-of-Time compilation, which compiles directly to native code at build time. This eliminates JIT startup cost but produces larger binaries.\n\n**Why This Matters**\nUnderstanding the compilation pipeline helps you:\n- Debug assembly-related errors\n- Optimize startup time vs runtime performance\n- Choose between JIT and AOT for deployment\n\n**Viewing IL Code**\nYou can inspect IL using `dotnet tool install -g ilspycmd` then `ilspycmd YourApp.dll`."
}
```

**Step 4: Update examples to reference ShopFlow**

In lesson-01-01, update the EXAMPLE content to include:
```csharp
// Welcome to ShopFlow - the e-commerce app you'll build!
Console.WriteLine("Welcome to ShopFlow!");
Console.WriteLine("Your journey to full-stack .NET developer starts here.");

// Throughout this course, you'll build:
Console.WriteLine("- A product catalog API");
Console.WriteLine("- A shopping cart system");
Console.WriteLine("- User authentication");
Console.WriteLine("- A Blazor storefront");
Console.WriteLine("- Cloud deployment to Azure");
```

**Step 5: Save changes to course.json**

**Step 6: Validate JSON**

```bash
node -e "JSON.parse(require('fs').readFileSync('content/courses/csharp/course.json')); console.log('Valid JSON')"
```
Expected: "Valid JSON"

**Step 7: Commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): enhance module-01 with REAL_WORLD and DEEP_DIVE sections"
```

---

### Task 3: Update Module 2 - Variables and Data Types (E-commerce Domain)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-02 lessons)

**Step 1: Update lesson examples to use ShopFlow domain**

Replace generic examples with:
```csharp
// Product information
string productName = "Wireless Headphones";
decimal price = 79.99m;  // Always use decimal for money!
int stockQuantity = 150;
bool isAvailable = stockQuantity > 0;

// Customer data
string customerEmail = "alex@example.com";
int loyaltyPoints = 2500;

// Order calculations
int quantity = 3;
decimal subtotal = price * quantity;  // 239.97
decimal taxRate = 0.08m;
decimal tax = subtotal * taxRate;     // 19.20
decimal total = subtotal + tax;       // 259.17

Console.WriteLine($"Order Total: {total:C}");  // $259.17
```

**Step 2: Add ARCHITECTURE section to types lesson**

```json
{
  "type": "ARCHITECTURE",
  "title": "Choosing the Right Type for the Job",
  "content": "## Type Selection in Real Applications\n\n**Money: Always `decimal`**\nNever use `float` or `double` for financial calculations. They use binary floating-point which can't precisely represent values like 0.10. The `decimal` type uses base-10 and is designed for financial accuracy.\n\n```csharp\n// WRONG: Binary floating-point errors\ndouble badPrice = 0.1 + 0.2;  // 0.30000000000000004\n\n// CORRECT: Decimal precision\ndecimal goodPrice = 0.1m + 0.2m;  // 0.3\n```\n\n**IDs: `int` vs `long` vs `Guid`**\n- `int`: Up to 2.1 billion records, common for most apps\n- `long`: For systems expecting billions of records\n- `Guid`: When you need globally unique IDs across distributed systems\n\n**Strings: Immutability Matters**\nStrings are immutable—every modification creates a new string. For building strings in loops, use `StringBuilder` to avoid memory pressure.\n\n**In ShopFlow**, we use:\n- `decimal` for all prices and money\n- `int` for entity IDs (sufficient for our scale)\n- `string` for names, descriptions\n- `DateTime` for timestamps (UTC always!)"
}
```

**Step 3: Update challenges to use ShopFlow context**

Update challenge instructions to reference products, orders, customers instead of generic examples.

**Step 4: Validate and commit**

```bash
node -e "JSON.parse(require('fs').readFileSync('content/courses/csharp/course.json')); console.log('Valid JSON')"
git add content/courses/csharp/course.json
git commit -m "feat(csharp): update module-02 with e-commerce domain examples"
```

---

### Task 4: Update Module 3 - Control Flow (Cart Validation Logic)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-03 lessons)

**Step 1: Update if/else examples to cart validation**

```csharp
// Shopping cart validation
decimal cartTotal = 150.00m;
bool hasPromoCode = true;
string promoCode = "SAVE20";

// Apply discount logic
decimal discount = 0m;

if (cartTotal >= 100m && hasPromoCode)
{
    if (promoCode == "SAVE20")
    {
        discount = cartTotal * 0.20m;  // 20% off
        Console.WriteLine($"Promo applied! You save: {discount:C}");
    }
    else if (promoCode == "SAVE10")
    {
        discount = cartTotal * 0.10m;  // 10% off
        Console.WriteLine($"Promo applied! You save: {discount:C}");
    }
}
else if (cartTotal >= 200m)
{
    // Automatic discount for large orders
    discount = cartTotal * 0.05m;
    Console.WriteLine($"Bulk discount applied! You save: {discount:C}");
}
else
{
    Console.WriteLine("No discount applicable.");
}

decimal finalTotal = cartTotal - discount;
Console.WriteLine($"Final total: {finalTotal:C}");
```

**Step 2: Add pattern matching for order status**

```csharp
// Order status handling with pattern matching
string orderStatus = "Shipped";

string message = orderStatus switch
{
    "Pending" => "Your order is being processed.",
    "Confirmed" => "Your order has been confirmed!",
    "Shipped" => "Your order is on its way!",
    "Delivered" => "Your order has been delivered.",
    "Cancelled" => "Your order was cancelled.",
    _ => "Unknown order status."
};

Console.WriteLine(message);
```

**Step 3: Add REAL_WORLD section on validation patterns**

```json
{
  "type": "REAL_WORLD",
  "title": "Validation in Production E-commerce Systems",
  "content": "## Real-World Validation Patterns\n\n**Defense in Depth**\nNever trust user input. Validate at multiple layers:\n1. Client-side (UX, not security)\n2. API controller (Data Annotations)\n3. Service layer (business rules)\n4. Database (constraints)\n\n**Early Returns (Guard Clauses)**\nProfessional code validates preconditions first:\n```csharp\npublic decimal CalculateDiscount(Order order)\n{\n    if (order is null) throw new ArgumentNullException(nameof(order));\n    if (order.Items.Count == 0) return 0m;\n    if (order.Total < 50m) return 0m;\n    \n    // Main logic here\n    return order.Total * 0.10m;\n}\n```\n\n**Fail Fast Principle**\nDetect and report errors as early as possible. A validation error caught at the API layer is better than a database constraint violation.\n\n**In ShopFlow**, we'll implement a multi-layer validation strategy using Data Annotations, FluentValidation, and domain validation in the service layer."
}
```

**Step 4: Validate and commit**

```bash
node -e "JSON.parse(require('fs').readFileSync('content/courses/csharp/course.json')); console.log('Valid JSON')"
git add content/courses/csharp/course.json
git commit -m "feat(csharp): update module-03 with cart validation examples"
```

---

### Task 5: Update Module 4 - Loops (Inventory Processing)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-04 lessons)

**Step 1: Update loop examples to inventory processing**

```csharp
// Product inventory processing
List<(string Name, int Stock, decimal Price)> products = new()
{
    ("Laptop", 25, 999.99m),
    ("Headphones", 150, 79.99m),
    ("Keyboard", 0, 49.99m),
    ("Mouse", 75, 29.99m),
    ("Monitor", 10, 299.99m)
};

// Find low stock items (foreach)
Console.WriteLine("Low Stock Alert (under 20 units):");
foreach (var product in products)
{
    if (product.Stock < 20)
    {
        Console.WriteLine($"  {product.Name}: {product.Stock} remaining");
    }
}

// Calculate total inventory value (for)
decimal totalValue = 0m;
for (int i = 0; i < products.Count; i++)
{
    totalValue += products[i].Stock * products[i].Price;
    Console.WriteLine($"Running total after {products[i].Name}: {totalValue:C}");
}
Console.WriteLine($"Total inventory value: {totalValue:C}");

// Process orders until stock depleted (while)
int keyboardStock = 50;
int ordersProcessed = 0;
while (keyboardStock > 0)
{
    int orderQty = Random.Shared.Next(1, 6);  // Random order 1-5
    if (orderQty <= keyboardStock)
    {
        keyboardStock -= orderQty;
        ordersProcessed++;
        Console.WriteLine($"Order #{ordersProcessed}: {orderQty} keyboards. Stock remaining: {keyboardStock}");
    }
    else
    {
        Console.WriteLine($"Cannot fulfill order of {orderQty}. Only {keyboardStock} in stock.");
        break;
    }
}
```

**Step 2: Add DEEP_DIVE on loop performance**

```json
{
  "type": "DEEP_DIVE",
  "title": "Loop Performance: What Actually Happens",
  "content": "## Understanding Loop Performance\n\n**Array vs List Iteration**\n```csharp\n// Arrays: Direct memory access, fastest\nint[] array = new int[1000];\nforeach (var item in array) { }  // JIT optimizes bounds checks away\n\n// List<T>: One level of indirection\nList<int> list = new(1000);\nforeach (var item in list) { }  // Slightly slower, usually negligible\n```\n\n**for vs foreach**\nIn most cases, `foreach` is just as fast as `for` and more readable. The JIT compiler optimizes common patterns. Use `for` when you need the index.\n\n**LINQ vs Loops**\n```csharp\n// LINQ: Allocates iterator objects\nvar total = products.Where(p => p.Stock > 0).Sum(p => p.Price);\n\n// Loop: No allocations\ndecimal total = 0m;\nforeach (var p in products) if (p.Stock > 0) total += p.Price;\n```\nFor hot paths processing millions of items, the loop may be 2-3x faster. For typical app code, LINQ's readability wins.\n\n**Span<T> for Ultimate Performance**\n.NET's `Span<T>` provides array-like performance without allocations. Used extensively in ASP.NET Core's request pipeline."
}
```

**Step 3: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): update module-04 with inventory processing examples"
```

---

### Task 6: Update Module 5 - Collections (Product Catalogs)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-05 lessons)

**Step 1: Update collection examples to product catalogs**

```csharp
// Product catalog with Dictionary
Dictionary<int, Product> catalog = new()
{
    [1] = new Product { Id = 1, Name = "Laptop", Price = 999.99m, CategoryId = 1 },
    [2] = new Product { Id = 2, Name = "Headphones", Price = 79.99m, CategoryId = 2 },
    [3] = new Product { Id = 3, Name = "Keyboard", Price = 49.99m, CategoryId = 2 }
};

// Fast lookup by ID - O(1)
if (catalog.TryGetValue(2, out var product))
{
    Console.WriteLine($"Found: {product.Name} at {product.Price:C}");
}

// Shopping cart with List
List<CartItem> cart = new();
cart.Add(new CartItem { ProductId = 1, Quantity = 1 });
cart.Add(new CartItem { ProductId = 2, Quantity = 2 });

// Category tags with HashSet (no duplicates)
HashSet<string> tags = new() { "Electronics", "Audio", "Gaming" };
tags.Add("Electronics");  // No effect - already exists
Console.WriteLine($"Unique tags: {tags.Count}");  // 3

// Recently viewed with Queue (FIFO)
Queue<int> recentlyViewed = new();
recentlyViewed.Enqueue(1);  // Viewed laptop
recentlyViewed.Enqueue(2);  // Viewed headphones
recentlyViewed.Enqueue(3);  // Viewed keyboard
if (recentlyViewed.Count > 5)
{
    recentlyViewed.Dequeue();  // Remove oldest
}
```

**Step 2: Add ARCHITECTURE section on collection choice**

```json
{
  "type": "ARCHITECTURE",
  "title": "Choosing the Right Collection",
  "content": "## Collection Selection Guide\n\n| Scenario | Collection | Why |\n|----------|------------|-----|\n| Product catalog lookup | `Dictionary<int, Product>` | O(1) lookup by ID |\n| Shopping cart items | `List<CartItem>` | Ordered, indexed access |\n| Unique category tags | `HashSet<string>` | Automatic deduplication |\n| Recently viewed | `Queue<T>` | FIFO ordering |\n| Undo history | `Stack<T>` | LIFO ordering |\n| Sorted leaderboard | `SortedSet<T>` | Auto-sorted unique items |\n\n**Immutable Collections**\nFor thread-safe scenarios or when you want to prevent modifications:\n```csharp\nusing System.Collections.Immutable;\nvar immutableProducts = catalog.Values.ToImmutableList();\n```\n\n**Concurrent Collections**\nFor multi-threaded access without explicit locking:\n```csharp\nusing System.Collections.Concurrent;\nConcurrentDictionary<int, Product> threadSafeCatalog = new();\n```\n\n**In ShopFlow**, we use:\n- `Dictionary` for in-memory caches\n- `List` for entity collections (backed by EF Core)\n- `HashSet` for permission checks\n- `ConcurrentDictionary` for rate limiting"
}
```

**Step 3: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): update module-05 with product catalog examples"
```

---

### Task 7: Update Module 6 - Methods (ShopFlow Services)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-06 lessons)

**Step 1: Update method examples to service patterns**

```csharp
// ShopFlow pricing service methods

/// <summary>
/// Calculates the final price after applying applicable discounts.
/// </summary>
public static decimal CalculateFinalPrice(
    decimal basePrice,
    int quantity,
    decimal discountPercent = 0m,
    bool isMember = false)
{
    // Validate inputs
    ArgumentOutOfRangeException.ThrowIfNegative(basePrice);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

    decimal subtotal = basePrice * quantity;

    // Apply percentage discount
    decimal discount = subtotal * (discountPercent / 100m);

    // Members get additional 5% off
    if (isMember)
    {
        discount += subtotal * 0.05m;
    }

    return subtotal - discount;
}

// Usage examples:
decimal price1 = CalculateFinalPrice(99.99m, 2);  // No discount
decimal price2 = CalculateFinalPrice(99.99m, 2, discountPercent: 10m);  // 10% off
decimal price3 = CalculateFinalPrice(99.99m, 2, isMember: true);  // Member discount

// Expression-bodied method
public static bool IsInStock(int quantity) => quantity > 0;

// Tuple return for multiple values
public static (decimal Subtotal, decimal Tax, decimal Total) CalculateOrderTotals(
    decimal subtotal,
    decimal taxRate = 0.08m)
{
    decimal tax = subtotal * taxRate;
    decimal total = subtotal + tax;
    return (subtotal, tax, total);
}

var (sub, tax, total) = CalculateOrderTotals(199.99m);
Console.WriteLine($"Subtotal: {sub:C}, Tax: {tax:C}, Total: {total:C}");
```

**Step 2: Add REAL_WORLD section on method design**

```json
{
  "type": "REAL_WORLD",
  "title": "Method Design in Production Code",
  "content": "## Professional Method Design\n\n**Single Responsibility**\nEach method should do one thing well. If you're using 'and' to describe it, split it.\n\n```csharp\n// BAD: Does too much\npublic void ProcessOrderAndSendEmailAndUpdateInventory(Order order) { }\n\n// GOOD: Single responsibility\npublic void ProcessOrder(Order order) { }\npublic void SendOrderConfirmation(Order order) { }\npublic void UpdateInventory(Order order) { }\n```\n\n**Method Length**\nIf a method doesn't fit on one screen (~30 lines), consider extracting helper methods. Long methods are hard to test and maintain.\n\n**Parameter Count**\nMore than 3-4 parameters is a code smell. Consider:\n- Using a parameter object: `CreateOrder(OrderRequest request)`\n- Using a builder pattern for complex construction\n\n**Return Early (Guard Clauses)**\n```csharp\npublic decimal GetDiscount(Customer customer)\n{\n    if (customer is null) return 0m;\n    if (!customer.IsActive) return 0m;\n    if (customer.Orders.Count < 5) return 0m;\n    \n    return customer.LoyaltyTier switch\n    {\n        \"Gold\" => 0.15m,\n        \"Silver\" => 0.10m,\n        _ => 0.05m\n    };\n}\n```\n\n**In ShopFlow**, all service methods follow these patterns with XML documentation for public APIs."
}
```

**Step 3: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): update module-06 with ShopFlow service examples"
```

---

### Task 8: Update Module 7 - OOP Basics (ShopFlow Domain Model)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-07 lessons)
- Reference: `content/courses/csharp/capstone/src/ShopFlow.Core/Entities/`

**Step 1: Update class examples to ShopFlow entities**

```csharp
// ShopFlow domain model

public class Product
{
    // Properties with validation
    public int Id { get; private set; }

    private string _name = string.Empty;
    public required string Name
    {
        get => _name;
        set => _name = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Name cannot be empty");
    }

    public required string Description { get; set; }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set => _price = value >= 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(Price));
    }

    public int StockQuantity { get; private set; }

    // Behavior methods
    public bool IsInStock => StockQuantity > 0;

    public void AddStock(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        StockQuantity += quantity;
    }

    public void RemoveStock(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        if (quantity > StockQuantity)
            throw new InvalidOperationException("Insufficient stock");
        StockQuantity -= quantity;
    }
}

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Navigation property
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        _products.Add(product);
    }
}
```

**Step 2: Add ARCHITECTURE section on domain modeling**

```json
{
  "type": "ARCHITECTURE",
  "title": "Domain-Driven Design Principles",
  "content": "## Modeling Real Business Concepts\n\n**Rich Domain Models vs Anemic Models**\n\n*Anemic Model (avoid):*\n```csharp\npublic class Product\n{\n    public int Id { get; set; }\n    public string Name { get; set; }\n    public int Stock { get; set; }  // Anyone can set to -5!\n}\n```\n\n*Rich Domain Model (prefer):*\n```csharp\npublic class Product\n{\n    public int Stock { get; private set; }  // Controlled access\n    \n    public void RemoveStock(int qty)  // Enforces business rules\n    {\n        if (qty > Stock) throw new InvalidOperationException();\n        Stock -= qty;\n    }\n}\n```\n\n**Encapsulation Guards Invariants**\nBusiness rules should be enforced by the domain model itself, not scattered across services. A `Product` should never have negative stock—the class prevents it.\n\n**Entity vs Value Object**\n- *Entity*: Has identity (Product with Id=5 is specific)\n- *Value Object*: Defined by values (Money, Address—two $10 amounts are equal)\n\n**In ShopFlow**, we use:\n- Entities: `Product`, `Category`, `Order`, `Customer`\n- Value Objects: `Money`, `Address`, `OrderStatus`\n- The domain layer has ZERO dependencies on infrastructure"
}
```

**Step 3: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): update module-07 with ShopFlow domain model"
```

---

## Phase 2: Expand Testing Modules (14-16)

### Task 9: Expand Module 15 - Unit Testing (Currently 3 lessons → 8 lessons)

**Files:**
- Modify: `content/courses/csharp/course.json` (module-15)

**Step 1: Audit existing testing content**

Current lessons:
1. xUnit Testing Fundamentals
2. Mocking Dependencies
3. Integration Testing & Test Organization

**Step 2: Add new lessons for TDD workflow**

Add 5 new lessons:

**Lesson 15-04: Test-Driven Development Workflow**
```json
{
  "id": "lesson-15-04",
  "title": "Test-Driven Development (Red-Green-Refactor)",
  "moduleId": "module-15",
  "order": 4,
  "estimatedMinutes": 30,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Understanding TDD",
      "content": "Imagine building a house by first creating a detailed blueprint (the test), then constructing to match the blueprint (implementation), then improving the design (refactor). TDD is 'blueprint-first' development.\n\nThe cycle is:\n1. **RED**: Write a failing test for behavior you want\n2. **GREEN**: Write the minimum code to pass the test\n3. **REFACTOR**: Improve the code while keeping tests green\n\nThis feels backwards at first, but it produces better-designed, more maintainable code."
    },
    {
      "type": "EXAMPLE",
      "title": "TDD in Action: ShopFlow Cart Total",
      "content": "Let's build a cart total calculator using TDD.",
      "code": "// STEP 1: RED - Write the failing test first\n[Fact]\npublic void CalculateTotal_WithTwoItems_ReturnsCorrectSum()\n{\n    // Arrange\n    var calculator = new CartCalculator();\n    var items = new List<CartItem>\n    {\n        new() { Price = 10.00m, Quantity = 2 },  // $20\n        new() { Price = 5.50m, Quantity = 3 }    // $16.50\n    };\n    \n    // Act\n    var total = calculator.CalculateTotal(items);\n    \n    // Assert\n    Assert.Equal(36.50m, total);\n}\n\n// Run test: dotnet test\n// Result: FAIL - CartCalculator doesn't exist yet!\n\n// STEP 2: GREEN - Write minimum code to pass\npublic class CartCalculator\n{\n    public decimal CalculateTotal(List<CartItem> items)\n    {\n        return items.Sum(i => i.Price * i.Quantity);\n    }\n}\n\n// Run test: dotnet test\n// Result: PASS!\n\n// STEP 3: REFACTOR - Improve without changing behavior\npublic decimal CalculateTotal(IEnumerable<CartItem> items)\n{\n    ArgumentNullException.ThrowIfNull(items);\n    return items.Sum(item => item.LineTotal);  // Use property\n}\n\n// Run test: dotnet test\n// Result: Still PASS - safe to refactor!",
      "language": "csharp"
    },
    {
      "type": "THEORY",
      "title": "Why TDD Works",
      "content": "## Benefits of Test-First Development\n\n**1. Design Feedback**\nIf code is hard to test, it's hard to use. TDD forces good design:\n- Small, focused functions\n- Clear dependencies (easy to mock)\n- Well-defined interfaces\n\n**2. Executable Specification**\nTests document exactly what the code should do. Unlike comments, tests can't lie—they either pass or fail.\n\n**3. Confidence to Refactor**\nWith comprehensive tests, you can improve code structure knowing you'll catch any regressions immediately.\n\n**4. Reduced Debugging Time**\nBugs are caught immediately when tests fail. No hunting through logs wondering what broke.\n\n## The TDD Mantra\n\n*\"Write a test. Watch it fail. Make it pass. Refactor. Repeat.\"*\n\nEach cycle should take 2-5 minutes. If longer, you're taking too big a step."
    },
    {
      "type": "WARNING",
      "title": "Common TDD Mistakes",
      "content": "## Pitfalls to Avoid\n\n**Writing too much test at once**\nOne assertion per test. Test one behavior at a time.\n\n**Skipping the RED step**\nAlways see the test fail first! A test that never fails might have a bug.\n\n**Over-engineering in GREEN**\nWrite the *minimum* code to pass. Resist adding 'just in case' logic.\n\n**Skipping REFACTOR**\nGreen doesn't mean done. Clean up before moving on.\n\n**Testing implementation, not behavior**\n```csharp\n// BAD: Tests internal implementation\nAssert.True(calculator._cache.ContainsKey(itemId));\n\n// GOOD: Tests observable behavior\nvar result = calculator.GetItem(itemId);\nAssert.NotNull(result);\n```"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "lesson-15-04-challenge-01",
      "title": "TDD: Discount Calculator",
      "description": "Use TDD to build a discount calculator.",
      "instructions": "Follow the TDD cycle to implement a discount calculator:\n\n1. Write a failing test for: 10% discount on orders over $100\n2. Implement the minimum code to pass\n3. Write a test for: No discount on orders under $100\n4. Implement to pass both tests\n5. Refactor if needed\n\nYour DiscountCalculator should have a method:\n`decimal CalculateDiscount(decimal orderTotal)`",
      "starterCode": "// Test file: DiscountCalculatorTests.cs\nusing Xunit;\n\npublic class DiscountCalculatorTests\n{\n    // TODO: Write your first failing test here\n    [Fact]\n    public void CalculateDiscount_OrderOver100_Returns10Percent()\n    {\n        // Arrange\n        \n        // Act\n        \n        // Assert\n    }\n}",
      "solution": "// Tests\npublic class DiscountCalculatorTests\n{\n    [Fact]\n    public void CalculateDiscount_OrderOver100_Returns10Percent()\n    {\n        var calc = new DiscountCalculator();\n        var discount = calc.CalculateDiscount(150.00m);\n        Assert.Equal(15.00m, discount);\n    }\n    \n    [Fact]\n    public void CalculateDiscount_OrderUnder100_ReturnsZero()\n    {\n        var calc = new DiscountCalculator();\n        var discount = calc.CalculateDiscount(50.00m);\n        Assert.Equal(0m, discount);\n    }\n    \n    [Theory]\n    [InlineData(100.00, 10.00)]  // Boundary\n    [InlineData(99.99, 0)]       // Just under\n    public void CalculateDiscount_BoundaryValues(decimal total, decimal expected)\n    {\n        var calc = new DiscountCalculator();\n        Assert.Equal(expected, calc.CalculateDiscount(total));\n    }\n}\n\n// Implementation\npublic class DiscountCalculator\n{\n    private const decimal DiscountThreshold = 100m;\n    private const decimal DiscountRate = 0.10m;\n    \n    public decimal CalculateDiscount(decimal orderTotal)\n    {\n        if (orderTotal < DiscountThreshold)\n            return 0m;\n        return orderTotal * DiscountRate;\n    }\n}",
      "language": "csharp",
      "testCases": [
        {"id": "test-1", "description": "Implements DiscountCalculator class", "expectedOutput": "DiscountCalculator", "isVisible": true},
        {"id": "test-2", "description": "Has at least 2 test methods", "expectedOutput": "[Fact]", "isVisible": true}
      ],
      "hints": [
        {"level": 1, "text": "Start with the test: new DiscountCalculator() and call CalculateDiscount(150m)"},
        {"level": 2, "text": "Assert.Equal(15.00m, discount) for 10% of $150"},
        {"level": 3, "text": "For boundary testing, use [Theory] with [InlineData] for multiple test cases"}
      ],
      "difficulty": "intermediate"
    }
  ]
}
```

**Step 3: Add lessons 15-05 through 15-08**

- 15-05: Testing Async Code
- 15-06: Testing Edge Cases and Exceptions
- 15-07: Code Coverage and When It Matters
- 15-08: Testing ShopFlow Services (Capstone Integration)

**Step 4: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): expand module-15 with TDD lessons"
```

---

## Phase 3: Add New Authentication Modules (20-22)

### Task 10: Create Module 20 - Authentication Fundamentals

**Files:**
- Modify: `content/courses/csharp/course.json` (add new module-20)

**Step 1: Create new authentication module structure**

```json
{
  "id": "module-20",
  "title": "Authentication Fundamentals",
  "description": "Secure your applications with ASP.NET Core Identity, cookies, and JWT tokens. Learn the complete authentication story for web applications and APIs.",
  "difficulty": "intermediate",
  "estimatedHours": 4,
  "lessons": [
    {
      "id": "lesson-20-01",
      "title": "Authentication vs Authorization (The Security Guard)",
      "moduleId": "module-20",
      "order": 1,
      "estimatedMinutes": 25,
      "difficulty": "intermediate",
      "contentSections": [
        {
          "type": "ANALOGY",
          "title": "Understanding the Difference",
          "content": "Imagine a secure office building:\n\n**Authentication** = The security guard checking your ID badge at the entrance. \"Who are you? Prove it.\"\n\n**Authorization** = The access control system deciding which floors you can visit. \"You're verified as John, but can John access the executive floor?\"\n\nAuthentication answers: *\"Who is this person?\"*\nAuthorization answers: *\"What can this person do?\"*\n\nYou must authenticate first, then authorize. You can't decide what someone can access until you know who they are."
        },
        {
          "type": "EXAMPLE",
          "title": "Auth in ASP.NET Core",
          "content": "Here's how authentication and authorization work together in ASP.NET Core.",
          "code": "// Program.cs - Setting up auth\nvar builder = WebApplication.CreateBuilder(args);\n\n// Add authentication (WHO are you?)\nbuilder.Services.AddAuthentication(options =>\n{\n    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;\n})\n.AddCookie()  // Cookie-based auth for web apps\n.AddJwtBearer();  // JWT for APIs\n\n// Add authorization (WHAT can you do?)\nbuilder.Services.AddAuthorization(options =>\n{\n    options.AddPolicy(\"AdminOnly\", policy => \n        policy.RequireRole(\"Admin\"));\n    options.AddPolicy(\"CanEditProducts\", policy =>\n        policy.RequireClaim(\"Permission\", \"Products.Edit\"));\n});\n\nvar app = builder.Build();\n\n// Middleware order matters!\napp.UseAuthentication();  // First: identify the user\napp.UseAuthorization();   // Then: check permissions\n\n// Protecting an endpoint\napp.MapGet(\"/admin/dashboard\", () => \"Admin Dashboard\")\n   .RequireAuthorization(\"AdminOnly\");\n\napp.MapGet(\"/products/edit/{id}\", (int id) => $\"Edit product {id}\")\n   .RequireAuthorization(\"CanEditProducts\");",
          "language": "csharp"
        },
        {
          "type": "THEORY",
          "title": "Authentication Schemes",
          "content": "## Common Authentication Methods\n\n**Cookie Authentication**\n- Best for: Server-rendered apps (MVC, Razor Pages, Blazor Server)\n- How it works: Server creates encrypted cookie after login, browser sends it with each request\n- Pros: Simple, secure (HttpOnly, Secure flags), automatic CSRF protection\n- Cons: Doesn't work for mobile apps or cross-domain APIs\n\n**JWT (JSON Web Tokens)**\n- Best for: APIs consumed by SPAs, mobile apps, microservices\n- How it works: Server issues signed token, client includes in Authorization header\n- Pros: Stateless, works across domains, self-contained claims\n- Cons: Token revocation is complex, larger request size\n\n**OAuth 2.0 / OpenID Connect**\n- Best for: \"Sign in with Google/Microsoft/GitHub\"\n- How it works: Delegates authentication to trusted provider\n- Pros: No password storage, user convenience\n- Cons: Dependency on external provider\n\n**In ShopFlow**, we use:\n- Cookies for the Blazor storefront\n- JWT for the public API\n- OAuth for social login options"
        },
        {
          "type": "ARCHITECTURE",
          "title": "Security Architecture Decisions",
          "content": "## Designing Secure Authentication\n\n**Defense in Depth**\nNever rely on a single security layer:\n1. HTTPS everywhere (transport security)\n2. Secure password hashing (Identity uses PBKDF2)\n3. Account lockout after failed attempts\n4. Multi-factor authentication for sensitive operations\n\n**Token Storage**\n```\n| Storage       | XSS Safe? | CSRF Safe? | Best For      |\n|---------------|-----------|------------|---------------|\n| HttpOnly Cookie | Yes     | Need token | Web apps      |\n| localStorage    | No      | Yes        | Avoid!        |\n| Memory only     | Yes     | Yes        | High security |\n```\n\n**Session Management**\n- Set reasonable expiration times\n- Implement proper logout (invalidate tokens)\n- Rotate tokens on privilege escalation\n- Track active sessions per user\n\n**In ShopFlow**, authentication is handled by a dedicated `AuthService` in the application layer, with Identity managing user storage in infrastructure."
        }
      ]
    }
  ]
}
```

**Step 2: Add remaining lessons 20-02 through 20-06**

- 20-02: ASP.NET Core Identity Setup
- 20-03: User Registration and Login
- 20-04: Cookie Authentication for Blazor
- 20-05: JWT Tokens for API Authentication
- 20-06: Refresh Tokens and Token Lifecycle

**Step 3: Validate and commit**

```bash
git add content/courses/csharp/course.json
git commit -m "feat(csharp): add module-20 authentication fundamentals"
```

---

### Task 11: Create Module 21 - External Authentication Providers

*(Similar structure - OAuth, Google, Microsoft, GitHub integration)*

### Task 12: Create Module 22 - Authorization Patterns

*(Similar structure - roles, claims, policies, resource-based)*

---

## Phase 4: Add Clean Architecture Module (18)

### Task 13: Restructure and Create Module 18 - Clean Architecture

**Files:**
- Modify: `content/courses/csharp/course.json` (reorganize module-18)

**Current module-18:** Modern API Development with OpenAPI & Scalar

**Action:** Renumber existing module-18 to module-19, insert new Clean Architecture as module-18.

**Step 1: Create Clean Architecture module**

Lessons:
- 18-01: Why Architecture Matters
- 18-02: Layers: Domain, Application, Infrastructure, Presentation
- 18-03: Domain Layer: Entities and Value Objects
- 18-04: Application Layer: Use Cases and Interfaces
- 18-05: Infrastructure Layer: Implementations
- 18-06: Refactoring ShopFlow to Clean Architecture
- 18-07: CQRS Introduction (Optional Pattern)

*(Full lesson content follows same pattern as above examples)*

---

## Phase 5: Add CI/CD Module (24)

### Task 14: Create Module 24 - CI/CD with GitHub Actions

**Files:**
- Modify: `content/courses/csharp/course.json` (add new module-24)
- Create: `content/courses/csharp/capstone/.github/workflows/ci.yml`

**Step 1: Create GitHub Actions workflow for ShopFlow**

```yaml
# .github/workflows/ci.yml
name: ShopFlow CI

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  build-and-test:
    runs-on: ubuntu-latest

    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_USER: shopflow
          POSTGRES_PASSWORD: shopflow_test
          POSTGRES_DB: shopflow_test
        ports:
          - 5432:5432
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    steps:
      - uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'

      - name: Restore dependencies
        run: dotnet restore

      - name: Build
        run: dotnet build --no-restore --configuration Release

      - name: Run unit tests
        run: dotnet test tests/ShopFlow.Tests.Unit --no-build --configuration Release --verbosity normal

      - name: Run integration tests
        run: dotnet test tests/ShopFlow.Tests.Integration --no-build --configuration Release --verbosity normal
        env:
          ConnectionStrings__DefaultConnection: "Host=localhost;Database=shopflow_test;Username=shopflow;Password=shopflow_test"

      - name: Publish coverage report
        uses: codecov/codecov-action@v4
        if: github.event_name == 'push'
```

**Step 2: Create CI/CD lessons**

- 24-01: What is CI/CD?
- 24-02: GitHub Actions Fundamentals
- 24-03: Build and Test Pipeline
- 24-04: Code Quality Gates
- 24-05: Building and Pushing Docker Images
- 24-06: Environment-Based Deployments

---

## Phase 6: Final Capstone Module (26)

### Task 15: Create Module 26 - ShopFlow Capstone Completion

**Step 1: Create capstone integration lessons**

- 26-01: Feature: Inventory Management
- 26-02: Feature: Order Processing Workflow
- 26-03: Feature: Payment Integration (Stripe Test)
- 26-04: Feature: Search with Filtering & Pagination
- 26-05: Performance Optimization
- 26-06: Security Hardening
- 26-07: Final Production Deployment
- 26-08: Course Retrospective

---

## Implementation Tracking

| Phase | Modules | Tasks | Status |
|-------|---------|-------|--------|
| Setup | - | Task 0: ShopFlow Solution | ⬜ |
| Phase 1 | 1-7 | Tasks 1-8: Foundation Restructure | ⬜ |
| Phase 2 | 14-16 | Task 9: Expand Testing | ⬜ |
| Phase 3 | 20-22 | Tasks 10-12: Add Auth Modules | ⬜ |
| Phase 4 | 18 | Task 13: Clean Architecture | ⬜ |
| Phase 5 | 24 | Task 14: CI/CD Module | ⬜ |
| Phase 6 | 26 | Task 15: Capstone Completion | ⬜ |

---

## Quality Checklist Per Task

Before marking a task complete:

- [ ] All code examples compile on .NET 9
- [ ] Challenge solutions are complete and tested
- [ ] No TODOs, stubs, or placeholders
- [ ] ANALOGY section uses relatable comparison
- [ ] EXAMPLE section has complete, runnable code
- [ ] THEORY section fully explains syntax/concepts
- [ ] WARNING section covers real mistakes
- [ ] New section types (DEEP_DIVE, REAL_WORLD, ARCHITECTURE) where appropriate
- [ ] JSON validates: `node -e "JSON.parse(require('fs').readFileSync('...'))`
- [ ] Git commit with conventional commit message

---

## Estimated Effort

| Phase | New Lessons | Modified Lessons | Est. Hours |
|-------|-------------|------------------|------------|
| Setup | 0 | 0 | 2 |
| Phase 1 | 0 | 42 | 20 |
| Phase 2 | 5 | 3 | 8 |
| Phase 3 | 17 | 0 | 15 |
| Phase 4 | 7 | 0 | 8 |
| Phase 5 | 6 | 0 | 6 |
| Phase 6 | 8 | 0 | 10 |
| **Total** | **43** | **45** | **69 hours** |

---

*Plan created: 2025-01-01*
*Ready for execution with superpowers:executing-plans or superpowers:subagent-driven-development*
