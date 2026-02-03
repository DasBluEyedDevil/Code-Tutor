---
type: "THEORY"
title: "Choosing the Right Type for the Job"
---

## Type Selection in Real Applications

Choosing the right data type is a critical architectural decision that affects correctness, performance, and maintainability. Here's how professional developers approach type selection in production systems like ShopFlow.

**Money: Always Use `decimal`**
Never use `float` or `double` for financial calculations. They use binary floating-point representation which cannot precisely represent values like 0.10 or 0.01. The `decimal` type uses base-10 arithmetic and is specifically designed for financial accuracy.

```csharp
// WRONG: Binary floating-point causes rounding errors
double badPrice = 0.1 + 0.2;  // Result: 0.30000000000000004

// CORRECT: Decimal provides exact representation
decimal goodPrice = 0.1m + 0.2m;  // Result: 0.3
```

**Identifiers: `int` vs `long` vs `Guid`**
Choose based on your scale requirements:
- `int`: Supports up to 2.1 billion unique records—sufficient for most applications
- `long`: Use when you expect billions of records (social media scale)
- `Guid`: When you need globally unique IDs across distributed systems without coordination

**Strings: Understanding Immutability**
Strings in C# are immutable—every modification creates a new string object. For building strings in loops or concatenating many values, use `StringBuilder` to avoid memory pressure and performance degradation.

**DateTime: Always Think About Time Zones**
Store dates in UTC (`DateTime.UtcNow`) and convert to local time only for display. This prevents bugs when users are in different time zones or when daylight saving time changes.

**In ShopFlow**, we follow these patterns:
- `decimal` for all prices, totals, and tax calculations
- `int` for entity IDs (our scale doesn't require `long`)
- `string` for names and descriptions
- `DateTime` in UTC for all timestamps, converted to local only in the UI layer