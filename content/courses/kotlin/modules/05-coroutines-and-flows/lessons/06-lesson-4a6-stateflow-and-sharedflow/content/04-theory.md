---
type: "THEORY"
title: "SharedFlow - Event Broadcasting"
---

**SharedFlow** broadcasts values to multiple collectors without keeping latest:

```kotlin
class EventBus {
    private val _events = MutableSharedFlow<Event>()
    val events: SharedFlow<Event> = _events.asSharedFlow()
    
    suspend fun emit(event: Event) {
        _events.emit(event)
    }
}
```

### StateFlow vs SharedFlow

| Feature | StateFlow | SharedFlow |
|---------|-----------|------------|
| Initial value | Required | Not required |
| Latest value | Always available | No current value |
| Replay | 1 (latest) | Configurable (0+) |
| Use case | State | Events |

### SharedFlow Configuration
```kotlin
val sharedFlow = MutableSharedFlow<Event>(
    replay = 0,           // How many past values to replay
    extraBufferCapacity = 64, // Buffer for slow collectors
    onBufferOverflow = BufferOverflow.DROP_OLDEST
)
```

### Events vs State
```kotlin
// State - use StateFlow
val isLoggedIn: StateFlow<Boolean>  // UI needs current value
val userProfile: StateFlow<User?>   // Always need latest

// Events - use SharedFlow
val navigationEvents: SharedFlow<NavEvent>  // One-shot, don't replay
val toastMessages: SharedFlow<String>       // Show once, then gone
val errors: SharedFlow<Throwable>           // Handle once
```