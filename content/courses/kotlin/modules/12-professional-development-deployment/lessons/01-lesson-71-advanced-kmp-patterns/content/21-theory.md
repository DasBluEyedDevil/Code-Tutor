---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) ./gradlew podspec**

The CocoaPods plugin generates a Podspec file that iOS developers can use to integrate your KMP library.

---

**Question 2: B) @ObjCName**

The @ObjCName annotation lets you specify how Kotlin declarations appear to Swift/Objective-C consumers, improving API ergonomics.

---

**Question 3: B) Calling C libraries from Kotlin/Native**

Cinterop (C interoperability) generates Kotlin bindings for C headers, enabling you to use native C libraries like OpenSSL, SQLite, etc.

---

**Question 4: B) actual typealias PlatformContext = android.content.Context**

Type aliases let you map expect declarations to existing platform types without wrapping them.

---

**Question 5: C) Share code between subsets of platforms**

Hierarchical source sets allow sharing code between specific platform groups (e.g., jvmCommonMain for Android + server, nativeMain for iOS + desktop native).

---



```kotlin
// Hierarchical source set example
val jvmCommonMain by creating {
    dependsOn(commonMain)
}
val androidMain by getting {
    dependsOn(jvmCommonMain)  // Share JVM code with Android
}
val jvmMain by getting {
    dependsOn(jvmCommonMain)  // Share JVM code with server
}
```
