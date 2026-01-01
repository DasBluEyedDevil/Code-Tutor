---
type: "THEORY"
title: "Solution: Simple Calculator"
---



**Sample Run**:

**What's Happening**:
1. We read two numbers from the user
2. We add them: `val sum = num1 + num2`
3. We print the result with string interpolation

---

```kotlin
fun main() {
    println("=== Simple Calculator ===")
    
    print("Enter first number: ")
    val num1 = readln().toInt()
    
    print("Enter second number: ")
    val num2 = readln().toInt()
    
    val sum = num1 + num2
    println("$num1 + $num2 = $sum")
}
```

**Sample Run**:
