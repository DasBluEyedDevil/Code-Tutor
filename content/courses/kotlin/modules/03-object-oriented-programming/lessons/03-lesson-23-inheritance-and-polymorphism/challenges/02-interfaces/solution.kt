interface Drivable {
    fun drive()
}

class Car : Drivable {
    override fun drive() {
        println("Driving a car with engine")
    }
}

class Bicycle : Drivable {
    override fun drive() {
        println("Riding a bicycle with pedals")
    }
}

fun main() {
    val car: Drivable = Car()
    val bicycle: Drivable = Bicycle()
    car.drive()
    bicycle.drive()
}