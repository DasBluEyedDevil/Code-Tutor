---
type: "THEORY"
title: "✅ Solution & Explanation"
---


Here's the complete `Routing.kt` with all exercises:


### Testing Your Solutions


### Key Concepts Demonstrated

1. **Path Parameters**: `{name}` in the route becomes accessible via `call.parameters["name"]`
2. **String Templates**: `"Hello, $name"` embeds variables in strings
3. **Null Safety**: `name.isNullOrBlank()` checks for null or empty values
4. **Libraries**: Using `LocalDateTime` and `Random` from Kotlin/Java standard library

---



```bash
# Test ping
curl http://localhost:8080/ping
# Output: pong

# Test time
curl http://localhost:8080/api/time
# Output: Current server time: 2024-11-13T15:30:45.123

# Test greeting
curl http://localhost:8080/api/greet/Alice
# Output: Hello, Alice! Welcome to our API! 👋

# Test random
curl http://localhost:8080/api/random
# Output: Your random number is: 42

# Test bonus
curl http://localhost:8080/api/greet/John/Doe
# Output: Hello, John Doe! 🎉
```
