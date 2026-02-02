---
type: "EXAMPLE"
title: "Setting Up Dependencies for Authentication"
---

Before building the registration flow, you need to add the required packages to your Flutter project. These packages handle secure storage, form validation, and communication with Serverpod.

**Step 1: Add Dependencies to pubspec.yaml**

Open your `pubspec.yaml` and add these dependencies:

```yaml
dependencies:
  flutter:
    sdk: flutter
  
  # Serverpod client (generated for your project)
  your_app_client:
    path: ../your_app_server/your_app_client
  
  # Serverpod Flutter integration with auth support
  serverpod_flutter: ^2.0.0
  serverpod_auth_shared_flutter: ^2.0.0
  
  # Secure storage for auth tokens
  flutter_secure_storage: ^9.0.0
  
  # State management (we will use Riverpod)
  flutter_riverpod: ^2.4.0
  
  # Form validation helpers
  email_validator: ^2.1.17
```

Run `flutter pub get` to install the packages.

**Step 2: Understanding Each Package**

**flutter_secure_storage** stores sensitive data using:
- iOS: Keychain Services (encrypted by the Secure Enclave)
- Android: EncryptedSharedPreferences (AES-256 encryption)
- This is critical because regular SharedPreferences stores data in plain text!

**serverpod_auth_shared_flutter** provides:
- Pre-built authentication state management
- Session handling and token refresh
- Integration with Serverpod's auth module

**email_validator** provides:
- RFC 5322 compliant email validation
- No need to write complex regex patterns yourself

**Step 3: Configure flutter_secure_storage for Android**

For Android, you need to set a minimum SDK version. Open `android/app/build.gradle`:

```gradle
android {
    // ...
    defaultConfig {
        // ...
        minSdkVersion 23  // Required for EncryptedSharedPreferences
    }
}
```

**Step 4: Configure flutter_secure_storage for iOS**

For iOS, you need to add Keychain sharing capability. Open `ios/Runner/Runner.entitlements` and add:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>keychain-access-groups</key>
    <array>
        <string>$(AppIdentifierPrefix)com.yourcompany.yourapp</string>
    </array>
</dict>
</plist>
```

**Step 5: Create the Secure Storage Service**

Create a dedicated service to handle all secure storage operations. This abstraction makes testing easier and centralizes storage logic.

Create `lib/services/secure_storage_service.dart`:

```dart
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

/// Service for securely storing sensitive data like auth tokens.
/// Uses platform-specific encryption (Keychain on iOS, EncryptedSharedPreferences on Android).
class SecureStorageService {
  // Keys for stored values
  static const String _authTokenKey = 'auth_token';
  static const String _refreshTokenKey = 'refresh_token';
  static const String _userIdKey = 'user_id';
  static const String _userEmailKey = 'user_email';
  
  // Configure secure storage with recommended options
  final FlutterSecureStorage _storage = const FlutterSecureStorage(
    aOptions: AndroidOptions(
      encryptedSharedPreferences: true,
      // Use strong encryption
      keyCipherAlgorithm: KeyCipherAlgorithm.RSA_ECB_OAEPwithSHA_256andMGF1Padding,
      storageCipherAlgorithm: StorageCipherAlgorithm.AES_GCM_NoPadding,
    ),
    iOptions: IOSOptions(
      accessibility: KeychainAccessibility.first_unlock_this_device,
    ),
  );
  
  /// Stores the authentication token securely.
  Future<void> saveAuthToken(String token) async {
    await _storage.write(key: _authTokenKey, value: token);
  }
  
  /// Retrieves the stored authentication token.
  /// Returns null if no token is stored.
  Future<String?> getAuthToken() async {
    return await _storage.read(key: _authTokenKey);
  }
  
  /// Stores the refresh token securely.
  Future<void> saveRefreshToken(String token) async {
    await _storage.write(key: _refreshTokenKey, value: token);
  }
  
  /// Retrieves the stored refresh token.
  Future<String?> getRefreshToken() async {
    return await _storage.read(key: _refreshTokenKey);
  }
  
  /// Stores the user's ID after successful authentication.
  Future<void> saveUserId(int userId) async {
    await _storage.write(key: _userIdKey, value: userId.toString());
  }
  
  /// Retrieves the stored user ID.
  Future<int?> getUserId() async {
    final value = await _storage.read(key: _userIdKey);
    return value != null ? int.tryParse(value) : null;
  }
  
  /// Stores the user's email for display purposes.
  Future<void> saveUserEmail(String email) async {
    await _storage.write(key: _userEmailKey, value: email);
  }
  
  /// Retrieves the stored user email.
  Future<String?> getUserEmail() async {
    return await _storage.read(key: _userEmailKey);
  }
  
  /// Saves all authentication data at once after successful login/registration.
  Future<void> saveAuthData({
    required String authToken,
    required String refreshToken,
    required int userId,
    required String email,
  }) async {
    await Future.wait([
      saveAuthToken(authToken),
      saveRefreshToken(refreshToken),
      saveUserId(userId),
      saveUserEmail(email),
    ]);
  }
  
  /// Clears all stored authentication data (for logout).
  Future<void> clearAllAuthData() async {
    await _storage.deleteAll();
  }
  
  /// Checks if the user has stored authentication credentials.
  Future<bool> hasAuthCredentials() async {
    final token = await getAuthToken();
    return token != null && token.isNotEmpty;
  }
}
```

This service encapsulates all secure storage operations with proper encryption settings for both platforms.

