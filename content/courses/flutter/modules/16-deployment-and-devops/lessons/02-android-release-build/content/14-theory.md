---
type: "THEORY"
title: "Testing Release Builds"
---


Always test release builds before submission. Release builds behave differently than debug:
- No hot reload
- No debug banner
- Optimized code paths
- ProGuard may break reflection-based code

**Testing Approaches:**

1. **Install on Physical Device:**
```bash
# Build and install directly
flutter install --release

# Or build then install manually
flutter build apk --release
adb install build/app/outputs/flutter-apk/app-release.apk
```

2. **Run in Release Mode:**
```bash
# Run release build on connected device
flutter run --release
```

3. **Test App Bundle with bundletool:**
```bash
# Install bundletool
# Download from: https://github.com/google/bundletool/releases

# Generate APKs from AAB
java -jar bundletool.jar build-apks --bundle=app-release.aab --output=app.apks --ks=my-release-key.jks --ks-key-alias=my-key-alias

# Install on connected device
java -jar bundletool.jar install-apks --apks=app.apks
```

