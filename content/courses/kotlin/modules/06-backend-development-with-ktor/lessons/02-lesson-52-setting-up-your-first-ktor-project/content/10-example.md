---
type: "EXAMPLE"
title: "🔍 Code Breakdown: How It All Works"
---


Let's trace what happens when you visit `http://localhost:8080/`:


### Understanding the `call` Object


**`call` provides access to:**
- `call.request` - Information about the incoming request
- `call.response` - The response you're building
- `call.respondText()` - Send plain text
- `call.respond()` - Send any object (will be converted to JSON)
- `call.parameters` - URL parameters
- `call.receive<T>()` - Get request body as object

---



```kotlin
get("/") {
    call.respondText("Hello")
    // 'call' is of type ApplicationCall
    // It represents the current request/response
}
```
