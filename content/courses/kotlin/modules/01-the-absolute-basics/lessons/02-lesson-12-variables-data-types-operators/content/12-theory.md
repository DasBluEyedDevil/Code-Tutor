---
type: "THEORY"
title: "Solution 1: Temperature Converter"
---



**Key Points**:
- We use `toDouble()` to allow decimal temperatures
- Formula uses decimal division (9 / 5 works because we're in Double context)
- String interpolation displays all values

---



```kotlin
fun main() {
    println("=== Temperature Converter ===")
    println("Enter temperature in Celsius:")

    val celsius = readln().toDouble()

    val fahrenheit = (celsius * 9 / 5) + 32
    val kelvin = celsius + 273.15

    println("$celsius°C = $fahrenheit°F = ${kelvin}K")
}
```
