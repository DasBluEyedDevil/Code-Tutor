// Define a sealed class Result with Success and Error subclasses

fun describe(result: Result): String {
    // Use when to return "Success: <data>" or "Error: <message>"
    TODO("Implement this function")
}

fun main() {
    println(describe(Result.Success("Hello")))
    println(describe(Result.Error("Not found")))
}
