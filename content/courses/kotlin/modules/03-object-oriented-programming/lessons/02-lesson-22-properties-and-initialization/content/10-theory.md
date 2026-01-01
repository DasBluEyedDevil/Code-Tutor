---
type: "THEORY"
title: "Solution: Temperature Converter"
---



---



```kotlin
class Temperature(celsius: Double = 0.0) {
    var celsius: Double = celsius
        set(value) {
            field = value
            println("Temperature set to $value°C (${fahrenheit}°F)")
        }

    val fahrenheit: Double
        get() = celsius * 9 / 5 + 32

    fun setFahrenheit(f: Double) {
        celsius = (f - 32) * 5 / 9
    }

    fun display() {
        println("$celsius°C = $fahrenheit°F")
    }
}

fun main() {
    val temp = Temperature()

    temp.celsius = 0.0    // Temperature set to 0.0°C (32.0°F)
    temp.display()        // 0.0°C = 32.0°F

    temp.celsius = 100.0  // Temperature set to 100.0°C (212.0°F)
    temp.display()        // 100.0°C = 212.0°F

    temp.setFahrenheit(98.6)
    temp.display()        // 37.0°C = 98.6°F
}
```
