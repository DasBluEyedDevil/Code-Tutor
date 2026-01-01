---
type: "THEORY"
title: "Running with Config Files"
---


**Run commands:**
```bash
# Development
flutter run --dart-define-from-file=config/dev.json

# Staging
flutter run --dart-define-from-file=config/staging.json

# Production build
flutter build apk --dart-define-from-file=config/prod.json
```

**Benefits:**
- All config in one place per environment
- Easy to see differences between environments
- Can be version controlled (except secrets!)
- No long command lines

**Security Note:**
Don't put real secrets in these files! Use CI/CD environment variables for:
- API keys
- Signing certificates
- Service account credentials

