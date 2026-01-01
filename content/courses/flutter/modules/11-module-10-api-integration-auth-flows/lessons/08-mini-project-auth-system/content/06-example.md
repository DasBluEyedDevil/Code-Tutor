---
type: "EXAMPLE"
title: "Section 3: Auth Provider (Riverpod Notifier)"
---

The AuthNotifier ties together the AuthService and AuthState, providing a clean interface for the UI:

```dart
// lib/features/auth/providers/auth_provider.dart

import 'package:flutter/foundation.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../data/auth_service.dart';
import '../domain/auth_state.dart';

/// Provider for the AuthService.
final authServiceProvider = Provider<AuthService>((ref) {
  return AuthService();
});

/// Provider for the AuthNotifier.
final authNotifierProvider =
    StateNotifierProvider<AuthNotifier, AuthState>((ref) {
  final authService = ref.watch(authServiceProvider);
  return AuthNotifier(authService);
});

/// Convenience provider for checking if user is authenticated.
final isAuthenticatedProvider = Provider<bool>((ref) {
  return ref.watch(authNotifierProvider).isAuthenticated;
});

/// ChangeNotifier for GoRouter's refreshListenable.
final authChangeNotifierProvider = ChangeNotifierProvider<AuthChangeNotifier>((ref) {
  final authState = ref.watch(authNotifierProvider);
  return AuthChangeNotifier(authState);
});

class AuthChangeNotifier extends ChangeNotifier {
  AuthState _state;

  AuthChangeNotifier(this._state);

  void update(AuthState newState) {
    if (_state.status != newState.status) {
      _state = newState;
      notifyListeners();
    }
  }
}

class AuthNotifier extends StateNotifier<AuthState> {
  final AuthService _authService;

  AuthNotifier(this._authService) : super(const AuthState.initial()) {
    // Check stored auth on initialization
    _checkStoredAuth();
  }

  Future<void> _checkStoredAuth() async {
    try {
      final result = await _authService.checkStoredAuth();

      if (result != null) {
        state = AuthState.authenticated(
          user: result.user,
          accessToken: result.accessToken,
          refreshToken: result.refreshToken,
        );
      } else {
        state = const AuthState.unauthenticated();
      }
    } catch (e) {
      state = const AuthState.unauthenticated();
    }
  }

  Future<void> login({
    required String email,
    required String password,
  }) async {
    state = const AuthState.loading();

    try {
      final result = await _authService.login(
        email: email,
        password: password,
      );

      state = AuthState.authenticated(
        user: result.user,
        accessToken: result.accessToken,
        refreshToken: result.refreshToken,
      );
    } on AuthException catch (e) {
      state = AuthState.error(e.message);
    } catch (e) {
      state = AuthState.error('An unexpected error occurred');
    }
  }

  Future<void> register({
    required String email,
    required String password,
    String? displayName,
  }) async {
    state = const AuthState.loading();

    try {
      final result = await _authService.register(
        email: email,
        password: password,
        displayName: displayName,
      );

      state = AuthState.authenticated(
        user: result.user,
        accessToken: result.accessToken,
        refreshToken: result.refreshToken,
      );
    } on AuthException catch (e) {
      state = AuthState.error(e.message);
    } catch (e) {
      state = AuthState.error('Registration failed. Please try again.');
    }
  }

  Future<void> loginWithGoogle() async {
    state = const AuthState.loading();

    try {
      final result = await _authService.loginWithGoogle();

      state = AuthState.authenticated(
        user: result.user,
        accessToken: result.accessToken,
        refreshToken: result.refreshToken,
      );
    } on AuthException catch (e) {
      if (e.code == 'cancelled') {
        // User cancelled, go back to unauthenticated
        state = const AuthState.unauthenticated();
      } else {
        state = AuthState.error(e.message);
      }
    } catch (e) {
      state = AuthState.error('Google sign-in failed');
    }
  }

  Future<void> logout() async {
    await _authService.logout();
    state = const AuthState.unauthenticated();
  }

  void clearError() {
    if (state.status == AuthStatus.error) {
      state = const AuthState.unauthenticated();
    }
  }
}
```
