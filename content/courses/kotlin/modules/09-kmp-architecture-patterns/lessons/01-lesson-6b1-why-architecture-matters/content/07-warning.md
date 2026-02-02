---
type: "WARNING"
title: "Architecture Anti-Patterns"
---

### Avoid These Mistakes

**1. Over-Architecture for Simple Apps**
```kotlin
// ❌ Three layers for a hello world app
class HelloWorldUseCase(private val repo: HelloRepository) {
    suspend fun execute() = repo.getHello()
}
```
Start simple, add layers when complexity demands it.

**2. Leaking Platform Details**
```kotlin
// ❌ Android Context in shared code
class SharedViewModel(private val context: Context) // NO!

// ✅ Abstract platform needs
expect class PlatformStorage {
    fun save(key: String, value: String)
}
```

**3. Massive ViewModels**
```kotlin
// ❌ 2000-line ViewModel with 50 functions
class AppViewModel { ... }

// ✅ Feature-scoped ViewModels
class ProfileViewModel { ... }
class SettingsViewModel { ... }
```

**4. Ignoring the Dependency Rule**
```kotlin
// ❌ Inner layer depends on outer layer
class UserEntity(val context: Context) // Domain depends on Android!

// ✅ Dependencies point inward
class UserEntity(val id: String, val name: String) // Pure data
```