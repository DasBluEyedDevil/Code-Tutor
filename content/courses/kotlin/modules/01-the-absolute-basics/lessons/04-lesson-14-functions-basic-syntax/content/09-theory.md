---
type: "THEORY"
title: "Named Arguments"
---


Call functions with parameter names for clarity:


**Benefits of Named Arguments**:
- Code is more readable
- Order doesn't matter
- Especially useful with many parameters or default values


---



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
