fun parseQuery(url: String): Map<String, String> {
    // Extract query parameters from URL into a map
    TODO("Implement query parameter parsing")
}

fun main() {
    val params = parseQuery("/api/users?name=Alice&age=25")
    for ((key, value) in params.toSortedMap()) {
        println("$key=$value")
    }
}
