---
type: "THEORY"
title: "Lazy Initialization"
---


**Lazy properties** are initialized only when they're first accessed. Perfect for expensive operations that might not be needed.

### The `lazy` Delegate


**Output**:

**Key Points**:
- The lambda `{ ... }` is only executed once, on first access
- The result is cached and reused for subsequent accesses
- Thread-safe by default
- Can only be used with `val` (not `var`)

**Example: Configuration Loading**


**Output**:

---



```kotlin
App object created
Application starting...
Loading configuration from file...
App: MyApp v1.0.0
Database: localhost
```
