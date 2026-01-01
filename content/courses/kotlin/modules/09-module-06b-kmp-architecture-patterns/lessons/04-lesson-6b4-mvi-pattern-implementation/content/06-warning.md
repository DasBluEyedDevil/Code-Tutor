---
type: "WARNING"
title: "MVI Considerations"
---

### Potential Downsides

**1. Boilerplate**
```kotlin
// Every action needs an Intent class
sealed interface Intent {
    data class UpdateName(val name: String) : Intent
    data class UpdateEmail(val email: String) : Intent
    data class UpdatePhone(val phone: String) : Intent
    // ... many more for complex forms
}
```

**2. Performance Overhead**
- Every state change creates a new state object
- Large state objects can impact performance
- Solution: Use `distinctUntilChanged()` and stable keys

**3. Learning Curve**
- Team needs to understand unidirectional flow
- Side effects pattern takes time to grasp

### When MVI is Overkill

- Simple CRUD apps
- Few interacting state pieces
- Small team without MVI experience

### When MVI Shines

- Complex state with many interdependencies
- Need for state history/debugging
- Large team needing predictable patterns
- Apps with undo/redo requirements