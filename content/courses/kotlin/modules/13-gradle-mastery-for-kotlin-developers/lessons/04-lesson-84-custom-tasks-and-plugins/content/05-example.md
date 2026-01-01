---
type: "EXAMPLE"
title: "Typed Tasks"
---


Use built-in task types for common operations:



```kotlin
// Copy task
tasks.register<Copy>("copyDocs") {
    from("docs")
    into(layout.buildDirectory.dir("documentation"))
    include("**/*.md")
    rename { it.replace(".md", ".txt") }
}

// Delete task
tasks.register<Delete>("cleanGenerated") {
    delete(layout.buildDirectory.dir("generated"))
}

// Zip task
tasks.register<Zip>("packageDist") {
    from(layout.buildDirectory.dir("dist"))
    archiveFileName.set("app-${project.version}.zip")
    destinationDirectory.set(layout.buildDirectory.dir("packages"))
}

// Exec task
tasks.register<Exec>("npmInstall") {
    workingDir = file("frontend")
    commandLine("npm", "install")
}
```
