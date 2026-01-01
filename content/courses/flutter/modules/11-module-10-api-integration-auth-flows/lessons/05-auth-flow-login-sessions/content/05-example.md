---
type: "EXAMPLE"
title: "Token Refresh Flow"
---

Automatic token refresh is critical for a seamless user experience. Users should never see login screens unexpectedly due to expired tokens. Let us implement comprehensive token refresh logic.

**Add Token Refresh to Auth Service**

Add these methods to `lib/services/auth_service.dart`:

```dart
/// Result of a token refresh attempt.
class TokenRefreshResult {
  final bool success;
  final String? newAuthToken;
  final String? newRefreshToken;
  final String? errorMessage;
  
  TokenRefreshResult({
    required this.success,
    this.newAuthToken,
    this.newRefreshToken,
    this.errorMessage,
  });
  
  factory TokenRefreshResult.success({
    required String authToken,
    String? refreshToken,
  }) {
    return TokenRefreshResult(
      success: true,
      newAuthToken: authToken,
      newRefreshToken: refreshToken,
    );
  }
  
  factory TokenRefreshResult.failure(String message) {
    return TokenRefreshResult(
      success: false,
      errorMessage: message,
    );
  }
}

// Add to AuthService class:

/// Refreshes the authentication token using the refresh token.
/// Implements token rotation - each refresh returns a new refresh token.
Future<TokenRefreshResult> refreshAuthToken(String refreshToken) async {
  try {
    // Call Serverpod's token refresh endpoint
    // This endpoint should validate the refresh token and return new tokens
    final response = await _client.auth.refreshToken(
      refreshToken: refreshToken,
    );
    
    if (response.success && response.key != null) {
      // Token rotation: return both new auth token and new refresh token
      return TokenRefreshResult.success(
        authToken: response.key!,
        refreshToken: response.keyId?.toString(),
      );
    } else {
      return TokenRefreshResult.failure(
        'Failed to refresh token. Please log in again.',
      );
    }
  } on ServerpodClientException catch (e) {
    if (e.statusCode == 401) {
      // Refresh token is invalid or expired
      return TokenRefreshResult.failure(
        'Session expired. Please log in again.',
      );
    }
    return TokenRefreshResult.failure(
      'Failed to refresh session. Please try again.',
    );
  } catch (e) {
    return TokenRefreshResult.failure(
      'Network error. Please check your connection.',
    );
  }
}
```

**Create an HTTP Interceptor for Automatic Token Refresh**

To automatically refresh tokens when API calls fail due to expiration, create an interceptor. Create `lib/utils/auth_interceptor.dart`:

```dart
import 'dart:async';
import 'package:flutter/foundation.dart';
import '../services/secure_storage_service.dart';
import '../services/auth_service.dart';

/// Interceptor that handles automatic token refresh for API calls.
/// 
/// When an API call fails with 401 Unauthorized, this interceptor:
/// 1. Attempts to refresh the auth token
/// 2. Retries the original request with the new token
/// 3. If refresh fails, triggers logout
class AuthInterceptor {
  final SecureStorageService _secureStorage;
  final AuthService _authService;
  final void Function() _onSessionExpired;
  
  // Prevent multiple simultaneous refresh attempts
  bool _isRefreshing = false;
  Completer<bool>? _refreshCompleter;
  
  AuthInterceptor({
    required SecureStorageService secureStorage,
    required AuthService authService,
    required void Function() onSessionExpired,
  })  : _secureStorage = secureStorage,
        _authService = authService,
        _onSessionExpired = onSessionExpired;
  
  /// Handles an unauthorized response by refreshing the token.
  /// Returns true if refresh was successful and request should be retried.
  Future<bool> handleUnauthorized() async {
    // If already refreshing, wait for that attempt to complete
    if (_isRefreshing && _refreshCompleter != null) {
      return _refreshCompleter!.future;
    }
    
    _isRefreshing = true;
    _refreshCompleter = Completer<bool>();
    
    try {
      final refreshToken = await _secureStorage.getRefreshToken();
      if (refreshToken == null) {
        _onSessionExpired();
        _refreshCompleter!.complete(false);
        return false;
      }
      
      final result = await _authService.refreshAuthToken(refreshToken);
      
      if (result.success && result.newAuthToken != null) {
        // Store new tokens
        await _secureStorage.saveAuthToken(result.newAuthToken!);
        if (result.newRefreshToken != null) {
          await _secureStorage.saveRefreshToken(result.newRefreshToken!);
        }
        await _secureStorage.saveTokenExpiration(
          DateTime.now().add(const Duration(hours: 1)),
        );
        
        _refreshCompleter!.complete(true);
        return true;
      } else {
        // Refresh failed - session expired
        _onSessionExpired();
        _refreshCompleter!.complete(false);
        return false;
      }
    } catch (e) {
      if (kDebugMode) {
        print('Token refresh in interceptor failed: $e');
      }
      _onSessionExpired();
      _refreshCompleter!.complete(false);
      return false;
    } finally {
      _isRefreshing = false;
    }
  }
  
  /// Proactively refreshes the token if it is about to expire.
  /// Call this before making important API calls.
  Future<void> ensureValidToken() async {
    final isExpiring = await _secureStorage.isTokenExpiredOrExpiring();
    if (isExpiring) {
      await handleUnauthorized();
    }
  }
}
```

**Wrap API Calls with Token Refresh**

Create `lib/utils/api_client.dart` to wrap API calls with automatic retry:

```dart
import 'dart:async';
import 'package:your_app_client/your_app_client.dart';
import 'auth_interceptor.dart';

/// Wrapper for API calls that handles automatic token refresh and retry.
class ApiClient {
  final Client _client;
  final AuthInterceptor _authInterceptor;
  
  ApiClient({
    required Client client,
    required AuthInterceptor authInterceptor,
  })  : _client = client,
        _authInterceptor = authInterceptor;
  
  /// Executes an API call with automatic token refresh on 401 errors.
  /// 
  /// Example usage:
  /// ```dart
  /// final tasks = await apiClient.execute(
  ///   () => client.tasks.getAllTasks(),
  /// );
  /// ```
  Future<T> execute<T>(Future<T> Function() apiCall) async {
    try {
      // Proactively refresh if token is expiring soon
      await _authInterceptor.ensureValidToken();
      
      // Make the API call
      return await apiCall();
    } on ServerpodClientException catch (e) {
      if (e.statusCode == 401) {
        // Token expired - try to refresh
        final refreshed = await _authInterceptor.handleUnauthorized();
        if (refreshed) {
          // Retry the original call with new token
          return await apiCall();
        }
      }
      rethrow;
    }
  }
}
```

This implementation ensures tokens are refreshed automatically without user intervention, providing a seamless experience.

