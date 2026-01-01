---
type: "THEORY"
title: "Interface Properties"
---


Interfaces can declare properties, but they can't have backing fields.


---



```kotlin
interface Vehicle {
    val maxSpeed: Int  // Must be overridden
    val type: String
        get() = "Generic Vehicle"  // Can provide default

    fun start()
    fun stop()
}

class Car(override val maxSpeed: Int) : Vehicle {
    override val type: String
        get() = "Car"

    override fun start() {
        println("Car starting with key")
    }

    override fun stop() {
        println("Car stopping")
    }
}

class Motorcycle(override val maxSpeed: Int) : Vehicle {
    override val type: String = "Motorcycle"  // Can also initialize directly

    override fun start() {
        println("Motorcycle starting with button")
    }

    override fun stop() {
        println("Motorcycle stopping")
    }
}

fun main() {
    val car = Car(180)
    println("${car.type} - Max Speed: ${car.maxSpeed} km/h")
    car.start()

    val bike = Motorcycle(220)
    println("${bike.type} - Max Speed: ${bike.maxSpeed} km/h")
    bike.start()
}
```
