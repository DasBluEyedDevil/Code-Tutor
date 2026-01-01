open class Animal(val name: String) {
    open fun makeSound() {
        println("Some sound")
    }
}

class Dog(name: String) : Animal(name) {
    override fun makeSound() {
        println("Woof!")
    }
}

fun main() {
    val dog = Dog("Buddy")
    println("Name: ${dog.name}")
    dog.makeSound()
}