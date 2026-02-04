---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Run all target tests in CI**—`./gradlew allTests` runs commonTest on JVM, Android, iOS simulator, and other configured targets. This ensures multiplatform consistency before merging code.

**Cache Gradle dependencies and build outputs** in CI to reduce build times. GitHub Actions cache with `gradle/gradle-build-action` can cut build times by 50% or more.

**Fail builds on test failures or coverage drops**. Integrate coverage tools like Kover to enforce minimum coverage thresholds, preventing untested code from reaching production.
