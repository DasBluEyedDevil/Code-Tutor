---
type: "THEORY"
title: "Dependency Configurations"
---


### Understanding Configurations

Configurations determine how dependencies are used:

| Configuration | Visible to Consumers? | Included at Runtime? | Use Case |
|--------------|----------------------|---------------------|----------|
| `implementation` | No | Yes | Internal dependencies |
| `api` | Yes | Yes | Part of public API |
| `compileOnly` | No | No | Compile-time only (annotations) |
| `runtimeOnly` | No | Yes | Runtime only (drivers) |
| `testImplementation` | No | Yes (tests) | Test dependencies |

### Example

```kotlin
dependencies {
    // Internal - consumers don't see it
    implementation("com.squareup.okhttp3:okhttp:4.12.0")
    
    // API - exposed to library consumers
    api("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.9.0")
    
    // Annotation processor - compile only
    compileOnly("org.projectlombok:lombok:1.18.30")
    
    // Database driver - runtime only
    runtimeOnly("org.postgresql:postgresql:42.7.4")
    
    // Testing
    testImplementation(kotlin("test"))
}
```

---

