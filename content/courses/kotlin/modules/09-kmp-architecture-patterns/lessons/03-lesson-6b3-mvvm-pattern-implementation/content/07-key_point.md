---
type: "KEY_POINT"
title: "Key Takeaways"
---

**MVVM in KMP puts ViewModels in commonMain exposing StateFlow/SharedFlow**—platform UI code observes these flows. ViewModels coordinate use cases, handle business logic, and transform domain data into UI state.

**UI state should be a single immutable data class** representing everything the UI needs. Write `data class ProfileScreenState(val user: User?, val isLoading: Boolean, val error: String?)` instead of multiple StateFlows.

**ViewModels own coroutine scopes** for launching async operations. When the ViewModel is cleared, its scope cancels all ongoing work, preventing leaks and ensuring structured concurrency.
