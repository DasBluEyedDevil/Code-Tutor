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
4. Create `displayConversions(temp: Double, unit: String)` that shows all conversions (e.g., if given 25C, display F and K values)
5. Test with different temperatures

**Formulas**:
- F = (C × 9/5) + 32
- C = (F - 32) × 5/9
- K = C + 273.15

**Starter Code**:
```kotlin
fun celsiusToFahrenheit(celsius: Double): Double {
    // Implement formula
    TODO()
}

fun fahrenheitToCelsius(fahrenheit: Double): Double {
    // Implement formula
    TODO()
}

fun celsiusToKelvin(celsius: Double): Double {
    // Implement formula
    TODO()
}

fun displayConversions(temp: Double, unit: String) {
    // Call conversion functions and print results
    // Handle 'C', 'F', 'K' as input unit
    TODO()
}

fun main() {
    println("--- Temperature Converter ---")
    displayConversions(25.0, "C")
    displayConversions(77.0, "F")
    displayConversions(298.15, "K")
}
```

**Expected Output**:
```text
--- Temperature Converter ---
25.0°C = 77.0°F = 298.15K
77.0°F = 25.0°C = 298.15K
298.15K = 25.0°C = 77.0°F
```

**Try it yourself first, then check the solution!**

<details>
<summary>Click to see Solution</summary>

```kotlin
fun celsiusToFahrenheit(celsius: Double): Double {
    return (celsius * 9 / 5) + 32
}

fun fahrenheitToCelsius(fahrenheit: Double): Double {
    return (fahrenheit - 32) * 5 / 9
}

fun celsiusToKelvin(celsius: Double): Double {
    return celsius + 273.15
}

fun displayConversions(temp: Double, unit: String) {
    when (unit.uppercase()) {
        "C" -> {
            val f = celsiusToFahrenheit(temp)
            val k = celsiusToKelvin(temp)
            println("${"%.2f".format(temp)}°C = ${"%.2f".format(f)}°F = ${"%.2f".format(k)}K")
        }
        "F" -> {
            val c = fahrenheitToCelsius(temp)
            val k = celsiusToKelvin(c) // Convert C to K
            println("${"%.2f".format(temp)}°F = ${"%.2f".format(c)}°C = ${"%.2f".format(k)}K")
        }
        "K" -> {
            val c = temp - 273.15 // Convert K to C
            val f = celsiusToFahrenheit(c)
            println("${"%.2f".format(temp)}K = ${"%.2f".format(c)}°C = ${"%.2f".format(f)}°F")
        }
        else -> println("Invalid unit: $unit")
    }
}

fun main() {
    println("--- Temperature Converter ---")
    displayConversions(25.0, "C")
    displayConversions(77.0, "F")
    displayConversions(298.15, "K")
}
```

</details>

---

### Exercise 2: Shopping Cart Calculator

**Goal**: Create a shopping cart calculator with tax and discounts.

**Requirements**:
1. Create `calculateSubtotal(price: Double, quantity: Int): Double`
2. Create `calculateTax(amount: Double, taxRate: Double = 0.08): Double`
3. Create `applyDiscount(amount: Double, discountPercent: Double = 0.0): Double`
4. Create `calculateTotal(price: Double, quantity: Int, taxRate: Double = 0.08, discountPercent: Double = 0.0): Double`
5. Create `displayReceipt(itemName: String, price: Double, quantity: Int, taxRate: Double = 0.08, discountPercent: Double = 0.0)`

**Starter Code**:
```kotlin
fun calculateSubtotal(price: Double, quantity: Int): Double {
    TODO()
}

fun calculateTax(amount: Double, taxRate: Double = 0.08): Double {
    TODO()
}

fun applyDiscount(amount: Double, discountPercent: Double = 0.0): Double {
    TODO()
}

fun calculateTotal(price: Double, quantity: Int, taxRate: Double = 0.08, discountPercent: Double = 0.0): Double {
    TODO()
}

fun displayReceipt(itemName: String, price: Double, quantity: Int, taxRate: Double = 0.08, discountPercent: Double = 0.0) {
    TODO()
}

fun main() {
    println("--- Shopping Cart Calculator ---")
    displayReceipt("Laptop", 1200.0, 1)
    displayReceipt("Keyboard", 75.0, 2, discountPercent = 0.10)
    displayReceipt("Mouse", 25.0, 1, taxRate = 0.05)
}
```

**Expected Output**:
```text
--- Shopping Cart Calculator ---
Receipt for Laptop:
  Price: $1200.00
  Quantity: 1
  Subtotal: $1200.00
  Tax (8.00%): $96.00
  Discount (0.00%): $0.00
  Total: $1296.00

Receipt for Keyboard:
  Price: $75.00
  Quantity: 2
  Subtotal: $150.00
  Tax (8.00%): $12.00
  Discount (10.00%): $15.00
  Total: $147.00

Receipt for Mouse:
  Price: $25.00
  Quantity: 1
  Subtotal: $25.00
  Tax (5.00%): $1.25
  Discount (0.00%): $0.00
  Total: $26.25
```

<details>
<summary>Click to see Solution</summary>

```kotlin
fun calculateSubtotal(price: Double, quantity: Int): Double {
    return price * quantity
}

fun calculateTax(amount: Double, taxRate: Double = 0.08): Double {
    return amount * taxRate
}

fun applyDiscount(amount: Double, discountPercent: Double = 0.0): Double {
    return amount * (1 - discountPercent)
}

fun calculateTotal(price: Double, quantity: Int, taxRate: Double = 0.08, discountPercent: Double = 0.0): Double {
    val subtotal = calculateSubtotal(price, quantity)
    val discountedSubtotal = applyDiscount(subtotal, discountPercent)
    val tax = calculateTax(discountedSubtotal, taxRate)
    return discountedSubtotal + tax
}

fun displayReceipt(itemName: String, price: Double, quantity: Int, taxRate: Double = 0.08, discountPercent: Double = 0.0) {
    val subtotal = calculateSubtotal(price, quantity)
    val discountedSubtotal = applyDiscount(subtotal, discountPercent)
    val tax = calculateTax(discountedSubtotal, taxRate)
    val total = discountedSubtotal + tax

    println("Receipt for $itemName:")
    println("  Price: $%.2f".format(price))
    println("  Quantity: $quantity")
    println("  Subtotal: $%.2f".format(subtotal))
    println("  Tax (${"%.2f".format(taxRate * 100)}%): $%.2f".format(tax))
    println("  Discount (${"%.2f".format(discountPercent * 100)}%): $%.2f".format(subtotal * discountPercent))
    println("  Total: $%.2f".format(total))
    println()
}

fun main() {
    println("--- Shopping Cart Calculator ---")
    displayReceipt("Laptop", 1200.0, 1)
    displayReceipt("Keyboard", 75.0, 2, discountPercent = 0.10)
    displayReceipt("Mouse", 25.0, 1, taxRate = 0.05)
}
```

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

**Starter Code**:
```kotlin
fun calculateAverage(score1: Int, score2: Int, score3: Int): Double {
    TODO()
}

fun getLetterGrade(average: Double): String {
    TODO()
}

fun isPassing(grade: String): Boolean {
    TODO()
}

fun displayGradeReport(name: String, score1: Int, score2: Int, score3: Int) {
    TODO()
}

fun main() {
    println("--- Grade Calculator ---")
    displayGradeReport("Alice", 90, 85, 92)
    displayGradeReport("Bob", 60, 65, 58)
    displayGradeReport("Charlie", 75, 72, 79)
}
```

**Expected Output**:
```text
--- Grade Calculator ---
Grade Report for Alice:
  Scores: 90, 85, 92
  Average: 89.00
  Letter Grade: B
  Status: Passing

Grade Report for Bob:
  Scores: 60, 65, 58
  Average: 61.00
  Letter Grade: D
  Status: Failing

Grade Report for Charlie:
  Scores: 75, 72, 79
  Average: 75.33
  Letter Grade: C
  Status: Passing
```

<details>
<summary>Click to see Solution</summary>

```kotlin
fun calculateAverage(score1: Int, score2: Int, score3: Int): Double {
    return (score1 + score2 + score3) / 3.0
}

fun getLetterGrade(average: Double): String {
    return when {
        average >= 90 -> "A"
        average >= 80 -> "B"
        average >= 70 -> "C"
        average >= 60 -> "D"
        else -> "F"
    }
}

fun isPassing(grade: String): Boolean {
    return grade == "A" || grade == "B" || grade == "C"
}

fun displayGradeReport(name: String, score1: Int, score2: Int, score3: Int) {
    val average = calculateAverage(score1, score2, score3)
    val letterGrade = getLetterGrade(average)
    val passingStatus = if (isPassing(letterGrade)) "Passing" else "Failing"

    println("Grade Report for $name:")
    println("  Scores: $score1, $score2, $score3")
    println("  Average: %.2f".format(average))
    println("  Letter Grade: $letterGrade")
    println("  Status: $passingStatus")
    println()
}

fun main() {
    println("--- Grade Calculator ---")
    displayGradeReport("Alice", 90, 85, 92)
    displayGradeReport("Bob", 60, 65, 58)
    displayGradeReport("Charlie", 75, 72, 79)
}
```

</details>

---

### Exercise 4: BMI Calculator

**Goal**: Create a Body Mass Index calculator with health recommendations.

**Requirements**:
1. Create `calculateBMI(weightKg: Double, heightM: Double): Double`
2. Create `getBMICategory(bmi: Double): String`
3. Create `getHealthAdvice(category: String): String`
4. Create `displayBMIReport(name: String, weightKg: Double, heightM: Double)` to format and display the report
5. Test with different values

**BMI Categories**:
- Underweight: < 18.5
- Normal: 18.5-24.9
- Overweight: 25-29.9
- Obese: ≥ 30

**Formula**: BMI = weight (kg) / height² (m)

**Starter Code**:
```kotlin
fun calculateBMI(weightKg: Double, heightM: Double): Double {
    TODO()
}

fun getBMICategory(bmi: Double): String {
    TODO()
}

fun getHealthAdvice(category: String): String {
    TODO()
}

fun displayBMIReport(name: String, weightKg: Double, heightM: Double) {
    TODO()
}

fun main() {
    println("--- BMI Calculator ---")
    displayBMIReport("Alice", 65.0, 1.70)
    displayBMIReport("Bob", 95.0, 1.80)
    displayBMIReport("Charlie", 55.0, 1.75)
}
```

**Expected Output**:
```text
--- BMI Calculator ---
═══════════════════════════════════════
         BMI HEALTH REPORT
═══════════════════════════════════════

Name: Alice
Weight: 65.0kg
Height: 1.7m

BMI: 22.5
Category: Normal weight

Health Advice:
Great! Maintain your current healthy lifestyle.

═══════════════════════════════════════

═══════════════════════════════════════
         BMI HEALTH REPORT
═══════════════════════════════════════

Name: Bob
Weight: 95.0kg
Height: 1.8m

BMI: 29.3
Category: Overweight

Health Advice:
Consider a balanced diet and regular exercise.

═══════════════════════════════════════

═══════════════════════════════════════
         BMI HEALTH REPORT
═══════════════════════════════════════

Name: Charlie
Weight: 55.0kg
Height: 1.75m

BMI: 18.0
Category: Underweight

Health Advice:
Consider consulting a nutritionist to gain weight healthily.

═══════════════════════════════════════
```

<details>
<summary>Click to see Solution</summary>

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
    println("Weight: ${"%.1f".format(weightKg)}kg")
    println("Height: ${"%.1f".format(heightM)}m")
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
    println("--- BMI Calculator ---")
    displayBMIReport("Alice", 65.0, 1.70)
    displayBMIReport("Bob", 95.0, 1.80)
    displayBMIReport("Charlie", 55.0, 1.75)
}
```

</details>



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
