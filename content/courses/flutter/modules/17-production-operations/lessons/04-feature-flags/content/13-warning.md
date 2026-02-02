---
type: "WARNING"
title: "Feature Flag Best Practices and Pitfalls"
---


**Best Practices**

1. **Always Have Defaults**

```dart
// GOOD - Feature works even if fetch fails
await remoteConfig.setDefaults({
  'new_feature': false, // Safe default
});

// BAD - No defaults, feature state unknown
// (Don't do this!)
```

2. **Clean Up Old Flags**

Feature flags are technical debt. Remove them after full rollout:

```dart
// After 100% rollout, remove the flag check
// Before:
if (featureFlags.isNewCheckoutEnabled) {
  return NewCheckout();
}
return OldCheckout();

// After (when flag is 100% on):
return NewCheckout();
// Delete OldCheckout code and remove flag
```

3. **Test Both States**

Always test your app with flags enabled AND disabled:

```dart
// In tests, verify both paths
test('shows new UI when flag enabled', () {
  when(mockFlags.isNewUIEnabled).thenReturn(true);
  // Test new UI behavior
});

test('shows old UI when flag disabled', () {
  when(mockFlags.isNewUIEnabled).thenReturn(false);
  // Test old UI behavior
});
```

4. **Log Flag States**

Know which flags users have:

```dart
await analytics.setUserProperty(
  name: 'feature_new_checkout',
  value: isEnabled.toString(),
);
```

**Common Pitfalls**

1. **Flag Explosion**

Too many flags become unmaintainable:
- Limit active flags (e.g., max 20)
- Review and clean up regularly
- Document each flag's purpose and owner

2. **Nested Flag Dependencies**

```dart
// BAD - Confusing and hard to test
if (flagA && flagB) {
  if (flagC || flagD) {
    // What state is this?!
  }
}

// GOOD - Simple, independent flags
if (isFeatureXEnabled) {
  showFeatureX();
}
```

3. **Ignoring Cache Expiry**

Users may see stale values:

```dart
// Consider fetch interval in critical situations
// Emergency: Force fresh fetch
await remoteConfig.setConfigSettings(RemoteConfigSettings(
  minimumFetchInterval: Duration.zero,
));
await remoteConfig.fetchAndActivate();
```

4. **Not Handling Fetch Failures**

```dart
// GOOD - Handle failures gracefully
try {
  await remoteConfig.fetchAndActivate();
} catch (e) {
  // Log error, continue with defaults
  print('Config fetch failed: $e');
}

// BAD - Crash on failure
await remoteConfig.fetchAndActivate(); // Unhandled exception!
```

5. **UI Flicker**

Avoid showing one UI then switching:

```dart
// BAD - Shows loading, then switches
@override
void initState() {
  super.initState();
  loadFlags(); // UI flickers when flags load
}

// GOOD - Load flags before showing screen
// Or show loading state until flags ready
if (!_flagsLoaded) {
  return const LoadingScreen();
}
```

