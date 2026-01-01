---
type: "EXAMPLE"
title: "Building App Bundle (AAB)"
---


Build a signed release App Bundle for Play Store:



```bash
# Build release App Bundle
flutter build appbundle --release

# Output: build/app/outputs/bundle/release/app-release.aab

# Build with flavor
flutter build appbundle --flavor prod --release

# Build with dart-define values
flutter build appbundle --release --dart-define-from-file=config/prod.json

# Full production build command
flutter build appbundle --flavor prod --release --dart-define-from-file=config/prod.json

# Check the bundle size
dir build\app\outputs\bundle\release\  # Windows
ls -la build/app/outputs/bundle/release/  # macOS/Linux
```
