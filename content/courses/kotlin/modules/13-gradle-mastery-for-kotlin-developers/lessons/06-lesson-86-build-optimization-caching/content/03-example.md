---
type: "EXAMPLE"
title: "Essential gradle.properties"
---


Optimize builds with these properties:



```properties
# gradle.properties

# Enable build cache
org.gradle.caching=true

# Enable configuration cache
org.gradle.configuration-cache=true

# Run tasks in parallel
org.gradle.parallel=true

# Keep daemon running
org.gradle.daemon=true

# JVM settings
org.gradle.jvmargs=-Xmx4g -XX:+UseParallelGC

# Kotlin settings
kotlin.incremental=true
kotlin.caching.enabled=true
```
