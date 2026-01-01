---
type: "THEORY"
title: "Build Optimization Strategies"
---


### 1. Avoid Configuration-Time Work

```kotlin
// SLOW - runs every build
val gitHash = "git rev-parse HEAD".execute().text

// FAST - runs only when task executes
val gitHash = providers.exec {
    commandLine("git", "rev-parse", "HEAD")
}.standardOutput.asText
```

### 2. Use Lazy Configuration

```kotlin
// SLOW - creates closure immediately
tasks.register("slow") {
    inputs.file(getExpensiveFile())  // Runs at configuration
}

// FAST - deferred execution
tasks.register("fast") {
    inputs.file(providers.provider { getExpensiveFile() })
}
```

### 3. Limit Plugin Application

```kotlin
// SLOW - applies to all projects
subprojects {
    apply(plugin = "kotlin")
}

// FAST - apply only where needed via convention plugins
```

---

