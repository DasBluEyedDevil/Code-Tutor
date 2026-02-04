---
type: KEY_POINT
---

- Deploy the Serverpod backend to a cloud provider (Railway, Fly.io) with proper database migration, environment variables, and health checks
- Build release APK/AAB for Android with code signing and release IPA for iOS with provisioning profiles before store submission
- Use staged rollouts (10% -> 50% -> 100%) on Google Play and TestFlight on iOS to catch issues before reaching all users
- Set up crash reporting (Sentry or Firebase Crashlytics) and analytics before launch so production issues are visible from day one
- Create a pre-launch checklist covering code freeze, QA sign-off, store metadata, privacy policy, and rollback plan
