---
type: "WARNING"
title: "Never Trust Client Input"
---

**All input from clients is untrusted and potentially malicious**. Even if you validate on the client, always revalidate on the server—client-side validation can be bypassed.

**SQL injection via string concatenation**:
```kotlin
// NEVER DO THIS
val query = "SELECT * FROM users WHERE email = '$email'"
database.execute(query)  // Attacker sends: ' OR '1'='1
```

Always use parameterized queries:
```kotlin
// Correct
database.execute(
    "SELECT * FROM users WHERE email = ?",
    email
)
```

**Mass assignment vulnerabilities** occur when you blindly copy request data to domain objects:
```kotlin
// Dangerous
val user = request.receive<User>()
database.save(user)  // Attacker includes "isAdmin: true" in JSON
```

Instead, explicitly map allowed fields:
```kotlin
// Safe
val request = call.receive<UserUpdateRequest>()
user.copy(
    name = request.name,
    email = request.email
    // isAdmin explicitly excluded
)
```

**Always validate** types, formats, ranges, and business rules server-side before trusting input.
