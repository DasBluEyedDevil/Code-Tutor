---
type: "THEORY"
title: "Solution 1"
---

**1. Project setup**:

```
Procfile:
web: java -jar build/libs/my-app-all.jar
```

```kotlin
// build.gradle.kts
plugins {
    kotlin("jvm") version "2.3.0"
    id("io.ktor.plugin") version "3.4.0"
}

application {
    mainClass.set("com.example.ApplicationKt")
}

ktor {
    fatJar {
        archiveFileName.set("my-app-all.jar")
    }
}
```

```kotlin
// src/main/kotlin/com/example/Application.kt
fun main() {
    val port = System.getenv("PORT")?.toInt() ?: 8080
    embeddedServer(Netty, port = port) {
        routing {
            get("/") { call.respondText("Hello from Heroku!") }
            get("/health") { call.respondText("OK") }
        }
    }.start(wait = true)
}
```

**2. Deploy**:

```bash
heroku create my-ktor-app
heroku addons:create heroku-postgresql:essential-0
git push heroku main
```

**3. Verify deployment**:

```bash
curl https://my-ktor-app.herokuapp.com/
# Output: Hello from Heroku!

curl https://my-ktor-app.herokuapp.com/health
# Output: OK

# Check logs
heroku logs --tail
```
