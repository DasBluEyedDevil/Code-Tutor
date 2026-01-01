---
type: "THEORY"
title: "Creating Custom Annotations"
---


### Basic Annotation


### Annotations with Parameters


### Annotation with Multiple Parameters


---



```kotlin
annotation class Route(
    val path: String,
    val method: String = "GET",
    val requiresAuth: Boolean = false
)

@Route("/users", method = "GET", requiresAuth = true)
fun getUsers() {
    println("Fetching users")
}

@Route("/users", method = "POST")
fun createUser() {
    println("Creating user")
}
```
