---
type: "WARNING"
title: "Common Task Mistakes"
---


### Doing Work at Configuration Time

```kotlin
// WRONG - runs during configuration
tasks.register("bad") {
    val content = file("data.txt").readText()  // Runs immediately!
    doLast {
        println(content)
    }
}

// RIGHT - runs during execution
tasks.register("good") {
    doLast {
        val content = file("data.txt").readText()  // Runs when task executes
        println(content)
    }
}
```

### Not Declaring Inputs/Outputs

Without proper declarations, tasks always run:

```kotlin
// WRONG - no caching
tasks.register("process") {
    doLast { /* work */ }
}

// RIGHT - proper caching
tasks.register("process") {
    inputs.file("input.txt")
    outputs.file(layout.buildDirectory.file("output.txt"))
    doLast { /* work */ }
}
```

---

