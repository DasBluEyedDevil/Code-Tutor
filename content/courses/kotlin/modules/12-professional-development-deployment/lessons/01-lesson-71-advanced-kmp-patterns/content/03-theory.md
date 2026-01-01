---
type: "THEORY"
title: "Swift Interoperability"
---


### Calling Kotlin from Swift

When you compile KMP for iOS, Kotlin generates an Objective-C framework. Swift can call this code, but there are nuances to understand.

**Kotlin code in shared module:**

```kotlin
// shared/src/commonMain/kotlin/Calculator.kt
class Calculator {
    fun add(a: Int, b: Int): Int = a + b
    fun multiply(a: Int, b: Int): Int = a * b
}
```

**Swift usage:**

```swift
import shared  // Your KMP framework name

let calc = Calculator()
let result = calc.add(a: 5, b: 3)  // Named parameters!
print(result)  // 8
```

### Making Kotlin More Swift-Friendly

Use annotations to improve the Swift API:

```kotlin
// Better Swift names with @ObjCName
@ObjCName("NetworkClient")
class ApiClient {
    @ObjCName("fetchUser")
    fun getUserById(id: String): User? = ...
}

// Swift sees: NetworkClient().fetchUser(id: "123")
```

### Handling Kotlin Nullability

```kotlin
// Kotlin
fun findUser(id: String): User?  // Nullable

// Swift automatically gets optional:
// func findUser(id: String) -> User?

if let user = api.findUser(id: "123") {
    print(user.name)
}
```

### Kotlin Sealed Classes in Swift

```kotlin
sealed class Result<T> {
    data class Success<T>(val data: T) : Result<T>()
    data class Error<T>(val message: String) : Result<T>()
}
```

**Swift pattern matching:**

```swift
switch result {
case let success as ResultSuccess<User>:
    print("Got user: \(success.data.name)")
case let error as ResultError<User>:
    print("Error: \(error.message)")
default:
    break
}
```

---

