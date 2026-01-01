---
type: "KEY_POINT"
title: "Best Practices for Crash Reporting"
---


**1. Don't Report in Debug Mode**

Use `kReleaseMode` or `kDebugMode` to avoid polluting your dashboard during development.

**2. Set Meaningful Custom Keys**

Good keys:
- `subscription_tier`: "premium" (helps prioritize premium user issues)
- `screen`: "checkout" (identifies where crashes happen)
- `feature_flag_x`: "enabled" (identifies feature-specific crashes)

Bad keys:
- `timestamp`: "2024-01-01" (already tracked)
- `device`: "iPhone" (already tracked)

**3. Use Breadcrumbs Strategically**

Log:
- Navigation events
- User actions (button taps, form submissions)
- API calls (request/response)
- State changes (login, logout)

Don't log:
- Every widget rebuild
- High-frequency events (scroll position)

**4. Test Your Setup**

```dart
// Trigger a test crash (ONLY in debug/staging)
if (kDebugMode) {
  FirebaseCrashlytics.instance.crash(); // Force crash
}

// Or throw a caught exception
FirebaseCrashlytics.instance.recordError(
  Exception('Test error'),
  StackTrace.current,
  reason: 'Testing crash reporting',
);
```

**5. Monitor Your Crash-Free Rate**

Set up alerts for:
- Crash-free rate drops below 99%
- New crash affecting >100 users
- Crash rate spikes after a release

