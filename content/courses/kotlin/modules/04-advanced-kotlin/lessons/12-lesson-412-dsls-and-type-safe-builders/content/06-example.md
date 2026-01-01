---
type: "EXAMPLE"
title: "HTML DSL Example"
---


Let's build a complete HTML DSL!

### Basic Structure


### Using the HTML DSL


### Enhanced HTML with Attributes


---



```kotlin
class EnhancedDiv : Tag("div") {
    var id: String
        get() = ""
        set(value) { attribute("id", value) }

    var cssClass: String
        get() = ""
        set(value) { attribute("class", value) }

    fun p(action: EnhancedP.() -> Unit) = initTag(EnhancedP(), action)
}

class EnhancedP : Tag("p") {
    var style: String
        get() = ""
        set(value) { attribute("style", value) }

    fun text(content: String) = initTag(Text(content)) {}
}

fun enhancedHtml(action: EnhancedHTML.() -> Unit): EnhancedHTML {
    val html = EnhancedHTML()
    html.action()
    return html
}

class EnhancedHTML : Tag("html") {
    fun body(action: EnhancedBody.() -> Unit) = initTag(EnhancedBody(), action)
}

class EnhancedBody : Tag("body") {
    fun div(action: EnhancedDiv.() -> Unit) = initTag(EnhancedDiv(), action)
}

fun main() {
    val page = enhancedHtml {
        body {
            div {
                id = "main"
                cssClass = "container"

                p {
                    style = "color: blue;"
                    text("Styled paragraph")
                }
            }
        }
    }

    println(page)
}
```
