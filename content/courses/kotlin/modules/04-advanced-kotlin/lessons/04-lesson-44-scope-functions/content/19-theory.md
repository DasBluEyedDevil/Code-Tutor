---
type: "THEORY"
title: "Exercise 3: Builder Pattern with apply"
---


**Goal**: Create a fluent builder using `apply`.

**Task**: Build an HTTP request configuration:


---



```kotlin
class HttpRequest {
    var url: String = ""
    var method: String = "GET"
    var headers: MutableMap<String, String> = mutableMapOf()
    var body: String? = null

    fun addHeader(key: String, value: String) {
        headers[key] = value
    }

    override fun toString(): String {
        return "HttpRequest(url=$url, method=$method, headers=$headers, body=$body)"
    }
}

fun main() {
    // TODO: Create POST request with headers using apply
}
```
