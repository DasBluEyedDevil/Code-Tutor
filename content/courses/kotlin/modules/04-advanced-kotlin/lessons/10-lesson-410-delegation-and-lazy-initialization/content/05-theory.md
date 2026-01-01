---
type: "THEORY"
title: "Property Delegation"
---


Property delegation allows you to delegate the implementation of property accessors.

### Syntax


The delegate must provide `getValue` and `setValue` operators:


---



```kotlin
class DelegateClass {
    operator fun getValue(thisRef: Any?, property: KProperty<*>): String {
        return "Value of ${property.name}"
    }

    operator fun setValue(thisRef: Any?, property: KProperty<*>, value: String) {
        println("Setting ${property.name} to $value")
    }
}
```
