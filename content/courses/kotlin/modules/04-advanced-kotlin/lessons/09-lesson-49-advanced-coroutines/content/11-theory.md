---
type: "THEORY"
title: "StateFlow vs SharedFlow vs LiveData"
---


### Comparison Table

| Feature | StateFlow | SharedFlow | LiveData |
|---------|-----------|------------|----------|
| Initial value | Required | Not required | Not required |
| Null values | Explicit | Explicit | Implicit |
| Lifecycle-aware | No | No | Yes |
| Conflation | Always | Configurable | Always |
| Replay | Always 1 | Configurable | Always 1 |
| Platform | Multiplatform | Multiplatform | Android only |

### When to Use What

**StateFlow**:
- UI state that should always have a value
- Observable properties
- When you need `.value` access
- Counter, form state, loading state

**SharedFlow**:
- One-time events
- Messages to multiple subscribers
- Events that shouldn't be replayed
- Navigation, snackbars, analytics

**LiveData** (Android legacy):
- When lifecycle awareness is critical
- Simple Android-only projects
- Existing codebases using it

### Modern Recommendation

For new Android projects, prefer **StateFlow for state** and **SharedFlow for events**. They integrate better with Kotlin coroutines and work across platforms.

---

