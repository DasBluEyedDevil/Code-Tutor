sealed class Result {
    data class Success(val data: String) : Result()
    data class Error(val message: String) : Result()
}

fun describe(result: Result): String {
    return when (result) {
        is Result.Success -> "Success: ${result.data}"
        is Result.Error -> "Error: ${result.message}"
    }
}

fun main() {
    println(describe(Result.Success("Hello")))
    println(describe(Result.Error("Not found")))
}
