---
type: "WARNING"
title: "Common Mistakes and How to Avoid Them"
---


### Mistake 1: Integer Division Surprise
When dividing two `Int` values, Kotlin throws away the remainder.
- `5 / 2` results in `2`
- `5.0 / 2` results in `2.5` (Correct!)
Always use at least one `Double` if you want a precise decimal result.

### Mistake 2: Trying to Reassign val
You cannot change a `val` once it is set.
- `val age = 20`
- `age = 21` ❌ (Compiler error)
Use `var` if the value must change.

### Mistake 3: Type Mismatch
Kotlin won't let you put the wrong type of data into a box.
- `val name: String = 25` ❌ (Cannot put Int in String box)
- `val age: Int = "25"` ❌ (Cannot put String in Int box)

### Mistake 4: NumberFormatException
Converting text to a number will crash your program if the text isn't a valid number.
- `readln().toInt()` ❌ (Crashes if input is "abc")
Use `toIntOrNull()` for a safer way to handle user input.

---



```kotlin
// ❌ Crashes if user types non-number
val number = readln().toInt()  // User types "hello" → crash!

// ✅ Safe conversion
val number = readln().toIntOrNull() ?: 0  // Returns 0 if invalid
```
