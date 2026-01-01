---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Forgetting spaces between concatenated words**:
```csharp
Console.WriteLine("Hello" + "World");   // HelloWorld (no space!)
Console.WriteLine("Hello " + "World");  // Hello World (correct)
Console.WriteLine("Hello" + " World"); // Hello World (also correct)
```

**The + operator order trap**: C# evaluates left to right!
```csharp
Console.WriteLine("Sum: " + 2 + 2);   // Sum: 22 (text + 2 = text, then + 2 = text)
Console.WriteLine("Sum: " + (2 + 2)); // Sum: 4 (math first in parentheses)
Console.WriteLine(2 + 2 + " is the sum"); // 4 is the sum (math happens first!)
```

**Forgetting the $ for interpolation**:
```csharp
Console.WriteLine("Hello {name}");  // Prints literally: Hello {name}
Console.WriteLine($"Hello {name}"); // Prints: Hello Alex
```

**Escaping braces in interpolation**: To print literal braces, double them:
```csharp
Console.WriteLine($"Use {{name}} syntax"); // Use {name} syntax
```

**Performance note**: In loops with many iterations, consider `StringBuilder` instead of repeated concatenation (covered in advanced lessons).