---
type: "THEORY"
title: "Exercise 3: DSL Builder"
---


**Goal**: Create a simple DSL for building configurations.

**Task**:


---



```kotlin
// TODO: Implement a configuration DSL
class ServerConfig {
    var host: String = ""
    var port: Int = 0
    val routes = mutableListOf<Route>()

    fun route(path: String, init: Route.() -> Unit) {
        // TODO
    }
}

class Route(val path: String) {
    var method: String = "GET"
    var handler: String = ""
}

fun server(init: ServerConfig.() -> Unit): ServerConfig {
    // TODO
}

fun main() {
    // Should work like this:
    val config = server {
        host = "localhost"
        port = 8080
        route("/users") {
            method = "GET"
            handler = "listUsers"
        }
        route("/users") {
            method = "POST"
            handler = "createUser"
        }
    }
}
```
