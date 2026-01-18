---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Forgetting the `$`**: If you forget the dollar sign, C# prints the curly braces literally!
```csharp
string name = "Sam";
Console.WriteLine("Hello {name}");  // Output: Hello {name}
Console.WriteLine($"Hello {name}"); // Output: Hello Sam
```

**Using the wrong brackets**: Interpolation uses CURLY braces `{}`, not parentheses `()` or square brackets `[]`.
```csharp
Console.WriteLine($"Value: (x)"); // Wrong!
Console.WriteLine($"Value: {x}"); // Correct!
```

**Variables must exist**: You can't put a variable in `{}` if you haven't created it yet!

**Concatenation Math Trap**: If you use `+`, remember order of operations!
```csharp
Console.WriteLine("Result: " + 5 + 5);   // Output: Result: 55 (String glue)
Console.WriteLine($"Result: {5 + 5}");   // Output: Result: 10 (Math!)
```
