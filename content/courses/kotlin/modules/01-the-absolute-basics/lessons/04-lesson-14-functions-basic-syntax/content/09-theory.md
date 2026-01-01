---
type: "THEORY"
title: "Named Arguments"
---


When calling a function, you can specify the name of the arguments. This makes the code much clearer, especially for functions with many parameters.

```kotlin
fun main() {
    // Without names: What does 'true' mean?
    formatText("hello", true, false, true)

    // With names: crystal clear!
    formatText("hello", uppercase = true, trim = false, reverse = true)
}
```

**Benefits of Named Arguments**:
...



```kotlin
fun formatText(
    text: String,
    uppercase: Boolean = false,
    trim: Boolean = true,
    reverse: Boolean = false
) {
    var result = text
    if (trim) result = result.trim()
    if (uppercase) result = result.uppercase()
    if (reverse) result = result.reversed()
    println(result)
}

fun main() {
    formatText("  hello  ", uppercase = true, reverse = true)
    // Output: OLLEH
}
```
