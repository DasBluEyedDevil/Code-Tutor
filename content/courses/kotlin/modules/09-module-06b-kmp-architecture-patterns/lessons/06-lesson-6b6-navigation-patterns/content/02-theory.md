---
type: "THEORY"
title: "Navigation Challenges in KMP"
---

### Platform Differences

| Aspect | Android | iOS |
|--------|---------|-----|
| **Pattern** | Single Activity + Navigation | UINavigationController |
| **Back Button** | Hardware/gesture | Swipe gesture |
| **Deep Links** | Intent filters | Universal Links |
| **Stack** | Fragment back stack | ViewController stack |

### KMP Navigation Approaches

1. **Platform-specific navigation** - Different implementation per platform
2. **Shared navigation events** - ViewModel emits events, platform handles
3. **Compose Multiplatform Navigation** - Single navigation library
4. **Third-party libraries** - Voyager, Decompose, etc.