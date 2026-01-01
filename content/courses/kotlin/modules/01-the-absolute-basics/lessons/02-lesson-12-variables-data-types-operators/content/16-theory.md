---
type: "THEORY"
title: "Solution 3: Age Calculator"
---



**Solution Code**:

```kotlin
fun main() {
    println("=== Age Calculator ===")
    println("Enter your age in years:")
    
    val years = readln().toInt()
    
    val days = years * 365
    val hours = days * 24
    val minutes = hours * 60
    
    println("\nYou are approximately:")
    println("$days days old")
    println("$hours hours old")
    println("$minutes minutes old")
}
```

**Sample Output**:
