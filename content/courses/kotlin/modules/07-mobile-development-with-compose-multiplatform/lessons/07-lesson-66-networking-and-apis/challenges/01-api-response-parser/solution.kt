fun parseResponse(response: String): String {
    val fields = response.split(",").associate { field ->
        val (key, value) = field.split("=", limit = 2)
        key.trim() to value.trim()
    }
    val status = fields["status"] ?: "unknown"
    return if (fields.containsKey("data")) {
        "Status: $status, Data: ${fields["data"]}"
    } else {
        "Status: $status, Error: ${fields["error"]}"
    }
}

fun main() {
    println(parseResponse("status=200,data=Alice"))
    println(parseResponse("status=404,error=Not Found"))
}
