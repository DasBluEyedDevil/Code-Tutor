---
type: "THEORY"
title: "Topic Introduction"
---


Look at your Application.kt file. You've been manually creating and wiring dependencies:


This works for small applications, but as your app grows, manual dependency management becomes unwieldy:
- Hard to test (can't easily swap implementations)
- Violates Single Responsibility Principle (Application.kt does too much)
- Difficult to manage complex dependency graphs
- No compile-time safety for missing dependencies

**Dependency Injection** (DI) frameworks solve these problems. In this lesson, you'll learn Koin—the most popular DI framework for Kotlin.

---



```kotlin
val userRepository = UserRepositoryImpl()
val userService = UserService(userRepository)
val authService = AuthService(userRepository)
```
