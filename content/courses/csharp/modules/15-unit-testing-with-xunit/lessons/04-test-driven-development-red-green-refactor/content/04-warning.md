---
type: "WARNING"
title: "Common TDD Mistakes"
---

## TDD Anti-Patterns to Avoid

**1. Writing Too Much Test Code at Once**
The RED phase should be ONE failing test, not ten. If you write many tests before any implementation, you've lost the rapid feedback loop. One test, one implementation, one refactor - repeat.

**2. Skipping the RED Phase**
Always see your test FAIL first! A test that never failed might not test what you think. If you write implementation first 'just to see how it works,' you've abandoned TDD and lost its design benefits.

**3. Over-Engineering in GREEN**
GREEN means MINIMAL code to pass. Don't add features the test doesn't require. Don't optimize. Don't handle edge cases you haven't tested yet. Write the simplest thing that works:

```csharp
// BAD - Over-engineered for one test
public decimal Calculate(List<Item> items) {
    if (items == null) throw new ArgumentNullException();
    if (!items.Any()) return 0;
    return items.Where(i => i.IsValid).Sum(i => i.Total);
}

// GOOD - Just enough for current test
public decimal Calculate(List<Item> items) {
    return items.Sum(i => i.Price);
}
```

**4. Skipping the REFACTOR Phase**
After GREEN, you MUST consider refactoring. Code that 'just works' accumulates technical debt. Refactor while context is fresh and tests protect you.

**5. Testing Implementation Instead of Behavior**
Test WHAT code does, not HOW it does it:

```csharp
// BAD - Tests implementation details
Assert.True(calculator._internalCache.ContainsKey("total"));

// GOOD - Tests observable behavior
Assert.Equal(100m, calculator.GetTotal());
```

Implementation-focused tests break when you refactor, even if behavior is unchanged. Behavior-focused tests let you freely restructure internals.