---
type: "EXAMPLE"
title: "Building APK"
---


Build a signed release APK:



```bash
# Build release APK (all ABIs in one file)
flutter build apk --release

# Output: build/app/outputs/flutter-apk/app-release.apk

# Build split APKs per ABI (smaller individual files)
flutter build apk --split-per-abi --release

# Outputs:
# build/app/outputs/flutter-apk/app-armeabi-v7a-release.apk  (32-bit ARM)
# build/app/outputs/flutter-apk/app-arm64-v8a-release.apk   (64-bit ARM)
# build/app/outputs/flutter-apk/app-x86_64-release.apk      (x86 emulators)

# Build with specific flavor (if using flavors)
flutter build apk --flavor prod --release

# Build with dart-define values
flutter build apk --release --dart-define-from-file=config/prod.json

# Combine options
flutter build apk --flavor prod --split-per-abi --release --dart-define-from-file=config/prod.json
```
