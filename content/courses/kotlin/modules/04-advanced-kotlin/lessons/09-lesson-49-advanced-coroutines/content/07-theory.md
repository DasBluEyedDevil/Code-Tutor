---
type: "THEORY"
title: "StateFlow and SharedFlow - Modern State Management"
---


StateFlow and SharedFlow are the modern, recommended way to handle observable state and events in Kotlin. They are hot flows that actively maintain values or broadcast events, making them ideal for UI state management in Android and other reactive applications.

### Why StateFlow and SharedFlow?

**Modern Alternative to LiveData**:
- Kotlin-first, not Android-specific
- Works with coroutines natively
- Better nullability handling
- No lifecycle awareness required (more flexible)

### StateFlow - Observable State Container

StateFlow always holds a current value and emits it to new collectors immediately.

**Key Characteristics**:
- Always has a value (never null unless explicitly typed as nullable)
- Conflates duplicate consecutive values (won't emit same value twice)
- Hot flow - active regardless of collectors
- Thread-safe value updates

---

