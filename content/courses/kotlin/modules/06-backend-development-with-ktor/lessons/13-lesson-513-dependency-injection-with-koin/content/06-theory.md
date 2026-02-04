---
type: "THEORY"
title: "Dependency Injection in Routes"
---


You can inject dependencies directly in route functions:


Update routing setup:

---



```kotlin
routing {
    authRoutes()    // No need to pass dependencies!
    userRoutes()
    adminRoutes()
}
```
