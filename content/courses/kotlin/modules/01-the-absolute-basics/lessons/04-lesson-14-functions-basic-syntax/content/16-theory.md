---
type: "THEORY"
title: "Solution 2: BMI Calculator with Functions"
---



**Solution Code**:

```kotlin
fun calculateBMI(weight: Double, height: Double): Double {
    return weight / (height * height)
}

fun getBMICategory(bmi: Double): String {
    return when {
        bmi < 18.5 -> "Underweight"
        bmi < 25.0 -> "Normal weight"
        bmi < 30.0 -> "Overweight"
        else -> "Obese"
    }
}

fun displayBMIReport(name: String, bmi: Double, category: String) {
    println("\n=== BMI Report for $name ===")
    println("BMI: %.2f".format(bmi))
    println("Category: $category")
    println("==============================")
}

fun main() {
    println("=== BMI Calculator ===")
    
    print("Enter your name: ")
    val name = readln()
    
    print("Enter your weight (kg): ")
    val weight = readln().toDouble()
    
    print("Enter your height (meters): ")
    val height = readln().toDouble()
    
    val bmi = calculateBMI(weight, height)
    val category = getBMICategory(bmi)
    
    displayBMIReport(name, bmi, category)
}
```

**Sample Output**:
