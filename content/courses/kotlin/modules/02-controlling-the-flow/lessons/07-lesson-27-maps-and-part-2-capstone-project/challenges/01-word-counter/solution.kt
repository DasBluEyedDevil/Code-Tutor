fun main() {
    val words = listOf("apple", "banana", "apple", "cherry", "banana", "apple")
    val counts = mutableMapOf<String, Int>()
    for (word in words) {
        counts[word] = counts.getOrDefault(word, 0) + 1
    }
    for ((word, count) in counts.toSortedMap()) {
        println("$word: $count")
    }
}
