---
type: "WARNING"
title: "Update Flow Best Practices"
---


**Do's**

1. **Check on App Launch** - Check for updates early but don't block splash screen too long:

```dart
// Good: Check in parallel with other initialization
await Future.wait([
  initializeServices(),
  checkForUpdates(),
]);
```

2. **Cache Update Decisions** - Don't nag users who dismissed optional updates:

```dart
// Store dismissed version
await prefs.setString('dismissed_version', latestVersion);

// Don't show again for same version
if (dismissedVersion == latestVersion) {
  return; // Skip prompt
}
```

3. **Handle Store Redirect Gracefully** - Users may return without updating:

```dart
// Re-check when app resumes
@override
void didChangeAppLifecycleState(AppLifecycleState state) {
  if (state == AppLifecycleState.resumed) {
    _recheckIfUpdateRequired();
  }
}
```

4. **Provide Clear Release Notes** - Help users understand why they should update

5. **Test Thoroughly** - Test update flows on real devices with real store listings

**Don'ts**

1. **Don't Block the App for Optional Updates** - Respect user choice

2. **Don't Force Updates Too Often** - Reserve for critical security fixes

3. **Don't Forget Offline Users** - Handle network failures gracefully:

```dart
try {
  await checkForUpdates();
} catch (e) {
  // Let users continue if network fails
  // unless you have cached "required update" state
}
```

4. **Don't Ignore Build Numbers** - Version 2.0.0+100 and 2.0.0+101 are different releases

5. **Don't Hardcode Store URLs** - Use platform detection:

```dart
// Good
final url = Platform.isIOS 
    ? 'https://apps.apple.com/app/id123' 
    : 'https://play.google.com/store/apps/details?id=com.app';

// Bad
const url = 'https://play.google.com/store/apps/details?id=com.app';
```

**Security Considerations**

- Always use HTTPS for version check endpoints
- Validate server responses
- Consider certificate pinning for critical apps
- Don't trust client-reported versions for security decisions

