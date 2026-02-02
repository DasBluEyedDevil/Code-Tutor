import kotlinx.coroutines.*

data class RawData(val content: String)
data class ProcessedData(val words: List<String>, val wordCount: Int)

suspend fun downloadData(url: String): RawData = withContext(Dispatchers.IO) {
    println("Downloading on ${Thread.currentThread().name}")
    delay(500)
    RawData("Hello world this is sample data for processing")
}

suspend fun processData(raw: RawData): ProcessedData = withContext(Dispatchers.Default) {
    println("Processing on ${Thread.currentThread().name}")
    delay(200)
    val words = raw.content.split(" ")
    ProcessedData(words, words.size)
}

suspend fun pipeline(url: String): ProcessedData {
    val raw = downloadData(url)      // Runs on IO
    val processed = processData(raw)  // Runs on Default
    return processed                  // Returns on caller's dispatcher
}

fun main() = runBlocking {
    println("Starting on ${Thread.currentThread().name}")
    val result = pipeline("https://example.com/data")
    println("Words: ${result.words}")
    println("Count: ${result.wordCount}")
    println("Finished on ${Thread.currentThread().name}")
}