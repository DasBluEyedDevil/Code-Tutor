---
type: "EXAMPLE"
title: "Auth State and Providers"
---


**Implementing Auth State Management with Riverpod**

We'll create a robust auth state system that handles authentication, token storage, and seamless state restoration.



```dart
// lib/features/auth/domain/auth_state.dart
import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:serverpod_auth_shared_flutter/serverpod_auth_shared_flutter.dart';

part 'auth_state.freezed.dart';

/// Represents the current authentication state
@freezed
class AuthState with _$AuthState {
  const AuthState._();

  /// User is not authenticated
  const factory AuthState.unauthenticated() = AuthStateUnauthenticated;

  /// Authentication is in progress
  const factory AuthState.authenticating() = AuthStateAuthenticating;

  /// User is authenticated
  const factory AuthState.authenticated({
    required UserInfo userInfo,
    required String authToken,
  }) = AuthStateAuthenticated;

  /// Authentication failed
  const factory AuthState.error({
    required String message,
    String? code,
  }) = AuthStateError;

  /// Check if user is authenticated
  bool get isAuthenticated => this is AuthStateAuthenticated;

  /// Get user info if authenticated
  UserInfo? get userInfo => mapOrNull(
    authenticated: (state) => state.userInfo,
  );

  /// Get auth token if authenticated
  String? get authToken => mapOrNull(
    authenticated: (state) => state.authToken,
  );
}

/// Credentials for login
class LoginCredentials {
  final String email;
  final String password;

  const LoginCredentials({
    required this.email,
    required this.password,
  });
}

/// Registration data
class RegistrationData {
  final String email;
  final String password;
  final String? displayName;
  final bool acceptedTerms;

  const RegistrationData({
    required this.email,
    required this.password,
    this.displayName,
    required this.acceptedTerms,
  });
}

---

// lib/features/auth/data/auth_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:serverpod_auth_client/serverpod_auth_client.dart';
import '../../../core/providers/serverpod_client_provider.dart';
import '../domain/auth_state.dart';

/// Keys for secure storage
abstract class AuthStorageKeys {
  static const String authToken = 'auth_token';
  static const String refreshToken = 'refresh_token';
  static const String userId = 'user_id';
  static const String userEmail = 'user_email';
  static const String tokenExpiry = 'token_expiry';
}

/// Repository for authentication operations
class AuthRepository {
  final Ref _ref;
  final FlutterSecureStorage _secureStorage;

  AuthRepository(this._ref, this._secureStorage);

  /// Get the Serverpod client
  Client get _client => _ref.read(serverpodClientProvider);

  /// Login with email and password
  Future<AuthResult> login(LoginCredentials credentials) async {
    try {
      // Call Serverpod auth endpoint
      final response = await _client.modules.auth.email.authenticate(
        credentials.email,
        credentials.password,
      );

      if (!response.success) {
        return AuthResult.failure(
          message: response.failReason?.toString() ?? 'Login failed',
          code: 'auth_failed',
        );
      }

      // Store tokens securely
      await _storeAuthData(
        userInfo: response.userInfo!,
        keyId: response.keyId!,
        key: response.key!,
      );

      // Configure session manager
      await _ref.read(sessionManagerProvider).registerSignedInUser(
        response.userInfo!,
        response.keyId!,
        response.key!,
      );

      return AuthResult.success(
        userInfo: response.userInfo!,
        authToken: response.key!,
      );
    } on ServerpodClientException catch (e) {
      return AuthResult.failure(
        message: _mapServerError(e),
        code: 'server_error',
      );
    } catch (e) {
      return AuthResult.failure(
        message: 'Connection error. Please check your internet.',
        code: 'network_error',
      );
    }
  }

  /// Register a new user
  Future<AuthResult> register(RegistrationData data) async {
    try {
      // Create account
      final response = await _client.modules.auth.email.createAccountRequest(
        data.displayName ?? data.email.split('@').first,
        data.email,
        data.password,
      );

      if (!response) {
        return AuthResult.failure(
          message: 'Registration failed. Email may already be in use.',
          code: 'registration_failed',
        );
      }

      // Return success - user needs to verify email
      return AuthResult.pendingVerification(
        email: data.email,
      );
    } on ServerpodClientException catch (e) {
      return AuthResult.failure(
        message: _mapServerError(e),
        code: 'server_error',
      );
    } catch (e) {
      return AuthResult.failure(
        message: 'Connection error. Please try again.',
        code: 'network_error',
      );
    }
  }

  /// Logout and clear all stored data
  Future<void> logout() async {
    try {
      // Sign out from Serverpod
      await _ref.read(sessionManagerProvider).signOutDevice();
    } catch (e) {
      // Continue with local logout even if server call fails
    }

    // Clear secure storage
    await _secureStorage.deleteAll();
  }

  /// Restore auth state from stored tokens
  Future<AuthState> restoreSession() async {
    try {
      final sessionManager = _ref.read(sessionManagerProvider);

      // Check if we have a stored session
      if (sessionManager.isSignedIn) {
        final userInfo = await sessionManager.signedInUser;
        if (userInfo != null) {
          final token = await _secureStorage.read(
            key: AuthStorageKeys.authToken,
          );
          return AuthState.authenticated(
            userInfo: userInfo,
            authToken: token ?? '',
          );
        }
      }

      return const AuthState.unauthenticated();
    } catch (e) {
      // Clear potentially corrupted data
      await _secureStorage.deleteAll();
      return const AuthState.unauthenticated();
    }
  }

  /// Request password reset
  Future<bool> requestPasswordReset(String email) async {
    try {
      return await _client.modules.auth.email.initiatePasswordReset(email);
    } catch (e) {
      return false;
    }
  }

  /// Store auth data securely
  Future<void> _storeAuthData({
    required UserInfo userInfo,
    required int keyId,
    required String key,
  }) async {
    await Future.wait([
      _secureStorage.write(
        key: AuthStorageKeys.authToken,
        value: key,
      ),
      _secureStorage.write(
        key: AuthStorageKeys.userId,
        value: userInfo.id.toString(),
      ),
      _secureStorage.write(
        key: AuthStorageKeys.userEmail,
        value: userInfo.email ?? '',
      ),
    ]);
  }

  /// Map server errors to user-friendly messages
  String _mapServerError(ServerpodClientException e) {
    // Map common error codes
    switch (e.statusCode) {
      case 401:
        return 'Invalid email or password';
      case 403:
        return 'Account is locked. Please contact support.';
      case 429:
        return 'Too many attempts. Please try again later.';
      case 500:
        return 'Server error. Please try again.';
      default:
        return 'An error occurred. Please try again.';
    }
  }
}

/// Result of an auth operation
class AuthResult {
  final bool success;
  final UserInfo? userInfo;
  final String? authToken;
  final String? message;
  final String? code;
  final bool pendingVerification;
  final String? email;

  const AuthResult._(
    this.success, {
    this.userInfo,
    this.authToken,
    this.message,
    this.code,
    this.pendingVerification = false,
    this.email,
  });

  factory AuthResult.success({
    required UserInfo userInfo,
    required String authToken,
  }) => AuthResult._(
    true,
    userInfo: userInfo,
    authToken: authToken,
  );

  factory AuthResult.failure({
    required String message,
    String? code,
  }) => AuthResult._(
    false,
    message: message,
    code: code,
  );

  factory AuthResult.pendingVerification({
    required String email,
  }) => AuthResult._(
    true,
    pendingVerification: true,
    email: email,
  );
}

---

// lib/features/auth/providers/auth_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import '../data/auth_repository.dart';
import '../domain/auth_state.dart';

/// Provider for secure storage
final secureStorageProvider = Provider<FlutterSecureStorage>((ref) {
  return const FlutterSecureStorage(
    aOptions: AndroidOptions(
      encryptedSharedPreferences: true,
    ),
    iOptions: IOSOptions(
      accessibility: KeychainAccessibility.first_unlock_this_device,
    ),
  );
});

/// Provider for auth repository
final authRepositoryProvider = Provider<AuthRepository>((ref) {
  return AuthRepository(ref, ref.watch(secureStorageProvider));
});

/// Main auth state notifier
class AuthNotifier extends AsyncNotifier<AuthState> {
  @override
  Future<AuthState> build() async {
    // Restore session on app start
    return ref.read(authRepositoryProvider).restoreSession();
  }

  /// Login with email and password
  Future<void> login(LoginCredentials credentials) async {
    state = const AsyncValue.loading();

    final result = await ref.read(authRepositoryProvider).login(credentials);

    if (result.success && !result.pendingVerification) {
      state = AsyncValue.data(AuthState.authenticated(
        userInfo: result.userInfo!,
        authToken: result.authToken!,
      ));
    } else {
      state = AsyncValue.data(AuthState.error(
        message: result.message ?? 'Login failed',
        code: result.code,
      ));
    }
  }

  /// Register a new user
  Future<RegistrationResult> register(RegistrationData data) async {
    state = const AsyncValue.loading();

    final result = await ref.read(authRepositoryProvider).register(data);

    if (result.pendingVerification) {
      state = const AsyncValue.data(AuthState.unauthenticated());
      return RegistrationResult(
        success: true,
        pendingVerification: true,
        email: result.email,
      );
    }

    if (!result.success) {
      state = AsyncValue.data(AuthState.error(
        message: result.message ?? 'Registration failed',
        code: result.code,
      ));
      return RegistrationResult(
        success: false,
        message: result.message,
      );
    }

    return RegistrationResult(success: true);
  }

  /// Logout
  Future<void> logout() async {
    await ref.read(authRepositoryProvider).logout();
    state = const AsyncValue.data(AuthState.unauthenticated());
  }

  /// Request password reset
  Future<bool> requestPasswordReset(String email) async {
    return ref.read(authRepositoryProvider).requestPasswordReset(email);
  }

  /// Clear any error state
  void clearError() {
    if (state.valueOrNull is AuthStateError) {
      state = const AsyncValue.data(AuthState.unauthenticated());
    }
  }
}

/// Provider for auth state
final authProvider = AsyncNotifierProvider<AuthNotifier, AuthState>(() {
  return AuthNotifier();
});

/// Convenience provider for checking if authenticated
final isAuthenticatedProvider = Provider<bool>((ref) {
  return ref.watch(authProvider).valueOrNull?.isAuthenticated ?? false;
});

/// Provider for current user info
final currentUserProvider = Provider<UserInfo?>((ref) {
  return ref.watch(authProvider).valueOrNull?.userInfo;
});

class RegistrationResult {
  final bool success;
  final bool pendingVerification;
  final String? email;
  final String? message;

  const RegistrationResult({
    required this.success,
    this.pendingVerification = false,
    this.email,
    this.message,
  });
}
```
