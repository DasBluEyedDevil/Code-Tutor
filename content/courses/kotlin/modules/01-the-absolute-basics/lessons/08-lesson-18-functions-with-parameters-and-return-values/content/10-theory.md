---
type: "THEORY"
title: "Quick Quiz"
---


Test your understanding!

### Question 1
What's the difference between a parameter and an argument?

A) They are the same thing
B) Parameter is in the function definition, argument is the actual value passed
C) Argument is in the function definition, parameter is the actual value passed
D) Parameters are for strings, arguments are for numbers

<details>
<summary>Show Answer</summary>

**Answer: B) Parameter is in the function definition, argument is the actual value passed**

Explanation:

Parameters are placeholders in the function signature. Arguments are the actual values you provide when calling the function.

</details>

---

### Question 2
What does this function return?

```kotlin
fun calculate(x: Int): Int {
    x * 2
}
```
A) 0
B) The value of x multiplied by 2
C) Nothing - it's an error
D) Unit

<details>
<summary>Show Answer</summary>

**Answer: C) Nothing - it's an error**

Explanation: The function has a return type of `Int` but no `return` statement. The calculation `x * 2` happens but the result is not returned.

**Correct version**:
```kotlin
fun calculate(x: Int): Int {
    return x * 2
}
```
</details>

### Question 4
What will this code output?

```kotlin
fun greet(name: String = "Guest", greeting: String = "Hello") {
    println("$greeting, $name!")
}

fun main() {
    greet()
}
```
A) Error: Missing arguments
B) Hello, Guest!
C) Guest, Hello!
D) Nothing

<details>
<summary>Show Answer</summary>

**Answer: B) Hello, Guest!**

Explanation: When a function has default parameters, you can call it without providing those arguments. The default values are used:
- `name` defaults to "Guest"
- `greeting` defaults to "Hello"

So `greet()` becomes `greet("Guest", "Hello")` which prints "Hello, Guest!"

</details>

---



```kotlin
fun greet(name: String = "Guest", greeting: String = "Hello") {
    println("$greeting, $name!")
}

fun main() {
    greet()
}
```
