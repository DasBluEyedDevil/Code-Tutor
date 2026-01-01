---
type: "THEORY"
title: "Building Signed Releases"
---

### Build Signed App Bundle (Recommended)

```bash
# Build signed App Bundle for Play Store
./gradlew bundleRelease

# Output: app/build/outputs/bundle/release/app-release.aab
```

### Build Signed APK

```bash
# Build signed APK for direct distribution
./gradlew assembleRelease

# Output: app/build/outputs/apk/release/app-release.apk
```

### Verify the Signature

```bash
# Verify APK signature
apksigner verify --verbose app-release.apk

# Check signature details
keytool -printcert -jarfile app-release.apk

# For App Bundles, use bundletool
bundletool validate --bundle app-release.aab
```