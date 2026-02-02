---
type: "EXAMPLE"
title: "Error Tracking with Sentry"
---


### Why Sentry?

- ✅ Captures all exceptions automatically
- ✅ Groups similar errors together
- ✅ Shows stack traces with context
- ✅ Email/Slack alerts on new errors
- ✅ Tracks error frequency and trends

### Backend (Ktor) Integration


**Initialize Sentry**:

**Manual Error Capture**:

### Android (Crashlytics) Integration


**Initialize in Application**:

**Log Custom Errors**:

---



```kotlin
try {
    processOrder(order)
} catch (e: Exception) {
    FirebaseCrashlytics.getInstance().apply {
        log("Processing order: ${order.id}")
        setCustomKey("order_id", order.id)
        setCustomKey("order_total", order.total)
        recordException(e)
    }
    throw e
}
```
