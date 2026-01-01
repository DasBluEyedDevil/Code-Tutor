---
type: "THEORY"
title: "Safe Call Operator (?.)"
---


The safe call operator `?.` safely accesses properties/methods on nullable objects.

### Basic Usage
The safe call operator `?.` allows you to call a function or access a property only if the variable is NOT null.

```kotlin
val name: String? = null
println(name?.length) // Prints "null" instead of crashing
```

### How it Works
1. If the variable is NOT null, it performs the action.
2. If the variable IS null, it skips the action and returns `null`.

### Chaining Safe Calls
You can chain multiple safe calls together. If any part of the chain is null, the whole thing returns null.

```kotlin
// user?.profile?.avatar?.url
```

### Safe Calls with Methods
...



```kotlin
val text: String? = "  Hello  "

println(text?.trim())       // "Hello"
println(text?.uppercase())  // "HELLO"

val nullText: String? = null
println(nullText?.trim())   // null
```
