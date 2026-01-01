---
type: "THEORY"
title: "StateFlow - Observable State Container"
---

**StateFlow** holds a single value and emits updates to collectors:

```kotlin
import kotlinx.coroutines.flow.*

class CounterViewModel {
    private val _count = MutableStateFlow(0) // Initial value required
    val count: StateFlow<Int> = _count.asStateFlow() // Read-only exposure
    
    fun increment() {
        _count.value++
    }
    
    fun decrement() {
        _count.value--
    }
}
```

### Key Characteristics

| Feature | Description |
|---------|-------------|
| **Always has value** | Must have initial value |
| **Conflated** | Only latest value is kept |
| **Equality-based** | Same value not re-emitted |
| **Hot** | Exists independently of collectors |
| **Thread-safe** | Safe for concurrent updates |