---
type: "THEORY"
title: "Task Dependencies"
---


### Ordering Tasks

Control when tasks run relative to each other:

```kotlin
// Task dependency - must run before
tasks.named("compileKotlin") {
    dependsOn("generateBuildInfo")
}

// Must run after (if both are in graph)
tasks.register("cleanup") {
    mustRunAfter("build")
}

// Should run after (weaker ordering)
tasks.register("report") {
    shouldRunAfter("test")
}

// Finalized by (always runs after, even on failure)
tasks.named("test") {
    finalizedBy("generateReport")
}
```

### Task Graph

```
compileKotlin
    |
    +-- dependsOn --> generateBuildInfo
    |
    v
test
    |
    +-- finalizedBy --> generateReport
```

---

