---
type: "THEORY"
title: "CI Strategy for KMP"
---

### Test Matrix

| Test Type | Where to Run | Frequency |
|-----------|-------------|------------|
| Shared unit tests | JVM (Ubuntu) | Every commit |
| Android unit tests | JVM (Ubuntu) | Every commit |
| Android instrumented | Emulator | PRs, main branch |
| iOS unit tests | macOS | PRs, main branch |
| iOS UI tests | Simulator | Nightly |
| Desktop tests | JVM | PRs |

### Cost Optimization

- **JVM tests are cheap** - run on Linux, fast
- **iOS tests are expensive** - require macOS runners ($0.08/min vs $0.008/min)
- **Instrumented tests are slow** - emulator startup time

Strategy: Run most tests on JVM, reserve platform-specific tests for PRs and main.