---
type: "KEY_POINT"
title: "Build Output Locations"
---


**APK Outputs:**
```
build/app/outputs/flutter-apk/
├── app-release.apk              (universal APK)
├── app-armeabi-v7a-release.apk  (32-bit ARM, if split)
├── app-arm64-v8a-release.apk    (64-bit ARM, if split)
└── app-x86_64-release.apk       (x86_64, if split)
```

**AAB Output:**
```
build/app/outputs/bundle/release/
└── app-release.aab
```

**Mapping Files (for crash reporting):**
```
build/app/outputs/mapping/release/
└── mapping.txt
```

**With Flavors:**
```
build/app/outputs/flutter-apk/
├── app-prod-release.apk
├── app-staging-release.apk
└── app-dev-release.apk

build/app/outputs/bundle/prodRelease/
└── app-prod-release.aab
```

