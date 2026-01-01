---
type: "THEORY"
title: "Return Values: Getting Results Back"
---


So far, our functions only **do** things (print output). But what if you want a function to **calculate** something and give you the result to use elsewhere?

**That's where return values come in!**

### The Return Statement


**Output**:

**Anatomy of a Return Function**:

---

### Return Types Explained

The return type tells you what kind of value the function will give back:


---

### Using Return Values

Once a function returns a value, you can use it in many ways:

#### 1. Store in a Variable

#### 2. Use Directly in Expressions

#### 3. Print Directly

#### 4. Use in Conditions

#### 5. Chain Function Calls

---

### Functions with Early Return

A function can have multiple return statements:


**How it works**:
- When a return is executed, the function immediately exits
- No code after the return runs
- Very useful for handling different cases

---

### Void Functions (Unit Type)

What about functions that don't return anything meaningful?


**Unit** is Kotlin's way of saying "this function doesn't return a useful value." It's like `void` in other languages, but in Kotlin, you usually just omit it.

---



```kotlin
fun printWelcome(name: String): Unit {
    println("Welcome, $name!")
    // No return statement needed
}

// Unit can be omitted (it's the default)
fun printGoodbye(name: String) {
    println("Goodbye, $name!")
}

fun main() {
    printWelcome("Alice")  // Welcome, Alice!
    printGoodbye("Bob")    // Goodbye, Bob!
}
```
