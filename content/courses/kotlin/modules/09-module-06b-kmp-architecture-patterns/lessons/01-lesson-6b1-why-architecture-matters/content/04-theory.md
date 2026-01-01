---
type: "THEORY"
title: "Core Architectural Principles"
---

### 1. Separation of Concerns (SoC)

Each component should have one job:

```kotlin
// ❌ Mixed concerns
class UserScreen {
    fun loadUser() {
        val json = URL("api/user").readText() // Network
        val user = Json.decodeFromString(json) // Parsing
        database.save(user) // Persistence
        textView.text = user.name // UI
    }
}

// ✅ Separated concerns
class UserRepository { fun getUser(): User }
class UserViewModel { val user: StateFlow<User> }
class UserScreen { @Composable fun Content() }
```

### 2. Dependency Inversion

High-level modules shouldn't depend on low-level modules:

```kotlin
// ❌ Direct dependency
class UserViewModel {
    private val api = RetrofitApi() // Coupled to Retrofit
}

// ✅ Abstraction dependency
class UserViewModel(
    private val repository: UserRepository // Interface
)
```

### 3. Single Source of Truth

Data should have one authoritative source:

```kotlin
// ❌ Multiple sources
class Screen1 { var userName = "" }
class Screen2 { var userName = "" } // Can get out of sync!

// ✅ Single source
class UserState {
    val userName: StateFlow<String> // One source, many observers
}
```

### 4. Unidirectional Data Flow

Data flows in one direction:

```
User Action → ViewModel → State → UI → User Sees Result
              ↑                          |
              |__________________________|  (User acts again)
```