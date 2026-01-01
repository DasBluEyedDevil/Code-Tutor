---
type: "THEORY"
title: "let: Transform or Process"
---


`let` takes the object as `it` and returns the lambda result.

### Basic Usage


### Primary Use Case: Null Safety


### Transforming Nullable Values


### Chaining Transformations


### Real-World Example: API Response Processing


---



```kotlin
data class ApiResponse(val data: String?, val error: String?)

fun processResponse(response: ApiResponse): String {
    return response.data?.let { data ->
        // Process successful response
        data.uppercase()
    } ?: response.error?.let { error ->
        // Handle error
        "Error: $error"
    } ?: "Unknown error"
}

val success = ApiResponse("hello", null)
println(processResponse(success))  // HELLO

val failure = ApiResponse(null, "Not found")
println(processResponse(failure))  // Error: Not found
```
