---
type: "WARNING"
title: "Common Flavor Configuration Mistakes"
---


**1. Mixing up applicationId and package name**
- `applicationId` in build.gradle is the unique identifier on Play Store
- Package name in Java/Kotlin files stays the same
- Using `applicationIdSuffix` is safer than changing the whole ID

**2. Forgetting to create all iOS configurations**
- iOS needs Debug-X, Release-X, AND Profile-X for each flavor
- Missing Profile configuration breaks `flutter run --profile`

**3. Not updating Firebase config per flavor**
- Each flavor with a different bundle ID needs its own:
  - `google-services.json` (Android) in `android/app/src/{flavor}/`
  - `GoogleService-Info.plist` (iOS) with correct bundle ID

**4. Hardcoding URLs instead of using config**
- Even in "obvious" places like deep links or share URLs
- Always use `AppConfig.instance.apiBaseUrl` or similar

**5. Shipping staging builds to production**
- Use CI/CD to enforce correct flavor for store uploads
- Double-check build logs before uploading

