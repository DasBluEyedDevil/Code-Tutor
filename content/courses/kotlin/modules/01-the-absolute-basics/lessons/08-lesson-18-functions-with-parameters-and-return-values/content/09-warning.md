---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Common Mistakes

#### Mistake 1: Wrong Number of Arguments


---

#### Mistake 2: Wrong Argument Type


---

#### Mistake 3: Wrong Argument Order


---

#### Mistake 4: Forgetting Return Statement


---

#### Mistake 5: Incorrect Return Type


---

### Best Practices

#### 1. Use Descriptive Parameter Names


---

#### 2. Keep Functions Focused (Single Responsibility)


---

#### 3. Use Default Parameters for Optional Values


---

#### 4. Use Single-Expression Functions for Simple Logic


---

#### 5. Validate Input Parameters


---



```kotlin
fun divide(a: Double, b: Double): Double {
    if (b == 0.0) {
        println("Error: Cannot divide by zero!")
        return 0.0
    }
    return a / b
}

fun createUser(name: String, age: Int) {
    if (name.isBlank()) {
        println("Error: Name cannot be empty!")
        return
    }
    if (age < 0 || age > 150) {
        println("Error: Invalid age!")
        return
    }
    println("User created: $name, age $age")
}
```
