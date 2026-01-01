---
type: "EXAMPLE"
title: "Section 2 (continued): Auth State"
---

**Step 2: Auth State**

Define an immutable state class that represents all possible authentication states:

```dart
// lib/features/auth/domain/auth_state.dart

import 'user_model.dart';

enum AuthStatus {
  initial,    // App just started, checking stored tokens
  loading,    // Login/register in progress
  authenticated,
  unauthenticated,
  error,
}

class AuthState {
  final AuthStatus status;
  final User? user;
  final String? errorMessage;
  final String? accessToken;
  final String? refreshToken;

  const AuthState({
    this.status = AuthStatus.initial,
    this.user,
    this.errorMessage,
    this.accessToken,
    this.refreshToken,
  });

  bool get isAuthenticated => status == AuthStatus.authenticated;
  bool get isLoading => status == AuthStatus.loading;
  bool get isInitial => status == AuthStatus.initial;

  AuthState copyWith({
    AuthStatus? status,
    User? user,
    String? errorMessage,
    String? accessToken,
    String? refreshToken,
  }) {
    return AuthState(
      status: status ?? this.status,
      user: user ?? this.user,
      errorMessage: errorMessage,
      accessToken: accessToken ?? this.accessToken,
      refreshToken: refreshToken ?? this.refreshToken,
    );
  }

  // Named constructors for common states
  const AuthState.initial() : this(status: AuthStatus.initial);

  const AuthState.loading() : this(status: AuthStatus.loading);

  const AuthState.unauthenticated()
      : this(status: AuthStatus.unauthenticated);

  AuthState.authenticated({
    required User user,
    required String accessToken,
    String? refreshToken,
  }) : this(
          status: AuthStatus.authenticated,
          user: user,
          accessToken: accessToken,
          refreshToken: refreshToken,
        );

  AuthState.error(String message)
      : this(
          status: AuthStatus.error,
          errorMessage: message,
        );
}
```
