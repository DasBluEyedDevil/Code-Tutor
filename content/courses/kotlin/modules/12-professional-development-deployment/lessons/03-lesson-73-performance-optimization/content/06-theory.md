---
type: "THEORY"
title: "Coroutine Performance"
---


### Dispatcher Selection

**Wrong Dispatcher = Poor Performance**:

❌ **Bad**:

✅ **Good**:

**Dispatcher Guide**:

### Avoiding Excessive Coroutine Creation

❌ **Bad** (Creates 1000 coroutines):

✅ **Good** (Single coroutine):

✅ **Better** (Parallel processing with limit):

### Flow Performance

**Cold vs Hot Flows**:

❌ **Bad** (Network call on every collect):

✅ **Good** (SharedFlow - single source):

**Debouncing Search**:

❌ **Bad** (API call on every keystroke):

✅ **Good** (Debounce 300ms):

---



```kotlin
searchField.textAsFlow()
    .debounce(300)
    .distinctUntilChanged()
    .collectLatest { query ->
        viewModel.search(query)
    }
```
