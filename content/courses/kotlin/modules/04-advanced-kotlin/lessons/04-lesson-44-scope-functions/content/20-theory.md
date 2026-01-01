---
type: "THEORY"
title: "Solution 3: Builder Pattern with apply"
---



**Explanation**:
- `apply` configures the object and returns it
- Making `addHeader` return `this` with `apply` enables chaining
- `also` adds logging without breaking the chain
- Fluent, readable builder pattern

---



```kotlin
class HttpRequest {
    var url: String = ""
    var method: String = "GET"
    var headers: MutableMap<String, String> = mutableMapOf()
    var body: String? = null

    fun addHeader(key: String, value: String) = apply {
        headers[key] = value
    }

    override fun toString(): String {
        return "HttpRequest(url=$url, method=$method, headers=$headers, body=$body)"
    }
}

fun main() {
    // Using apply for configuration
    val request = HttpRequest().apply {
        url = "https://api.example.com/users"
        method = "POST"
        body = """{"name": "Alice", "email": "alice@example.com"}"""
    }.apply {
        addHeader("Content-Type", "application/json")
        addHeader("Authorization", "Bearer token123")
    }

    println(request)
    // HttpRequest(url=https://api.example.com/users, method=POST,
    // headers={Content-Type=application/json, Authorization=Bearer token123},
    // body={"name": "Alice", "email": "alice@example.com"})

    // Alternative: chaining with fluent API
    val request2 = HttpRequest()
        .apply {
            url = "https://api.example.com/products"
            method = "PUT"
            body = """{"id": 1, "price": 99.99}"""
        }
        .addHeader("Content-Type", "application/json")
        .addHeader("Accept", "application/json")
        .also {
            println("\nCreated request: ${it.method} ${it.url}")
        }

    println(request2)
}
```
