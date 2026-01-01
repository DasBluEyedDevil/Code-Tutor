---
type: "THEORY"
title: "Gradle Tasks"
---


Tasks are the fundamental unit of work in Gradle.

### Common Tasks

| Task | Description |
|------|-------------|
| `build` | Compiles, tests, and packages |
| `clean` | Removes build outputs |
| `test` | Runs all tests |
| `run` | Runs the application |
| `jar` | Creates a JAR file |
| `assemble` | Assembles all outputs |

### Running Tasks

```bash
# Single task
./gradlew build

# Multiple tasks
./gradlew clean build

# Task in subproject
./gradlew :app:build

# List all tasks
./gradlew tasks
```

---

