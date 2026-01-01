---
type: "THEORY"
title: "Providing Delegates"
---


Create delegate providers that can initialize delegates with custom logic:


---



```kotlin
import kotlin.properties.ReadWriteProperty
import kotlin.reflect.KProperty

class ResourceDelegate<T>(private val resource: T) : ReadWriteProperty<Any?, T> {
    private var value: T = resource

    override fun getValue(thisRef: Any?, property: KProperty<*>): T {
        println("Accessing resource: ${property.name}")
        return value
    }

    override fun setValue(thisRef: Any?, property: KProperty<*>, value: T) {
        println("Updating resource: ${property.name}")
        this.value = value
    }
}

class ResourceProvider<T>(private val resource: T) {
    operator fun provideDelegate(thisRef: Any?, property: KProperty<*>): ResourceDelegate<T> {
        println("Providing delegate for ${property.name}")
        return ResourceDelegate(resource)
    }
}

class Example {
    var resource: String by ResourceProvider("Initial")
}

fun main() {
    val example = Example()
    // Output: Providing delegate for resource

    example.resource = "Updated"
    // Output: Updating resource: resource

    println(example.resource)
    // Output: Accessing resource: resource
    // Updated
}
```
