fun calculateLayout(items: List<String>, containerHeight: Int) {
    if (items.isEmpty()) return
    val itemHeight = containerHeight / items.size
    items.forEachIndexed { index, item ->
        println("Item $item: y=${index * itemHeight}, height=$itemHeight")
    }
}

fun main() {
    calculateLayout(listOf("A", "B", "C"), 300)
}
