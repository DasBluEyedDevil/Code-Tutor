---
type: "EXAMPLE"
title: "Project Structure"
---


A typical Gradle project looks like this:



```text
my-kotlin-project/
├── build.gradle.kts          # Main build script
├── settings.gradle.kts       # Project settings
├── gradle.properties         # Build configuration
├── gradle/
│   └── wrapper/
│       ├── gradle-wrapper.jar
│       └── gradle-wrapper.properties
├── gradlew                   # Unix wrapper script
├── gradlew.bat               # Windows wrapper script
└── src/
    ├── main/
    │   └── kotlin/
    └── test/
        └── kotlin/
```
