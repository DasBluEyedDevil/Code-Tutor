---
type: "KEY_POINT"
title: "Flutter Flavors Overview"
---


**Flutter provides two main approaches:**

1. **--dart-define flags** - Compile-time constants
   - Simple, works everywhere
   - Good for API URLs, feature flags
   - Values baked into the binary

2. **Native flavors** - Platform-specific configurations
   - Android: productFlavors in build.gradle
   - iOS: Schemes and configurations in Xcode
   - Required for different app IDs, icons, names

**Most apps need BOTH:**
- Native flavors for app identity (ID, name, icon)
- --dart-define for runtime configuration (URLs, flags)

