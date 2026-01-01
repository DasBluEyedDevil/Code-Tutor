---
type: "THEORY"
title: "Abstract Classes"
---


**Abstract classes** are classes that cannot be instantiated directly. They serve as blueprints for subclasses.

Use abstract classes when:
- You want to provide a common base with some implemented methods
- You want to force subclasses to implement specific methods


---



```kotlin
abstract class Vehicle(val brand: String, val model: String) {
    var speed: Int = 0

    // Abstract method (no implementation)
    abstract fun start()

    // Abstract method
    abstract fun stop()

    // Concrete method (has implementation)
    fun accelerate(amount: Int) {
        speed += amount
        println("$brand $model accelerating to $speed km/h")
    }

    fun brake(amount: Int) {
        speed -= amount
        if (speed < 0) speed = 0
        println("$brand $model slowing down to $speed km/h")
    }
}

class Car(brand: String, model: String) : Vehicle(brand, model) {
    override fun start() {
        println("$brand $model: Turning key, engine starts")
    }

    override fun stop() {
        println("$brand $model: Turning off engine")
        speed = 0
    }
}

class ElectricBike(brand: String, model: String) : Vehicle(brand, model) {
    override fun start() {
        println("$brand $model: Pressing power button, motor starts silently")
    }

    override fun stop() {
        println("$brand $model: Releasing throttle, motor stops")
        speed = 0
    }
}

fun main() {
    // val vehicle = Vehicle("Generic", "Model")  // ❌ Cannot instantiate abstract class

    val car = Car("Toyota", "Camry")
    car.start()          // Toyota Camry: Turning key, engine starts
    car.accelerate(50)   // Toyota Camry accelerating to 50 km/h
    car.accelerate(30)   // Toyota Camry accelerating to 80 km/h
    car.brake(20)        // Toyota Camry slowing down to 60 km/h
    car.stop()           // Toyota Camry: Turning off engine

    println()

    val bike = ElectricBike("Tesla", "E-Bike Pro")
    bike.start()         // Tesla E-Bike Pro: Pressing power button, motor starts silently
    bike.accelerate(25)  // Tesla E-Bike Pro accelerating to 25 km/h
    bike.stop()          // Tesla E-Bike Pro: Releasing throttle, motor stops
}
```
