---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Test shared business logic in commonTest where it runs on all platforms**. Testing in commonMain ensures your core logic works identically on Android, iOS, JVM, and other targets.

**Focus testing effort on shared code, not platform-specific UI**—shared code has the highest ROI for testing since bugs affect all platforms. Platform code often wraps framework-specific UI that's better tested with integration/manual testing.

**The testing pyramid applies to KMP**: many fast unit tests in commonTest, fewer integration tests with platform specifics, minimal UI tests. This balance maximizes coverage while keeping CI fast.
