fun main() {
    val result = mutableListOf<String>()
        .apply {
            add("apple")
            add("banana")
            add("cherry")
        }
        .also {
            println("List has ${it.size} items")
        }
        .let {
            it.map { item -> item.uppercase() }
        }
    
    println(result)
}
