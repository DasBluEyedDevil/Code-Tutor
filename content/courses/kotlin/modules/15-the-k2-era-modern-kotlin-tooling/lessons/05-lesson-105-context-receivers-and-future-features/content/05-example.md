---
type: "EXAMPLE"
title: "Context Parameters for DSLs"
---


Context parameters enable powerful DSL patterns:



```kotlin
// HTML DSL with context parameters
class HtmlBuilder {
    private val content = StringBuilder()

    fun append(text: String) {
        content.append(text)
    }

    override fun toString() = content.toString()
}

context(html: HtmlBuilder)
fun div(cssClass: String? = null, block: context(HtmlBuilder) () -> Unit) {
    html.append("<div")
    cssClass?.let { html.append(" class=\"$it\"") }
    html.append(">")
    block()
    html.append("</div>")
}

context(html: HtmlBuilder)
fun span(text: String, cssClass: String? = null) {
    html.append("<span")
    cssClass?.let { html.append(" class=\"$it\"") }
    html.append(">$text</span>")
}

context(html: HtmlBuilder)
fun p(text: String) {
    html.append("<p>$text</p>")
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
                p("Context parameters are powerful.")
            }
        }
    }
    return builder.toString()
}

// Outputs:
// <div class="container"><div class="header"><span class="title">Welcome</span></div>
// <div class="content"><p>Hello, World!</p><p>Context parameters are powerful.</p></div></div>
```
