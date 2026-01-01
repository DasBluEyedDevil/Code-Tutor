---
type: "EXAMPLE"
title: "Setting Up buildSrc"
---


The buildSrc directory is a special Gradle feature:



```text
project-root/
├── buildSrc/
│   ├── build.gradle.kts
│   └── src/main/kotlin/
│       └── kotlin-library-conventions.gradle.kts
├── app/
│   └── build.gradle.kts
├── lib/
│   └── build.gradle.kts
└── settings.gradle.kts
```
