---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: C) fun**

Kotlin uses `fun` keyword to declare functions:


---

**Question 2: C) Unit**

`Unit` is Kotlin's type for "no meaningful return value":


---

**Question 3: B) `fun double(x: Int) = x * 2`**

Single-expression functions use `=` instead of curly braces:


---

**Question 4: C) Improving code readability and allowing any parameter order**

Named arguments make function calls clearer:


---

**Question 5: C) The receiver object (the object the function is called on)**

In extension functions, `this` is the object being extended:


---



```kotlin
fun String.shout(): String {
    return this.uppercase() + "!!!"
    //     ^^^^
    //     The String object
}

"hello".shout()  // this = "hello"
```
