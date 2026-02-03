---
type: "THEORY"
title: "Migration Checklist"
---


### Pre-Migration Checklist

Before enabling K2, verify these items:

**1. Kotlin Version**
- Update to Kotlin 2.3.0 or later
- Update Kotlin Gradle Plugin

**2. Dependencies**
- Check all Kotlin libraries support 2.0
- Update kotlinx libraries (coroutines, serialization, etc.)
- Verify annotation processors support K2/KSP

**3. Build Plugins**
- Update Compose compiler plugin (now bundled)
- Migrate kapt to KSP where possible
- Update any custom compiler plugins

**4. IDE**
- Update to IntelliJ IDEA 2024.1+ or Android Studio Koala+
- Enable K2 IDE mode for testing

---

