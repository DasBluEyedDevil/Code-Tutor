---
type: "THEORY"
title: "The Challenge of Shared ViewModels"
---

### Platform Differences

| Aspect | Android | iOS |
|--------|---------|-----|
| **ViewModel** | androidx.lifecycle.ViewModel | No native equivalent |
| **Lifecycle** | Activity/Fragment lifecycle | UIViewController lifecycle |
| **Scope** | viewModelScope | Manual management |
| **State Persistence** | SavedStateHandle | No built-in equivalent |

### Approaches

1. **Pure Kotlin ViewModel** - Write once, use everywhere
2. **Expect/Actual** - Platform-specific implementations
3. **Wrapper Pattern** - Wrap shared logic in platform ViewModels