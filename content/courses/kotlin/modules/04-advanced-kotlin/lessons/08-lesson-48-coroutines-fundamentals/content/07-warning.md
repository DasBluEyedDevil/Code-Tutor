---
type: "WARNING"
title: "When to Use runBlocking"
---


### runBlocking Is for Demos Only!

`runBlocking` blocks the current thread until all coroutines inside complete. This is dangerous in production code.

**Safe Uses:**
- `main()` functions in demos and CLI tools
- Unit tests (though `runTest` is preferred)
- Bridging synchronous to async code (rare)

**Dangerous Uses:**
- Android UI thread (causes ANR - Application Not Responding)
- Ktor route handlers (blocks server thread)
- ViewModel initialization (use `viewModelScope.launch`)
- Compose composables (use `LaunchedEffect`)

---



```kotlin
// DEMO ONLY - appropriate for main()
fun main() = runBlocking {
    val data = fetchData()
    println(data)
}

// PRODUCTION - use proper scopes
class UserViewModel : ViewModel() {
    init {
        // Use viewModelScope, NOT runBlocking
        viewModelScope.launch {
            val data = fetchData()
            _state.value = data
        }
    }
}

// KTOR - suspend function, no blocking needed
suspend fun Route.userRoutes() {
    get("/users") {
        val users = userService.getAll()  // Already suspending
        call.respond(users)
    }
}
```
