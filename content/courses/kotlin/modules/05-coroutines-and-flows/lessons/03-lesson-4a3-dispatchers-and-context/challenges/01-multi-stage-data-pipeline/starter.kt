import kotlinx.coroutines.*

data class RawData(val content: String)
data class ProcessedData(val words: List<String>, val wordCount: Int)
data class DisplayResult(val summary: String)

// TODO: Implement these functions with correct dispatchers

// Should run on IO dispatcher
suspend fun downloadData(url: String): RawData {
    // Simulate network delay
    delay(500)
    return RawData("Hello world this is sample data for processing")
}

// Should run on Default dispatcher
suspend fun processData(raw: RawData): ProcessedData {
    // Simulate CPU work
    delay(200)
    val words = raw.content.split(" ")
    return ProcessedData(words, words.size)
}

// TODO: Implement this function that:
// 1. Downloads data on IO
// 2. Processes data on Default
// 3. Returns result on caller's dispatcher
suspend fun pipeline(url: String): ProcessedData {
    // Your code here
    return ProcessedData(emptyList(), 0)
}

fun main() = runBlocking {
    println("Starting on ${Thread.currentThread().name}")
    val result = pipeline("https://example.com/data")
    println("Words: ${result.words}")
    println("Count: ${result.wordCount}")
    println("Finished on ${Thread.currentThread().name}")
}