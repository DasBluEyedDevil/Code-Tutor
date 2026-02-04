class Container {
    private val factories = mutableMapOf<String, () -> Any>()
    
    fun register(name: String, factory: () -> Any) {
        factories[name] = factory
    }
    
    fun resolve(name: String): Any {
        return factories[name]?.invoke()
            ?: error("No registration found for '$name'")
    }
    
    val size: Int get() = factories.size
}

fun main() {
    val container = Container()
    container.register("greeting") { "Hello, World!" }
    println(container.resolve("greeting"))
    println("Registered: ${container.size}")
}
