---
type: "THEORY"
title: "Step-by-Step Implementation"
---


### Step 1: Data Model

First, let's create a data class to store calculations:


**What this does**:
- Stores all information about a calculation
- Custom `toString()` for nice display
- Example: "10.0 + 5.0 = 15.0"

---

### Step 2: Core Calculation Functions


**Key Points**:
- Simple, focused functions (Single Responsibility)
- Division and modulus return `Double?` (nullable) for error handling
- Error messages provided at the point of failure

---

### Step 3: Input Validation Functions


**Why nullable returns?**
- Safely handle invalid input
- Caller decides how to handle errors
- No crashes from bad input

---

### Step 4: UI Functions


**Design choices**:
- Clean, professional-looking menu
- Box drawing for visual appeal
- Clear section headers
- Formatted output

---

### Step 5: Operation Handler


---

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
