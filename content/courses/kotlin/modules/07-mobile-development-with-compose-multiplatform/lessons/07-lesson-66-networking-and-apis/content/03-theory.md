---
type: "THEORY"
title: "Setup Dependencies"
---

> **Platform note:** This section shows Retrofit (Android-only) for context. For cross-platform networking with Ktor Client (recommended for Compose Multiplatform), see the Ktor Client section later in this lesson.

Add in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


Enable serialization plugin in `build.gradle.kts`:


Add internet permission in `AndroidManifest.xml` (Android target only):


---



```xml
<!-- androidMain only -->
<uses-permission android:name="android.permission.INTERNET" />
```
