---
type: "THEORY"
title: "APK vs App Bundle (AAB)"
---


**APK (Android Package Kit)**

Traditional Android format:
- Single file containing all resources for all devices
- Larger download size
- Works everywhere (Play Store, sideloading, other stores)
- Good for: Internal testing, alternative stores, direct distribution

**AAB (Android App Bundle)**

Modern format introduced by Google:
- Google Play generates optimized APKs per device
- 15-20% smaller downloads on average
- Required for new apps on Play Store (since August 2021)
- Supports dynamic delivery and on-demand features
- Good for: Play Store submissions

**When to Use Each:**

| Scenario | Format |
|----------|--------|
| Play Store submission | AAB (required) |
| Internal testing (Firebase App Distribution) | APK |
| Alternative stores (Amazon, Samsung) | APK |
| Direct download from website | APK |
| Beta testing via email | APK |

