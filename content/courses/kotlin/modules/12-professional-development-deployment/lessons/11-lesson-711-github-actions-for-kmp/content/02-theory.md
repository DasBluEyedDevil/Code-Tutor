---
type: "THEORY"
title: "CI/CD Strategy for KMP"
---

### Build Matrix

KMP projects need to build for multiple platforms:

| Platform | Runner | Cost | Speed |
|----------|--------|------|-------|
| Android | ubuntu-latest | $0.008/min | Fast |
| iOS | macos-14 | $0.08/min | Slow |
| Desktop | ubuntu-latest | $0.008/min | Fast |
| Web | ubuntu-latest | $0.008/min | Fast |

### Recommended Workflow Structure

```
PR Created/Updated:
├── Build Android (ubuntu) - Always
├── Build iOS (macos) - Always
├── Run Tests (ubuntu) - Always
└── Lint/Static Analysis - Always

Merge to Main:
├── All above +
├── Deploy to Play Store Internal
└── Deploy to TestFlight

Release Tag:
├── Deploy to Play Store Production
└── Deploy to App Store
```