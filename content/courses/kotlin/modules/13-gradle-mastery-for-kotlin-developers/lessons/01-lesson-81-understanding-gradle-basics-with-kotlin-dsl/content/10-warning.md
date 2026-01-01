---
type: "WARNING"
title: "Common Mistakes"
---


### Using Wrong File Extension

```kotlin
// WRONG: build.gradle (Groovy syntax)
apply plugin: 'kotlin'

// RIGHT: build.gradle.kts (Kotlin syntax)
plugins {
    kotlin("jvm")
}
```

### Mixing Groovy and Kotlin Syntax

If you're converting from Groovy, remember:
- Single quotes `'text'` become double quotes `"text"`
- Method calls `compile 'lib'` become `implementation("lib")`
- Assignment `group 'com.example'` becomes `group = "com.example"`

---

