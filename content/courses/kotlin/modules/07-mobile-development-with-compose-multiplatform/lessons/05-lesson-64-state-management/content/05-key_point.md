---
type: "KEY_POINT"
title: "remember vs rememberSaveable"
---


### remember

Preserves state across recompositions but **lost on configuration changes** (rotation, language change):


### rememberSaveable

Preserves state across **recompositions AND configuration changes**:


### When to Use Each

| Use Case                          | Use                |
|-----------------------------------|--------------------|
| Temporary UI state (dialog open) | `remember`         |
| Form input                        | `rememberSaveable` |
| User selections                   | `rememberSaveable` |
| Scroll position                   | `rememberSaveable` |
| Animation values                  | `remember`         |

### Custom Saver

For complex objects, implement a custom `Saver`:


---



```kotlin
data class User(val name: String, val email: String)

@Composable
fun CustomSaverExample() {
    var user by rememberSaveable(stateSaver = UserSaver) {
        mutableStateOf(User("", ""))
    }

    // user survives configuration changes
}

val UserSaver = Saver<User, List<String>>(
    save = { listOf(it.name, it.email) },
    restore = { User(it[0], it[1]) }
)
```
