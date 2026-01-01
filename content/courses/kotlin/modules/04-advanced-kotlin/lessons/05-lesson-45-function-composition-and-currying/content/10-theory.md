---
type: "THEORY"
title: "Building a Simple DSL"
---


Combine everything to create a domain-specific language.

### HTML Builder DSL


---



```kotlin
@DslMarker
annotation class HtmlTagMarker

@HtmlTagMarker
abstract class Tag(val name: String) {
    val children = mutableListOf<Tag>()
    val attributes = mutableMapOf<String, String>()

    protected fun <T : Tag> initTag(tag: T, init: T.() -> Unit): T {
        tag.init()
        children.add(tag)
        return tag
    }

    fun render(): String {
        val attrs = if (attributes.isEmpty()) "" else {
            attributes.entries.joinToString(" ", " ") { "${it.key}=\"${it.value}\"" }
        }
        val content = children.joinToString("") { it.render() }
        return "<$name$attrs>$content</$name>"
    }
}

class HTML : Tag("html") {
    fun head(init: Head.() -> Unit) = initTag(Head(), init)
    fun body(init: Body.() -> Unit) = initTag(Body(), init)
}

class Head : Tag("head") {
    fun title(init: Title.() -> Unit) = initTag(Title(), init)
}

class Title : Tag("title") {
    operator fun String.unaryPlus() {
        children.add(Text(this))
    }
}

class Body : Tag("body") {
    fun h1(init: H1.() -> Unit) = initTag(H1(), init)
    fun p(init: P.() -> Unit) = initTag(P(), init)
}

class H1 : Tag("h1") {
    operator fun String.unaryPlus() {
        children.add(Text(this))
    }
}

class P : Tag("p") {
    operator fun String.unaryPlus() {
        children.add(Text(this))
    }
}

class Text(val content: String) : Tag("") {
    override fun render() = content
}

fun html(init: HTML.() -> Unit): HTML {
    val html = HTML()
    html.init()
    return html
}

// Usage: beautiful DSL!
val page = html {
    head {
        title { +"My Page" }
    }
    body {
        h1 { +"Welcome!" }
        p { +"This is a paragraph." }
        p { +"Another paragraph." }
    }
}

println(page.render())
// <html><head><title>My Page</title></head><body><h1>Welcome!</h1><p>This is a paragraph.</p><p>Another paragraph.</p></body></html>
```
