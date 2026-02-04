fun buildHtml(block: StringBuilder.() -> Unit): String {
    return StringBuilder().apply(block).toString()
}

fun StringBuilder.tag(name: String, content: String) {
    appendLine("<$name>$content</$name>")
}

fun main() {
    val html = buildHtml {
        tag("h1", "Hello")
        tag("p", "World")
    }
    print(html)
}
