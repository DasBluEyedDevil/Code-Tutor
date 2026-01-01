---
type: "ANALOGY"
title: "The Concept"
---


### The Recipe Analogy

**Simple Functions** (what you know already):

**Functions with Parameters** (what you're learning now):

**Real-World Examples**:
- **Coffee Shop**: `makeCoffee(size, type, milk)` → Takes your preferences, returns your custom coffee
- **ATM Machine**: `withdraw(accountNumber, amount)` → Takes account and amount, returns cash
- **Calculator**: `add(number1, number2)` → Takes two numbers, returns their sum

### Parameters vs Arguments

These terms are often confused, but they're different:

- **Parameter**: The placeholder variable in the function definition (like a recipe ingredient slot)
- **Argument**: The actual value you pass when calling the function (like the real ingredient)


Think of it like a form:
- **Parameter**: The blank field "Name: _______"
- **Argument**: What you write in that field "Name: Alice"

---



```kotlin
fun greet(name: String) {  // 'name' is a PARAMETER
    println("Hello, $name!")
}

fun main() {
    greet("Alice")  // "Alice" is an ARGUMENT
}
```
