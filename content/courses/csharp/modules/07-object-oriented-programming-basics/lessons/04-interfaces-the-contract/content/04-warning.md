---
type: "WARNING"
title: "Important Considerations"
---

## Default Interface Methods (C# 8+)

Starting with C# 8, interfaces CAN have default implementations! This was added to evolve interfaces without breaking existing implementers.

```csharp
interface ILogger
{
    void Log(string message);
    
    // DEFAULT implementation - classes don't HAVE to override
    void LogError(string error) => Log($"ERROR: {error}");
}
```

**Key gotchas with default interface methods:**
- Default methods are NOT inherited by the class! You must cast to the interface to call them: `((ILogger)myClass).LogError("oops")`
- They work best for ADDING new methods to existing interfaces without breaking implementers
- Don't overuse them - they can make code harder to understand
- Structs and default interface methods don't mix well (boxing occurs)

**When to use default interface methods:**
- Evolving library interfaces without breaking changes
- Providing sensible defaults that most implementers won't need to override
- NOT for complex logic - keep interfaces focused on contracts!