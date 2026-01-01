---
type: "THEORY"
title: "Exercise 2: BMI Calculator with Functions"
---


**Goal**: Create a BMI calculator using functions.

**Requirements**:
1. Create `calculateBMI(weight: Double, height: Double): Double` function
2. Create `getBMICategory(bmi: Double): String` function that returns:
   - "Underweight" if BMI < 18.5
   - "Normal weight" if BMI 18.5-24.9
   - "Overweight" if BMI 25-29.9
   - "Obese" if BMI ≥ 30
3. Create `displayBMIReport(name: String, bmi: Double, category: String)` function
4. In `main()`, get user input and display formatted report

**Starter Code**:
```kotlin
// Define your functions here

fun main() {
    println("=== BMI Calculator ===")
    // Your code here
}
```

**Expected Output**:
```text
=== BMI Calculator ===
Enter your name:
Alice
Enter your weight (kg):
65
Enter your height (meters):
1.70

=== BMI Report for Alice ===
BMI: 22.49
Category: Normal weight
==============================
```


