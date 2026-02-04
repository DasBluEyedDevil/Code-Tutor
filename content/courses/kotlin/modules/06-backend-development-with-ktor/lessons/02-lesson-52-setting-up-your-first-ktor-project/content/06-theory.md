---
type: "THEORY"
title: "📁 Understanding the Project Structure"
---


After creation, your project should look like this:


Let's understand each piece:

- **build.gradle.kts**: Defines dependencies and build configuration
- **Application.kt**: The main file that starts your server
- **plugins/**: Modular plugin configurations
- **resources/**: Configuration files (logging, etc.)

---



```kotlin
my-first-api/
├── build.gradle.kts              # Gradle build configuration
├── settings.gradle.kts           # Project settings
├── gradle.properties             # Gradle properties
├── gradlew                       # Gradle wrapper (Unix)
├── gradlew.bat                   # Gradle wrapper (Windows)
├── src/
│   └── main/
│       ├── kotlin/
│       │   └── com/example/
│       │       ├── Application.kt      # Main entry point
│       │       └── plugins/
│       │           ├── Routing.kt      # Route definitions
│       │           └── Serialization.kt # JSON config
│       └── resources/
│           └── logback.xml             # Logging configuration
└── .gitignore
```
