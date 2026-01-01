---
type: "EXAMPLE"
title: "Migrating Dagger/Hilt to KSP"
---


Dagger and Hilt now support KSP:



```kotlin
// Before: kapt for Hilt
plugins {
    id("org.jetbrains.kotlin.kapt")
    id("com.google.dagger.hilt.android")
}

dependencies {
    implementation("com.google.dagger:hilt-android:2.51.1")
    kapt("com.google.dagger:hilt-android-compiler:2.51.1")
}

// After: KSP for Hilt
plugins {
    id("com.google.devtools.ksp") version "2.0.21-1.0.28"
    id("com.google.dagger.hilt.android")
}

dependencies {
    implementation("com.google.dagger:hilt-android:2.51.1")
    ksp("com.google.dagger:hilt-android-compiler:2.51.1")
}

// For plain Dagger:
dependencies {
    implementation("com.google.dagger:dagger:2.51.1")
    ksp("com.google.dagger:dagger-compiler:2.51.1")
}

// Note: Remove kapt plugin if no longer needed
// plugins {
//     id("org.jetbrains.kotlin.kapt")  // Remove this
// }
```
