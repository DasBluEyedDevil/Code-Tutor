---
type: WARNING
---

**Never store authentication tokens in SharedPreferences.** SharedPreferences data is stored as plain text on the device file system. On rooted/jailbroken devices, any app can read it. On Android, it is stored in an XML file; on iOS, in a plist.

```dart
// WRONG - tokens visible in plain text on device
await prefs.setString('auth_token', token);

// RIGHT - encrypted storage
final storage = FlutterSecureStorage();
await storage.write(key: 'auth_token', value: token);
```

Use `flutter_secure_storage`, which stores data in Android Keystore and iOS Keychain -- both are hardware-backed encrypted storage. This applies to access tokens, refresh tokens, API keys, and any user credentials. SharedPreferences is fine for non-sensitive preferences like theme mode or language selection.
