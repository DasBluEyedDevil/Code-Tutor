---
type: WARNING
---

**Integration tests require a running device or emulator -- they cannot run headless.** Unlike unit tests and widget tests, integration tests use `flutter test integration_test/` which needs a connected device. If you run them in CI without an emulator setup step, the test command will fail with "No connected devices."

CI setup requirements:
- **Android**: Configure an Android emulator in CI (GitHub Actions: `reactivecircus/android-emulator-runner`)
- **iOS**: Requires a macOS runner with Xcode and iOS Simulator
- **Web**: Use `chromedriver` for Chrome-based integration testing

Integration tests are also significantly slower than widget tests (seconds per test vs. milliseconds). Run them as a separate CI job that does not block your fast feedback loop. Do not put business logic assertions in integration tests -- those belong in unit tests. Use integration tests only for verifying full user flows: navigation, data persistence across screens, and platform service interactions.
