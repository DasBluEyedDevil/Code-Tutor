class Container {
    private val factories = mutableMapOf<String, () -> Any>()
    
    fun register(name: String, factory: () -> Any) {
        TODO("Store the factory")
    }
    
    fun resolve(name: String): Any {
        TODO("Invoke and return the factory result")
    }
    
    val size: Int get() = factories.size
}

fun main() {
    val container = Container()
    container.register("greeting") { "Hello, World!" }
    println(container.resolve("greeting"))
    println("Registered: ${container.size}")
}
