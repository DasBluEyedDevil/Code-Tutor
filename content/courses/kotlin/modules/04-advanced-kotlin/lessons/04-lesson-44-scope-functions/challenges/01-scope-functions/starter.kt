fun main() {
    // Use apply to add items, also to print size, let to transform
    val result = mutableListOf<String>()
        .apply {
            // Add "apple", "banana", "cherry"
        }
        .also {
            // Print "List has X items"
        }
        .let {
            // Transform to uppercase list
            TODO("Transform list")
        }
    
    println(result)
}
