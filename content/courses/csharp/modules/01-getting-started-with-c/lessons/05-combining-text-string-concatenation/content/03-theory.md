---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down Syntax

**`$` (The Magic Symbol)**: Putting a dollar sign BEFORE the opening quote `$"..."` tells C# this string will contain variables.

**`{variable}` (The Placeholders)**: Inside the string, you wrap variables or expressions in curly braces `{}`. C# will calculate them and swap them out for their text value.

**`Concatenation (+)`**: The old way of joining strings.
- `("Hello " + name)` -> "Hello Alex"
- It works, but it's "noisy" and easy to break (like forgetting that space after "Hello").

**Why Interpolation is Better**:
1. **Readable**: It looks like a normal sentence.
2. **Safer**: Harder to make spacing mistakes.
3. **Powerful**: Supports formatting numbers (e.g., `{price:C}` for currency).

```csharp
double price = 19.99;
Console.WriteLine($"The cost is {price:C}"); // Output: The cost is $19.99
```
