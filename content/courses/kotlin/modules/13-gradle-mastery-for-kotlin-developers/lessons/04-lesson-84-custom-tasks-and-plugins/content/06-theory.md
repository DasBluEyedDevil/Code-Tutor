---
type: "THEORY"
title: "Task Inputs and Outputs"
---


### Incremental Builds

Gradle skips tasks when inputs/outputs haven't changed:

```kotlin
tasks.register("processConfig") {
    // Declare inputs
    inputs.file("config.json")
    inputs.property("version", project.version)
    
    // Declare outputs
    outputs.file(layout.buildDirectory.file("config.processed.json"))
    
    doLast {
        // Task logic
    }
}
```

### Why It Matters

- First run: Task executes
- Second run (no changes): `UP-TO-DATE` (skipped)
- Change input: Task executes again

This can save minutes on large builds!

---

