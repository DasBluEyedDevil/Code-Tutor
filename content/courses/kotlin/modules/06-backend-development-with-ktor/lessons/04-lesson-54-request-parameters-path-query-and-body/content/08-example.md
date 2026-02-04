---
type: "EXAMPLE"
title: "🔍 Code Breakdown: Best Practices"
---


### 1. Parameter Validation Pattern


**Key points:**
- Always validate parameter types
- Use `toIntOrNull()`, `toDoubleOrNull()` for safe conversion
- Return early on validation errors
- Send appropriate status codes

### 2. Default Values Pattern


### 3. Combining Parameters Pattern


### 4. Headers as Parameters

Don't forget about headers!


---



```kotlin
get("/profile") {
    val authToken = call.request.headers["Authorization"]
    val userAgent = call.request.headers["User-Agent"]
    val acceptLanguage = call.request.headers["Accept-Language"]

    if (authToken == null) {
        call.respond(HttpStatusCode.Unauthorized, "Token required")
        return@get
    }

    // Use the header data
    val user = AuthService.getUserFromToken(authToken)
    call.respond(user)
}
```
