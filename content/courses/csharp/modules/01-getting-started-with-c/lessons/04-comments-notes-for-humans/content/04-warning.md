---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Forgetting to close multi-line comments**: If you start `/*` but forget `*/`, EVERYTHING after becomes a comment!
```csharp
/* This is a comment
Console.WriteLine("This is also a comment!");
Console.WriteLine("So is this - oops!");
// ERROR: Entire rest of file is commented out!
```

**Comments that just repeat the code**: Bad comments waste time!
```csharp
// Bad: "Add 1 to x" - we can see that!
x = x + 1;

// Good: "Increment retry counter after failed connection"
retryCount = retryCount + 1;
```

**Outdated comments are worse than no comments**: If you change code, UPDATE the comment!
```csharp
// Calculates the sum  <-- LIE! Code now calculates average
int result = total / count;
```

**Nested multi-line comments don't work**:
```csharp
/* outer /* inner */ still in outer! */ // ERROR!
```

**Don't comment obvious code**: Trust the reader. Comment the WHY, not the WHAT.
