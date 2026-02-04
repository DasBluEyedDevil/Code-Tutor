---
type: "EXAMPLE"
title: "Code Breakdown"
---


### Authentication Flow


### Role-Based Access Control Flow


### Extracting User Information in Routes


---



```kotlin
// Get the authenticated user's principal
val principal = call.principal<UserPrincipal>()

// Use user information
println("User ID: ${principal.userId}")
println("Email: ${principal.email}")
println("Role: ${principal.role}")

// Use in business logic
userService.updateProfile(principal.userId, request)
```
