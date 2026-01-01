---
type: "THEORY"
title: "Step-by-Step Implementation"
---


### Step 1: Data Model

First, let's create a data class to store calculations:

```kotlin
data class Calculation(
    val num1: Double,
    val operator: String,
    val num2: Double,
    val result: Double
) {
    override fun toString() = "$num1 $operator $num2 = $result"
}
```

**What this does**:
...
### Step 2: Core Calculation Functions

```kotlin
fun add(a: Double, b: Double) = a + b
fun subtract(a: Double, b: Double) = a - b
fun multiply(a: Double, b: Double) = a * b
fun divide(a: Double, b: Double) = if (b != 0.0) a / b else null
```

**Key Points**:
...
### Step 3: Input Validation Functions

```kotlin
fun getNumber(prompt: String): Double? {
    print(prompt)
    return readln().toDoubleOrNull()
}

fun getMenuChoice(): Int? {
    print("Select an option (1-8): ")
    return readln().toIntOrNull()
}
```

**Why nullable returns?**
...
### Step 4: UI Functions

```kotlin
fun displayMenu() {
    println("\n--- Calculator Menu ---")
    println("1. Add")
    println("2. Subtract")
    println("3. Multiply")
    println("4. Divide")
    println("5. Modulus")
    println("6. Show History")
    println("7. Clear History")
    println("8. Exit")
}

fun displayHistory(history: List<Calculation>) {
    if (history.isEmpty()) {
        println("History is empty.")
    } else {
        println("\n--- Calculation History ---")
        history.forEachIndexed { index, calc -> println("${index + 1}: $calc") }
    }
}
```

**Design choices**:
...
### Step 5: Operation Handler

```kotlin
fun performOperation(choice: Int, history: MutableList<Calculation>) {
    val n1 = getNumber("Enter first number: ") ?: return
    val n2 = getNumber("Enter second number: ") ?: return

    val (result, op) = when (choice) {
        1 -> add(n1, n2) to "+"
        2 -> subtract(n1, n2) to "-"
        3 -> multiply(n1, n2) to "*"
        4 -> (divide(n1, n2) ?: Double.NaN) to "/"
        5 -> (n1 % n2) to "%"
        else -> 0.0 to "?"
    }

    if (result.isNaN()) {
        println("Error: Division by zero!")
    } else {
        val calc = Calculation(n1, op, n2, result)
        history.add(calc)
        println("Result: $calc")
    }
}
```

### Step 6: Main Program Loop

---



```kotlin
fun main() {
    val history = mutableListOf<Calculation>()
    var running = true

    println("Welcome to Kotlin Calculator!")

    while (running) {
        displayMenu()

        val choice = getMenuChoice()

        if (choice == null) {
            println("Invalid input! Please enter a number.")
            continue
        }

        when (choice) {
            in 1..5 -> {
                performOperation(choice, history)
            }
            6 -> {
                displayHistory(history)
            }
            7 -> {
                history.clear()
                println("History cleared!")
            }
            8 -> {
                println("\nThank you for using Kotlin Calculator!")
                println("Goodbye!")
                running = false
            }
            else -> {
                println("Invalid choice! Please select 1-8.")
            }
        }
    }
}
```
