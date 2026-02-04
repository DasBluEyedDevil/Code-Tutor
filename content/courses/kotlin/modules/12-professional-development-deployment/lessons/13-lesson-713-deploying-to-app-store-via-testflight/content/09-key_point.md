---
type: "KEY_POINT"
title: "Key Takeaways"
---

**TestFlight enables beta testing before App Store release**—distribute builds to internal testers (instant) or external testers (requires Apple review). Collect feedback and crash reports before public launch.

**Upload builds via Xcode or Fastlane** using Application Loader or API calls. Builds must pass automated validation (binary checks, entitlements, provisioning) before becoming available in TestFlight.

**TestFlight has 90-day expiration** per build and 10,000 external tester limit. Use it for pre-release testing, not as permanent distribution—users should ultimately download from App Store.
