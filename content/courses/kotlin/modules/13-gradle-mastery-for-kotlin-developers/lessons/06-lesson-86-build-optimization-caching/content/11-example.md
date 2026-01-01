---
type: "EXAMPLE"
title: "Optimized Multi-Module Build"
---


Optimal configuration for large projects:



```properties
# gradle.properties

# Core optimizations
org.gradle.caching=true
org.gradle.configuration-cache=true
org.gradle.parallel=true
org.gradle.daemon=true

# Memory settings (adjust based on project size)
org.gradle.jvmargs=-Xmx8g -XX:+UseParallelGC -XX:MaxMetaspaceSize=1g

# Kotlin settings
kotlin.incremental=true
kotlin.caching.enabled=true
kotlin.parallel.tasks.in.project=true

# File system watching (real-time change detection)
org.gradle.vfs.watch=true

# Reduce console output overhead
org.gradle.console=plain
```
