---
type: "THEORY"
title: "Custom Delegates"
---


Create your own property delegates by implementing `getValue` and `setValue`.

### Read-Only Delegate


### Logged Property Delegate


### Range-Validated Delegate


---



```kotlin
class RangeValidator<T : Comparable<T>>(
    private var value: T,
    private val range: ClosedRange<T>
) {
    operator fun getValue(thisRef: Any?, property: KProperty<*>): T {
        return value
    }

    operator fun setValue(thisRef: Any?, property: KProperty<*>, newValue: T) {
        if (newValue in range) {
            value = newValue
        } else {
            throw IllegalArgumentException(
                "${property.name} must be in $range, got $newValue"
            )
        }
    }
}

fun <T : Comparable<T>> rangeValidator(initial: T, range: ClosedRange<T>) =
    RangeValidator(initial, range)

class Temperature {
    var celsius: Double by rangeValidator(0.0, -273.15..1000.0)
}

fun main() {
    val temp = Temperature()

    temp.celsius = 25.0
    println(temp.celsius)  // 25.0

    temp.celsius = 100.0
    println(temp.celsius)  // 100.0

    // temp.celsius = -300.0  // ❌ Throws exception
}
```
