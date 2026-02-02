---
type: "THEORY"
title: "Scopes: Beyond Singletons"
---

Scopes create container-like lifecycles for groups of dependencies:

### User Session Scope

```kotlin
// Define scope
val userSessionModule = module {
    scope(named("userSession")) {
        scoped { UserSession() }
        scoped { UserPreferences(get()) }
        scoped { CartRepository(get(), get()) }
    }
}

class UserSession {
    var userId: String? = null
    var authToken: String? = null
}
```

### Managing Scope Lifecycle

```kotlin
class SessionManager {
    private var sessionScope: Scope? = null
    
    fun login(userId: String, token: String) {
        // Create new session scope
        sessionScope = getKoin().createScope(
            "session_$userId",
            named("userSession")
        )
        
        // Initialize session
        val session = sessionScope!!.get<UserSession>()
        session.userId = userId
        session.authToken = token
    }
    
    fun logout() {
        // Close scope - all scoped instances are destroyed
        sessionScope?.close()
        sessionScope = null
    }
    
    fun <T: Any> getSessionDependency(clazz: KClass<T>): T? {
        return sessionScope?.get(clazz)
    }
}
```