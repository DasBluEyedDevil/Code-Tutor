---
type: "EXAMPLE"
title: "Common Gradle Tasks"
---

Gradle tasks are run from the command line using ./gradlew. Common tasks include: build (compile, test, package), test (run tests), run (execute app), dependencies (show dependency tree), clean (delete build folder), and tasks (list available tasks). You can also create custom tasks.

```kotlin
# Common Gradle commands:
./gradlew build        # Compiles, tests, and packages
./gradlew test         # Runs all tests
./gradlew run          # Runs your application
./gradlew dependencies # Shows dependency tree
./gradlew clean        # Deletes build directory
./gradlew tasks        # Lists all available tasks

# Creating a custom task in build.gradle.kts:
tasks.register("hello") {
    doLast {
        println("Hello from Gradle!")
    }
}

# Run with:
./gradlew hello
```
