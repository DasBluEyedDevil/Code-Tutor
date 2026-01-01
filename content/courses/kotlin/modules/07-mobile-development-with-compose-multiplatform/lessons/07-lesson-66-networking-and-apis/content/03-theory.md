---
type: "THEORY"
title: "Setup Dependencies"
---


Add in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


Enable serialization plugin in `build.gradle.kts`:


Add internet permission in `AndroidManifest.xml`:


---



```xml
<uses-permission android:name="android.permission.INTERNET" />
```
