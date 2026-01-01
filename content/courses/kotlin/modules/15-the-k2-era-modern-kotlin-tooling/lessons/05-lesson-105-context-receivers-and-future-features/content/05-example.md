---
type: "EXAMPLE"
title: "Context Receivers for DSLs"
---


Context receivers enable powerful DSL patterns:



```kotlin
// HTML DSL with context receivers
class HtmlBuilder {
    private val content = StringBuilder()
    
    fun append(text: String) {
        content.append(text)
    }
    
    override fun toString() = content.toString()
}

context(HtmlBuilder)
fun div(cssClass: String? = null, block: context(HtmlBuilder) () -> Unit) {
    append("<div")
    cssClass?.let { append(" class=\"$it\"") }
    append(">")
    block()
    append("</div>")
}

context(HtmlBuilder)
fun span(text: String, cssClass: String? = null) {
    append("<span")
    cssClass?.let { append(" class=\"$it\"") }
    append(">$text</span>")
}

context(HtmlBuilder)
fun p(text: String) {
    append("<p>$text</p>")
}

// Usage
fun buildPage(): String {
    val builder = HtmlBuilder()
    with(builder) {
        div("container") {
            div("header") {
                span("Welcome", "title")
            }
            div("content") {
                p("Hello, World!")
                p("Context receivers are powerful.")
            }
        }
    }
    return builder.toString()
}

// Outputs:
// <div class="container"><div class="header"><span class="title">Welcome</span></div>
// <div class="content"><p>Hello, World!</p><p>Context receivers are powerful.</p></div></div>
```
