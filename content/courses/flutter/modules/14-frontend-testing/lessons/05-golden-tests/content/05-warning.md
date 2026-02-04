---
type: WARNING
---

**Golden test images differ across platforms and Flutter versions.** A golden image generated on macOS will not match the same widget rendered on Linux or Windows due to subtle differences in font rendering, anti-aliasing, and text layout. This causes CI failures when developers generate goldens locally on a different OS than CI runs on.

To avoid false failures:
- Generate golden images in CI (not locally) and commit the CI-generated files as the reference
- Pin the Flutter version in CI to prevent golden drift across SDK updates
- Use `matchesGoldenFile` with a tolerance threshold when pixel-perfect matching is not required
- Run `flutter test --update-goldens` only in a controlled environment with a consistent OS and Flutter version

Also be aware that golden tests break on every intentional UI change. Keep golden test coverage focused on design system components (buttons, cards, badges) rather than full screens, so a single text change does not invalidate dozens of goldens.
