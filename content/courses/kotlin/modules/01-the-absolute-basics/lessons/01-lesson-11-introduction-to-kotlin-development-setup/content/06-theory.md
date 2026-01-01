---
type: "THEORY"
title: "Understanding the KMP Project Structure"
---


### Three Source Sets

Your KMP project has three main code locations:

```
shared/
├── src/
│   ├── commonMain/      ← Code that runs EVERYWHERE
│   ├── androidMain/     ← Android-specific code
│   └── iosMain/         ← iOS-specific code

androidApp/              ← Android application
iosApp/                  ← iOS application (Xcode project)
```

**commonMain**: This is where 80-90% of your code lives. Business logic, data models, networking, and even UI with Compose Multiplatform.

**androidMain/iosMain**: Platform-specific implementations when you need native APIs (camera, GPS, etc.)

### The Mental Model

❌ Old way: "I'm building an Android app" (then maybe iOS later)
✅ New way: "I'm building a mobile app" (runs on Android AND iOS)

Every lesson from here on builds apps that work on both platforms.

---

