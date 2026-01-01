---
type: "THEORY"
title: "Hands-On Exercises"
---


### Exercise 1: Temperature Converter

**Goal**: Create a comprehensive temperature converter.

**Requirements**:
1. Create `celsiusToFahrenheit(celsius: Double): Double`
2. Create `fahrenheitToCelsius(fahrenheit: Double): Double`
3. Create `celsiusToKelvin(celsius: Double): Double`
4. Create `displayConversions(temp: Double, unit: String)` that shows all conversions
5. Test with different temperatures

**Formulas**:
- F = (C × 9/5) + 32
- C = (F - 32) × 5/9
- K = C + 273.15

**Try it yourself first, then check the solution!**

<details>
<summary>Click to see Solution</summary>


**Output**:

</details>

---

### Exercise 2: Shopping Cart Calculator

**Goal**: Create a shopping cart calculator with tax and discounts.

**Requirements**:
1. Create `calculateSubtotal(price: Double, quantity: Int): Double`
2. Create `calculateTax(amount: Double, taxRate: Double = 0.08): Double`
3. Create `applyDiscount(amount: Double, discountPercent: Double = 0.0): Double`
4. Create `calculateTotal(price: Double, quantity: Int, taxRate: Double, discountPercent: Double): Double`
5. Create `displayReceipt(itemName: String, price: Double, quantity: Int, taxRate: Double, discountPercent: Double)`

<details>
<summary>Click to see Solution</summary>


</details>

---

### Exercise 3: Grade Calculator

**Goal**: Create a student grade calculator.

**Requirements**:
1. Create `calculateAverage(score1: Int, score2: Int, score3: Int): Double`
2. Create `getLetterGrade(average: Double): String`
3. Create `isPassing(grade: String): Boolean`
4. Create `displayGradeReport(name: String, score1: Int, score2: Int, score3: Int)`

**Grading Scale**:
- A: 90-100
- B: 80-89
- C: 70-79
- D: 60-69
- F: Below 60
- Passing: C or better

<details>
<summary>Click to see Solution</summary>


**Output**:

</details>

---

### Exercise 4: BMI Calculator

**Goal**: Create a Body Mass Index calculator with health recommendations.

**Requirements**:
1. Create `calculateBMI(weightKg: Double, heightM: Double): Double`
2. Create `getBMICategory(bmi: Double): String`
3. Create `getHealthAdvice(category: String): String`
4. Test with different values

**BMI Categories**:
- Underweight: < 18.5
- Normal: 18.5-24.9
- Overweight: 25-29.9
- Obese: ≥ 30

**Formula**: BMI = weight (kg) / height² (m)

<details>
<summary>Click to see Solution</summary>


</details>

---



```kotlin
fun calculateBMI(weightKg: Double, heightM: Double): Double {
    return weightKg / (heightM * heightM)
}

fun getBMICategory(bmi: Double): String {
    return when {
        bmi < 18.5 -> "Underweight"
        bmi < 25.0 -> "Normal weight"
        bmi < 30.0 -> "Overweight"
        else -> "Obese"
    }
}

fun getHealthAdvice(category: String): String {
    return when (category) {
        "Underweight" -> "Consider consulting a nutritionist to gain weight healthily."
        "Normal weight" -> "Great! Maintain your current healthy lifestyle."
        "Overweight" -> "Consider a balanced diet and regular exercise."
        "Obese" -> "Consult a healthcare provider for a personalized health plan."
        else -> "Unknown category"
    }
}

fun displayBMIReport(name: String, weightKg: Double, heightM: Double) {
    val bmi = calculateBMI(weightKg, heightM)
    val category = getBMICategory(bmi)
    val advice = getHealthAdvice(category)

    println("═══════════════════════════════════════")
    println("         BMI HEALTH REPORT")
    println("═══════════════════════════════════════")
    println()
    println("Name: $name")
    println("Weight: ${weightKg}kg")
    println("Height: ${heightM}m")
    println()
    println("BMI: ${"%.1f".format(bmi)}")
    println("Category: $category")
    println()
    println("Health Advice:")
    println(advice)
    println()
    println("═══════════════════════════════════════")
    println()
}

fun main() {
    displayBMIReport("Alice", 65.0, 1.70)
    displayBMIReport("Bob", 95.0, 1.80)
    displayBMIReport("Charlie", 55.0, 1.75)
}
```
