fun buildHtml(block: StringBuilder.() -> Unit): String {
    return StringBuilder().apply(block).toString()
}

fun StringBuilder.tag(name: String, content: String) {
    // Append "<name>content</name>" followed by newline
    TODO("Implement tag helper")
}

fun main() {
    val html = buildHtml {
        tag("h1", "Hello")
        tag("p", "World")
    }
    print(html)
}
