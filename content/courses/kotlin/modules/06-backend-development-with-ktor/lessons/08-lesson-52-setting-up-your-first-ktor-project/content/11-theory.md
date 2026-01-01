---
type: "THEORY"
title: "🎯 Exercise: Add Your Own Endpoints"
---


Now it's your turn! Add these endpoints to your `Routing.kt`:

### Exercise Tasks

1. **Create a `/ping` endpoint** that returns "pong"

2. **Create a `/api/time` endpoint** that returns the current server time

3. **Create a `/api/greet/{name}` endpoint** that greets the user by name
   - Example: `/api/greet/Alice` → "Hello, Alice!"

4. **Create a `/api/random` endpoint** that returns a random number between 1 and 100

### Hints


Try to complete these on your own before looking at the solution!

---



```kotlin
// Hint for current time
import java.time.LocalDateTime
val now = LocalDateTime.now().toString()

// Hint for path parameter
get("/api/greet/{name}") {
    val name = call.parameters["name"]
    call.respondText("Hello, $name!")
}

// Hint for random number
import kotlin.random.Random
val number = Random.nextInt(1, 101)
```
