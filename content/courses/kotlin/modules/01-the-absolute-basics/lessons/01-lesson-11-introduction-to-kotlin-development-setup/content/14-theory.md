---
type: "THEORY"
title: "Programming Best Practices (Start Building Good Habits!)"
---


### 1. Use Meaningful Names


### 2. Add Comments


**Comment Types**:
- `// Single-line comment`
- `/* Multi-line
     comment */`

### 3. Use Blank Lines for Readability


---



```kotlin
// ❌ Cramped
fun main() {
    println("What's your name?")
    val name = readln()
    println("Hello, $name!")
}

// ✅ Readable
fun main() {
    println("What's your name?")
    val name = readln()

    println("Hello, $name!")
}
```
