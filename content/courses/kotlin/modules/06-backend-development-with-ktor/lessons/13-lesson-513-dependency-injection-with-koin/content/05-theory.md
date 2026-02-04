---
type: "THEORY"
title: "Advanced Koin Features"
---


### Organizing Modules

As your app grows, split modules by feature:


Load all modules:

### Named Dependencies

Sometimes you need multiple instances of the same type:


### Scopes

Koin supports scoped instances (created per request, per session, etc.):


### Factory vs Single


**When to use each**:
- **Single**: Services, repositories, database connections (stateless or shared state)
- **Factory**: Request/response objects, temporary data (stateful per-request)

---



```kotlin
val exampleModule = module {
    // Single: One instance for entire application
    single { EmailService() }  // Reused everywhere

    // Factory: New instance every time
    factory { EmailMessage() }  // Fresh message each time
}
```
