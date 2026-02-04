fun parseResponse(response: String): String {
    // Parse "status=200,data=Alice" or "status=404,error=Not Found"
    // Return formatted result string
    TODO("Implement response parsing")
}

fun main() {
    println(parseResponse("status=200,data=Alice"))
    println(parseResponse("status=404,error=Not Found"))
}
