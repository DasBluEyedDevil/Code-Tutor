fun parseQuery(url: String): Map<String, String> {
    val queryString = url.substringAfter("?", "")
    if (queryString.isEmpty()) return emptyMap()
    return queryString.split("&").associate { param ->
        val (key, value) = param.split("=")
        key to value
    }
}

fun main() {
    val params = parseQuery("/api/users?name=Alice&age=25")
    for ((key, value) in params.toSortedMap()) {
        println("$key=$value")
    }
}
