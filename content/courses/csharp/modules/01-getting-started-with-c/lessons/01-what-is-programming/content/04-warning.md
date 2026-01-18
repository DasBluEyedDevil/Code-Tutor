---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Forgetting the semicolon**: Every statement MUST end with `;`
```csharp
Console.WriteLine("Hi")  // ERROR: Missing semicolon!
Console.WriteLine("Hi"); // Correct
```

**Case sensitivity matters**: C# distinguishes uppercase from lowercase!
```csharp
console.writeline("Hi"); // ERROR: Wrong case!
Console.WriteLine("Hi"); // Correct
```

**Missing or mismatched quotes**: Text must be wrapped in matching double quotes.
```csharp
Console.WriteLine(Hello);   // ERROR: No quotes!
Console.WriteLine("Hello);  // ERROR: Missing closing quote!
Console.WriteLine("Hello"); // Correct
```

**Spelling errors**: `WriteLine` not `Writeline` or `WritLine` - there's no autocorrect!
