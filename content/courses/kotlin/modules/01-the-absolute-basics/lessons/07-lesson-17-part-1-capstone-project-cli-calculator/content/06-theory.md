---
type: "THEORY"
title: "Complete Solution"
---


Here's the full calculator application:


---



```kotlin
// ========================================
// Data Models
// ========================================

data class Calculation(
    val operation: String,
    val num1: Double,
    val num2: Double,
    val result: Double
) {
    override fun toString(): String {
        return "$num1 $operation $num2 = $result"
    }
}

// ========================================
// Core Calculation Functions
// ========================================

fun add(a: Double, b: Double): Double = a + b

fun subtract(a: Double, b: Double): Double = a - b

fun multiply(a: Double, b: Double): Double = a * b

fun divide(a: Double, b: Double): Double? {
    if (b == 0.0) {
        println("Error: Cannot divide by zero!")
        return null
    }
    return a / b
}

fun modulus(a: Double, b: Double): Double? {
    if (b == 0.0) {
        println("Error: Cannot calculate modulus with zero!")
        return null
    }
    return a % b
}

// ========================================
// Input Functions
// ========================================

fun getNumber(prompt: String): Double? {
    print(prompt)
    val input = readln()
    return input.toDoubleOrNull()
}

fun getMenuChoice(): Int? {
    print("Enter your choice: ")
    val input = readln()
    return input.toIntOrNull()
}

// ========================================
// UI Functions
// ========================================

fun displayMenu() {
    println("\n╔════════════════════════════════╗")
    println("║      KOTLIN CALCULATOR         ║")
    println("╠════════════════════════════════╣")
    println("║  1. Addition (+)               ║")
    println("║  2. Subtraction (-)            ║")
    println("║  3. Multiplication (*)         ║")
    println("║  4. Division (/)               ║")
    println("║  5. Modulus (%)                ║")
    println("║  6. View History               ║")
    println("║  7. Clear History              ║")
    println("║  8. Exit                       ║")
    println("╚════════════════════════════════╝")
}

fun displayHistory(history: List<Calculation>) {
    println("\n=== Calculation History ===")
    if (history.isEmpty()) {
        println("No calculations yet.")
    } else {
        history.forEachIndexed { index, calc ->
            println("${index + 1}. $calc")
        }
    }
}

fun displayResult(result: Double) {
    println("\nResult: ${"%.2f".format(result)}")
}

// ========================================
// Operation Handler
// ========================================

fun performOperation(
    operation: Int,
    history: MutableList<Calculation>
): Boolean {
    val num1 = getNumber("Enter first number: ")
    if (num1 == null) {
        println("Invalid number!")
        return true
    }

    val num2 = getNumber("Enter second number: ")
    if (num2 == null) {
        println("Invalid number!")
        return true
    }

    val result: Double?
    val opSymbol: String

    when (operation) {
        1 -> {
            opSymbol = "+"
            result = add(num1, num2)
        }
        2 -> {
            opSymbol = "-"
            result = subtract(num1, num2)
        }
        3 -> {
            opSymbol = "*"
            result = multiply(num1, num2)
        }
        4 -> {
            opSymbol = "/"
            result = divide(num1, num2)
        }
        5 -> {
            opSymbol = "%"
            result = modulus(num1, num2)
        }
        else -> {
            println("Invalid operation!")
            return true
        }
    }

    if (result != null) {
        displayResult(result)
        history.add(Calculation(opSymbol, num1, num2, result))
    }

    return true
}

// ========================================
// Main Program
// ========================================

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
