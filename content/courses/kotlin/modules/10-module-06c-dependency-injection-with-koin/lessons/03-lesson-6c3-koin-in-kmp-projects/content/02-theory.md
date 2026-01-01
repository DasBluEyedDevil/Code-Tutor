---
type: "THEORY"
title: "KMP Module Structure"
---

In KMP projects, organize Koin modules by layer and platform:

```
shared/
├── src/
│   ├── commonMain/
│   │   └── kotlin/
│   │       └── di/
│   │           ├── CommonModule.kt      # Shared dependencies
│   │           ├── DomainModule.kt      # Use cases
│   │           └── KoinInit.kt          # Initialization
│   ├── androidMain/
│   │   └── kotlin/
│   │       └── di/
│   │           └── PlatformModule.android.kt
│   └── iosMain/
│       └── kotlin/
│           └── di/
│               └── PlatformModule.ios.kt
```

### The Pattern

1. **commonMain**: Shared interfaces and common implementations
2. **androidMain/iosMain**: Platform-specific implementations
3. **Expect/Actual**: Bridge for platform modules