---
type: "EXAMPLE"
title: "Running Different Flavors"
---


Combine --flavor with --dart-define-from-file:



```bash
# Development (full debugging, local API)
flutter run --flavor dev --dart-define-from-file=config/dev.json

# Staging (test on staging servers)
flutter run --flavor staging --dart-define-from-file=config/staging.json

# Production (release mode)
flutter run --flavor prod --dart-define-from-file=config/prod.json --release

# Build APK for staging
flutter build apk --flavor staging --dart-define-from-file=config/staging.json

# Build iOS for production
flutter build ios --flavor prod --dart-define-from-file=config/prod.json --release

# Build App Bundle for Play Store
flutter build appbundle --flavor prod --dart-define-from-file=config/prod.json
```
