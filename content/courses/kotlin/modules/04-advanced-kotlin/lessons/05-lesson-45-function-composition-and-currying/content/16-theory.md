---
type: "THEORY"
title: "Solution 3: DSL Builder"
---



**Explanation**:
- DSL provides type-safe configuration
- Lambda with receiver (`init: ServerConfig.() -> Unit`) enables clean syntax
- Nested structures through builder pattern
- Reads almost like a configuration file!

---



```kotlin
class ServerConfig {
    var host: String = ""
    var port: Int = 0
    val routes = mutableListOf<Route>()

    fun route(path: String, init: Route.() -> Unit) {
        val route = Route(path)
        route.init()
        routes.add(route)
    }

    override fun toString(): String {
        return """
            Server Configuration:
              Host: $host
              Port: $port
              Routes:
                ${routes.joinToString("\n    ") { it.toString() }}
        """.trimIndent()
    }
}

class Route(val path: String) {
    var method: String = "GET"
    var handler: String = ""

    override fun toString() = "$method $path -> $handler"
}

fun server(init: ServerConfig.() -> Unit): ServerConfig {
    val config = ServerConfig()
    config.init()
    return config
}

fun main() {
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

        route("/users/{id}") {
            method = "GET"
            handler = "getUser"
        }

        route("/users/{id}") {
            method = "PUT"
            handler = "updateUser"
        }

        route("/users/{id}") {
            method = "DELETE"
            handler = "deleteUser"
        }
    }

    println(config)
    /*
    Server Configuration:
      Host: localhost
      Port: 8080
      Routes:
        GET /users -> listUsers
        POST /users -> createUser
        GET /users/{id} -> getUser
        PUT /users/{id} -> updateUser
        DELETE /users/{id} -> deleteUser
    */
}
```
