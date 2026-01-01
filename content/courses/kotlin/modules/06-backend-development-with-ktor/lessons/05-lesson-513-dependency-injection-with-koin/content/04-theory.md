---
type: "THEORY"
title: "Setting Up Koin"
---


### Step 1: Add Koin Dependency

Update your `build.gradle.kts`:


### Step 2: Define Koin Modules

Create a configuration file that declares all your dependencies:


**Key Koin DSL functions**:
- `single { }`: Creates a singleton (one instance for entire app)
- `factory { }`: Creates a new instance every time
- `get()`: Resolves a dependency from Koin

### Step 3: Install Koin in Ktor

Update your Application.kt:


**Before Koin**:

**After Koin**:

Much cleaner! Koin handles all the wiring automatically.

---



```kotlin
// Automatic dependency injection
val userService by inject<UserService>()
val authService by inject<AuthService>()
```
