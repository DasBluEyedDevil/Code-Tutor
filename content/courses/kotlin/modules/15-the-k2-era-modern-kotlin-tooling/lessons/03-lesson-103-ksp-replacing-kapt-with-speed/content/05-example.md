---
type: "EXAMPLE"
title: "Migrating Moshi to KSP"
---


Moshi code generation also supports KSP:



```kotlin
// Before: kapt
plugins {
    id("org.jetbrains.kotlin.kapt")
}

dependencies {
    implementation("com.squareup.moshi:moshi:1.15.1")
    kapt("com.squareup.moshi:moshi-kotlin-codegen:1.15.1")
}

// After: KSP
plugins {
    id("com.google.devtools.ksp") version "2.3.4"
}

dependencies {
    implementation("com.squareup.moshi:moshi:1.15.1")
    ksp("com.squareup.moshi:moshi-kotlin-codegen:1.15.1")
}

// Usage remains the same:
@JsonClass(generateAdapter = true)
data class User(
    @Json(name = "user_id") val id: Long,
    val name: String,
    val email: String
)

// KSP generates UserJsonAdapter.kt (not .java!)
```
