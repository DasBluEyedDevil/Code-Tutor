---
type: "THEORY"
title: "@DslMarker - Scope Control"
---


`@DslMarker` prevents implicit receiver mixing in nested DSLs.

### The Problem Without @DslMarker


### Solution with @DslMarker


**Benefits**:
- Prevents calling outer scope functions
- Makes DSL structure clearer
- Reduces errors

---



```kotlin
@DslMarker
annotation class HtmlTagMarker

@HtmlTagMarker
abstract class MarkedTag(val name: String) {
    private val children = mutableListOf<MarkedTag>()

    protected fun <T : MarkedTag> initTag(tag: T, action: T.() -> Unit): T {
        tag.action()
        children.add(tag)
        return tag
    }

    fun render(): String {
        val childrenHtml = children.joinToString("") { it.render() }
        return if (children.isEmpty()) {
            "<$name />"
        } else {
            "<$name>$childrenHtml</$name>"
        }
    }
}

@HtmlTagMarker
class MarkedHTML : MarkedTag("html") {
    fun body(action: MarkedBody.() -> Unit) = initTag(MarkedBody(), action)
}

@HtmlTagMarker
class MarkedBody : MarkedTag("body") {
    fun div(action: MarkedDiv.() -> Unit) = initTag(MarkedDiv(), action)
}

@HtmlTagMarker
class MarkedDiv : MarkedTag("div") {
    fun p(action: MarkedP.() -> Unit) = initTag(MarkedP(), action)
}

@HtmlTagMarker
class MarkedP : MarkedTag("p")

fun main() {
    val page = MarkedHTML().apply {
        body {
            div {
                p { }
                // body { }  // ❌ Error: can't call body from here
            }
        }
    }

    println(page.render())
}
```
