---
type: "THEORY"
title: "Programming Best Practices (Start Building Good Habits!)"
---


### 1. Use Meaningful Names
Your code should be self-documenting. Instead of using generic names like `x` or `v`, use names that describe what the data represents.

- `val n = readln()` ❌ (What is 'n'?)
- `val name = readln()` ✅ (Clear and obvious)
- `val s = n1 + n2` ❌
- `val sum = firstNum + secondNum` ✅

### 2. Add Comments
Comments are notes for humans. The computer ignores them. Use them to explain *why* you're doing something complex, not *what* you're doing (good names handle the 'what').

**Comment Types**:
- `// Single-line comment`: Used for quick notes on one line.
- `/* Multi-line comment */`: Used for longer explanations.

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
