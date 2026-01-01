---
type: "ANALOGY"
title: "Understanding the Concept"
---

MIGRATIONS = Version control for your database schema!

Imagine building a house:
• Version 1: Foundation only
• Version 2: Add walls
• Version 3: Add roof
• Version 4: Add windows

Each step is a MIGRATION:
• Records what changed
• Can go forward (apply) or backward (rollback)
• Track schema evolution in code!

BULK OPERATIONS (NEW in EF Core 7/8):
OLD way (slow):
```csharp
foreach (var product in products)
{
    product.Price *= 1.1;  // 10% increase
}
context.SaveChanges();  // Loads each, updates each - SLOW!
```

NEW way (fast):
```csharp
context.Products
    .Where(p => p.Category == "Electronics")
    .ExecuteUpdate(p => p.SetProperty(x => x.Price, x => x.Price * 1.1));
// Single UPDATE statement - FAST!
```

Think: Migrations = 'Git for database schema', Bulk = 'Update 1000 rows in one SQL statement!'