---
type: "THEORY"
title: "this vs it: Context Objects"
---


### Comparison

**`this` (receiver)**:
- Used by: `run`, `with`, `apply`
- Can be omitted (implicit)
- Feels like you "are" the object

**`it` (parameter)**:
- Used by: `let`, `also`
- Must be explicit
- Clearer distinction between outer and inner scope

### Examples


### When to Use Which


---



```kotlin
// Use 'this' when configuring object
val user = User().apply {
    name = "Alice"  // Clean, no 'this.' needed
    email = "alice@example.com"
    age = 25
}

// Use 'it' when object needs clear reference
val processed = user.let {
    saveToDatabase(it)  // Clear what's being passed
    sendEmail(it)
    it
}
```
