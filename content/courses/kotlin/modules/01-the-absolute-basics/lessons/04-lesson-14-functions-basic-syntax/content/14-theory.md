---
type: "THEORY"
title: "Solution 1: Temperature Converter Functions"
---



**Solution Code**:

```kotlin
fun celsiusToFahrenheit(celsius: Double) = (celsius * 9 / 5) + 32

fun celsiusToKelvin(celsius: Double) = celsius + 273.15

fun fahrenheitToCelsius(fahrenheit: Double) = (fahrenheit - 32) * 5 / 9

fun main() {
    println("=== Temperature Converter ===")
    print("Enter temperature in Celsius: ")
    val celsius = readln().toDouble()

    val fahrenheit = celsiusToFahrenheit(celsius)
    val kelvin = celsiusToKelvin(celsius)

    println("\nResults:")
    println("$celsius°C = $fahrenheit°F = $kelvin K")
}
```

**Sample Output**:
