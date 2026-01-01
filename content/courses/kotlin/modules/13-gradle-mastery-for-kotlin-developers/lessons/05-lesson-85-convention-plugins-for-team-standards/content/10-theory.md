---
type: "THEORY"
title: "Composite Builds"
---


### Beyond buildSrc

For sharing plugins across multiple projects, use composite builds:

```
project-root/
├── build-logic/              # Separate project for plugins
│   ├── build.gradle.kts
│   ├── settings.gradle.kts
│   └── conventions/
│       ├── build.gradle.kts
│       └── src/main/kotlin/
│           └── my.conventions.gradle.kts
├── app/
├── lib/
└── settings.gradle.kts
```

### settings.gradle.kts

```kotlin
pluginManagement {
    includeBuild("build-logic")
}

include(":app", ":lib")
```

### Benefits over buildSrc

- Plugins can be published and shared
- Changes don't invalidate entire build cache
- Cleaner separation of concerns

---

