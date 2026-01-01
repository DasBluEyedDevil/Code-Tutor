---
type: "WARNING"
title: "Critical Best Practices"
---

## Primary Constructor Parameters Are NOT Readonly!

Unlike record primary constructors, CLASS primary constructor parameters can be reassigned! This is a common source of bugs:

```csharp
public class Counter(int initialCount)
{
    public void Increment()
    {
        initialCount++;  // This WORKS but is often a mistake!
    }
    
    public int Count => initialCount;  // Returns modified value
}
```

**Best Practice: Capture to readonly fields for dependency injection:**
```csharp
public class UserService(ILogger logger, IUserRepository repo)
{
    // Capture to readonly fields immediately!
    private readonly ILogger _logger = logger;
    private readonly IUserRepository _repo = repo;
    
    public void CreateUser(User user)
    {
        _logger.Log("Creating user");  // Use the field, not the parameter
        _repo.Add(user);
    }
}
```

**Why this matters:**
- Prevents accidental reassignment
- Compiler can warn if you use parameter instead of field
- Makes intent clear: these are dependencies, not mutable state

**All other constructors must chain to primary:**
```csharp
public class Person(string name, int age)
{
    // Additional constructor MUST call primary via 'this()'
    public Person(string name) : this(name, 0) { }
}
```