---
type: "EXAMPLE"
title: "Section 6: Account Linking"
---

Account linking allows users to connect their social accounts to existing email accounts, providing flexibility in how they sign in.

**Why Account Linking Matters**

1. **User Convenience**: Users can sign in with any linked method
2. **Account Recovery**: If users forget their password, they can sign in with a linked social account
3. **Prevent Duplicates**: Avoids creating multiple accounts for the same user
4. **Data Preservation**: All user data stays in one account regardless of sign-in method

**Create Account Linking Service**

Create `lib/services/account_linking_service.dart`:

```dart
import 'package:flutter/foundation.dart';
import 'package:your_app_client/your_app_client.dart';
import 'auth_service.dart';
import 'google_auth_service.dart';
import 'apple_auth_service.dart';
import 'secure_storage_service.dart';

/// Result of an account linking attempt.
class LinkAccountResult {
  final bool success;
  final String? errorMessage;
  final String? errorCode;
  final List<String> linkedProviders;
  
  LinkAccountResult({
    required this.success,
    this.errorMessage,
    this.errorCode,
    this.linkedProviders = const [],
  });
  
  factory LinkAccountResult.success(List<String> providers) {
    return LinkAccountResult(
      success: true,
      linkedProviders: providers,
    );
  }
  
  factory LinkAccountResult.failure(String message, {String? code}) {
    return LinkAccountResult(
      success: false,
      errorMessage: message,
      errorCode: code,
    );
  }
  
  factory LinkAccountResult.alreadyLinked(String provider) {
    return LinkAccountResult(
      success: false,
      errorMessage: '$provider is already linked to another account.',
      errorCode: 'already_linked',
    );
  }
}

/// Service for linking and unlinking OAuth providers to user accounts.
class AccountLinkingService {
  final Client _client;
  final SecureStorageService _secureStorage;
  final GoogleAuthService _googleAuthService;
  final AppleAuthService _appleAuthService;
  
  AccountLinkingService({
    required Client client,
    required SecureStorageService secureStorage,
    required GoogleAuthService googleAuthService,
    required AppleAuthService appleAuthService,
  })  : _client = client,
        _secureStorage = secureStorage,
        _googleAuthService = googleAuthService,
        _appleAuthService = appleAuthService;
  
  /// Gets the list of providers linked to the current user's account.
  Future<List<String>> getLinkedProviders() async {
    try {
      final providers = await _client.auth.getLinkedProviders();
      return providers;
    } catch (e) {
      if (kDebugMode) {
        print('Failed to get linked providers: $e');
      }
      return [];
    }
  }
  
  /// Links a Google account to the current user's account.
  Future<LinkAccountResult> linkGoogleAccount() async {
    try {
      // Trigger Google Sign-In to get tokens
      final googleResult = await _googleAuthService.signIn();
      
      if (!googleResult.success) {
        return LinkAccountResult.failure(
          googleResult.errorMessage ?? 'Google sign-in failed',
          code: googleResult.errorCode,
        );
      }
      
      // Send tokens to server to link account
      final response = await _client.auth.linkGoogleAccount(
        idToken: googleResult.idToken!,
        accessToken: googleResult.accessToken!,
      );
      
      if (response.success) {
        final providers = await getLinkedProviders();
        return LinkAccountResult.success(providers);
      } else {
        return _handleLinkError(response);
      }
    } catch (e) {
      if (kDebugMode) {
        print('Link Google account error: $e');
      }
      return LinkAccountResult.failure(
        'Failed to link Google account. Please try again.',
        code: 'link_failed',
      );
    }
  }
  
  /// Links an Apple account to the current user's account.
  Future<LinkAccountResult> linkAppleAccount() async {
    try {
      // Check if Apple Sign-In is available
      final isAvailable = await _appleAuthService.isAvailable();
      if (!isAvailable) {
        return LinkAccountResult.failure(
          'Apple Sign-In is not available on this device',
          code: 'not_available',
        );
      }
      
      // Trigger Apple Sign-In to get tokens
      final appleResult = await _appleAuthService.signIn();
      
      if (!appleResult.success) {
        return LinkAccountResult.failure(
          appleResult.errorMessage ?? 'Apple sign-in failed',
          code: appleResult.errorCode,
        );
      }
      
      // Send tokens to server to link account
      final response = await _client.auth.linkAppleAccount(
        identityToken: appleResult.identityToken!,
        authorizationCode: appleResult.authorizationCode!,
        userIdentifier: appleResult.userIdentifier!,
      );
      
      if (response.success) {
        final providers = await getLinkedProviders();
        return LinkAccountResult.success(providers);
      } else {
        return _handleLinkError(response);
      }
    } catch (e) {
      if (kDebugMode) {
        print('Link Apple account error: $e');
      }
      return LinkAccountResult.failure(
        'Failed to link Apple account. Please try again.',
        code: 'link_failed',
      );
    }
  }
  
  /// Unlinks a provider from the current user's account.
  /// Users must have at least one sign-in method remaining.
  Future<LinkAccountResult> unlinkProvider(String provider) async {
    try {
      // Get current linked providers
      final currentProviders = await getLinkedProviders();
      
      // Ensure user has at least one other method
      if (currentProviders.length <= 1) {
        return LinkAccountResult.failure(
          'You must have at least one sign-in method. '
          'Link another account before unlinking this one.',
          code: 'last_provider',
        );
      }
      
      // Unlink on server
      final response = await _client.auth.unlinkProvider(provider: provider);
      
      if (response.success) {
        // Also sign out from the provider locally if needed
        if (provider == 'google') {
          await _googleAuthService.signOut();
        }
        
        final providers = await getLinkedProviders();
        return LinkAccountResult.success(providers);
      } else {
        return LinkAccountResult.failure(
          'Failed to unlink $provider. Please try again.',
          code: 'unlink_failed',
        );
      }
    } catch (e) {
      if (kDebugMode) {
        print('Unlink provider error: $e');
      }
      return LinkAccountResult.failure(
        'Failed to unlink account. Please try again.',
        code: 'unlink_failed',
      );
    }
  }
  
  LinkAccountResult _handleLinkError(dynamic response) {
    // Handle specific error cases from server
    if (response.errorCode == 'already_linked') {
      return LinkAccountResult.alreadyLinked(response.provider ?? 'This account');
    }
    return LinkAccountResult.failure(
      response.errorMessage ?? 'Failed to link account',
      code: response.errorCode,
    );
  }
}
```

**Add Riverpod Provider**

```dart
/// Account linking service provider
final accountLinkingServiceProvider = Provider<AccountLinkingService>((ref) {
  return AccountLinkingService(
    client: ref.watch(clientProvider),
    secureStorage: ref.watch(secureStorageProvider),
    googleAuthService: ref.watch(googleAuthServiceProvider),
    appleAuthService: ref.watch(appleAuthServiceProvider),
  );
});
```

Account linking gives users flexibility while keeping their data unified.

