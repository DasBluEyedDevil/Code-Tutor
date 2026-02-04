---
type: "KEY_POINT"
title: "Key Takeaways"
---

**GitHub Actions provides free CI/CD for open source**, with macOS runners needed for iOS builds. Define workflows in `.github/workflows/` with jobs for Android, iOS, and JVM targets.

**Use official actions for common tasks**: `actions/checkout` for code, `gradle/gradle-build-action` for caching, `oven-sh/setup-bun` for JS tooling. These actions are maintained and optimized.

**Matrix builds test multiple configurations in parallel**—different OS versions, API levels, or Kotlin versions. This catches platform-specific issues before they reach production.
