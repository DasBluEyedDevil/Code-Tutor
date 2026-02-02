---
type: "THEORY"
title: "Advanced Annotations"
---

### @Scope - For Scoped Dependencies

```kotlin
// Define a scope
@Scope(name = "session")
annotation class SessionScope

@SessionScope
class UserSession {
    var userId: String? = null
}

// Usage
val sessionScope = getKoin().createScope("session", named("session"))
val session = sessionScope.get<UserSession>()
// Later: sessionScope.close()
```

### @Property - For Configuration

```kotlin
@Single
class ApiClient(
    @Property("api.baseUrl") val baseUrl: String,
    @Property("api.timeout") val timeout: Long
)

// In startKoin:
startKoin {
    properties(mapOf(
        "api.baseUrl" to "https://api.example.com",
        "api.timeout" to 30000L
    ))
}
```

### @Provided - External Dependencies

For dependencies that come from outside Koin (like platform modules):

```kotlin
@Single
class NotesRepositoryImpl(
    @Provided database: AppDatabase  // Provided by platform module
) : NotesRepository
```