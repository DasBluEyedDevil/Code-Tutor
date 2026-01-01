---
type: "THEORY"
title: "The Testing Pyramid for KMP"
---

### Traditional Testing Pyramid

```
        /\
       /  \    E2E Tests (Slow, Expensive)
      /----\
     /      \   Integration Tests
    /--------\
   /          \  Unit Tests (Fast, Cheap)
  /____________\
```

### KMP Testing Pyramid

```
           /\
          /  \   Platform E2E (Android Espresso, iOS XCTest)
         /----\
        / Plat \ Platform Integration
       /--------\
      /  Shared  \  Shared Integration (Repository + DB)
     /-----------\
    /    Shared   \  Shared Unit Tests (Use Cases, ViewModels)
   /_______________\
```

### Key Insight: Test Shared Code on JVM

You can run most shared tests on the JVM (fast) rather than device simulators (slow):

```kotlin
// commonTest runs on JVM by default → FAST
./gradlew :shared:jvmTest          // Seconds

// iosTest runs on simulator → SLOW  
./gradlew :shared:iosSimulatorArm64Test  // Minutes
```

Write tests in `commonTest`, run them primarily on JVM, and only run platform-specific tests when necessary.