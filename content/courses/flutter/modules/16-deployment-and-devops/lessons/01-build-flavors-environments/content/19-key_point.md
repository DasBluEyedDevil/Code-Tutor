---
type: "KEY_POINT"
title: "Best Practices Summary"
---


**Do:**
- Use native flavors for app identity (ID, name, icon)
- Use --dart-define for API URLs and feature flags
- Create config files per environment (dev.json, staging.json, prod.json)
- Set up IDE launch configurations for quick switching
- Use different colors/badges on dev/staging icons
- Document which flavor to use for what purpose

**Don't:**
- Put secrets in config files (use CI/CD environment variables)
- Hard-code environment checks throughout your code
- Forget to update all environments when adding new config
- Ship dev/staging builds to production stores

**File Organization:**
```
project/
├── config/
│   ├── dev.json
│   ├── staging.json
│   └── prod.json
├── lib/
│   └── config/
│       ├── app_config.dart
│       └── feature_flags.dart
├── android/
│   └── app/
│       └── src/
│           ├── dev/
│           ├── staging/
│           └── prod/
└── ios/
    └── Flutter/
        ├── Dev.xcconfig
        ├── Staging.xcconfig
        └── Prod.xcconfig
```

