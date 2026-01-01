---
type: "THEORY"
title: "SharedFlow - Event Broadcasting"
---


SharedFlow is designed for events that should be delivered to all collectors. Unlike StateFlow, it doesn't hold a current value by default.

### When to Use SharedFlow

**One-time events**:
- Navigation events
- Snackbar/Toast messages
- Error notifications
- Analytics events

### SharedFlow Configuration

```kotlin
MutableSharedFlow<T>(
    replay: Int = 0,           // How many past values new collectors get
    extraBufferCapacity: Int = 0,  // Additional buffer capacity
    onBufferOverflow: BufferOverflow = SUSPEND  // What to do when buffer full
)
```

### BufferOverflow Options

- `SUSPEND` - Suspend emitter until buffer has space
- `DROP_OLDEST` - Drop oldest value when buffer full
- `DROP_LATEST` - Drop newest value when buffer full

---

