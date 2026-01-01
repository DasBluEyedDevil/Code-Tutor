---
type: "EXAMPLE"
title: "🔧 Writing Your First Server Code"
---


### Step 1: Create the Main Application File

Create `src/main/kotlin/com/example/Application.kt`:


**Let's break this down:**

- **embeddedServer**: Runs Ktor inside your application (no separate Tomcat/Jetty)
- **CIO**: Coroutine-based I/O engine (lightweight and perfect for learning)
- **port = 8080**: Your server will be accessible at `http://localhost:8080`
- **host = "0.0.0.0"**: Accept connections from any network interface

- This is an **extension function** on the `Application` class
- It's where you configure all your plugins and routes

### Step 2: Configure JSON Serialization

Create `src/main/kotlin/com/example/plugins/Serialization.kt`:


**What this does:**
- **ContentNegotiation**: Plugin that handles converting Kotlin objects ↔ JSON
- **json()**: Configure JSON serialization settings
- **prettyPrint**: Makes the JSON output readable (with indentation)

### Step 3: Define Your First Routes

Create `src/main/kotlin/com/example/plugins/Routing.kt`:


**Understanding the routing:**

- **routing { }**: Block where you define all routes
- **get("/")**: Handle GET requests to the root path
- **call**: Represents the current HTTP request/response
- **respondText()**: Send plain text response

### Step 4: Add Logging Configuration

Create `src/main/resources/logback.xml`:


This configures logging so you can see what your server is doing.

---



```xml
<configuration>
    <appender name="STDOUT" class="ch.qos.logback.core.ConsoleAppender">
        <encoder>
            <pattern>%d{HH:mm:ss.SSS} [%thread] %-5level %logger{36} - %msg%n</pattern>
        </encoder>
    </appender>

    <root level="INFO">
        <appender-ref ref="STDOUT"/>
    </root>

    <logger name="io.ktor" level="DEBUG"/>
</configuration>
```
