---
type: "EXAMPLE"
title: "gradle.properties"
---


The properties file configures Gradle behavior:



```properties
# gradle.properties

# JVM settings
org.gradle.jvmargs=-Xmx2g -XX:+UseParallelGC

# Enable parallel builds
org.gradle.parallel=true

# Enable build cache
org.gradle.caching=true

# Enable configuration cache
org.gradle.configuration-cache=true

# Kotlin settings
kotlin.code.style=official
```
