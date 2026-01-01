---
type: "EXAMPLE"
title: "Section 1: GoRouter + Riverpod Setup"
---

First, let us set up GoRouter with Riverpod integration. The key is making the router reactive to authentication state changes using `refreshListenable`.

**Step 1: Add Dependencies**

Ensure your `pubspec.yaml` includes:

```yaml
dependencies:
  go_router: ^14.6.0
  flutter_riverpod: ^2.5.1
```

**Step 2: Create Auth State for Navigation**

The router needs to know the current auth state. Create a notifier that GoRouter can listen to:

```dart
// lib/providers/auth_provider.dart

import 'package:flutter/foundation.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

/// User roles for access control.
enum UserRole { guest, user, premium, admin }

/// Authentication state for navigation.
class AuthState {
  final bool isAuthenticated;
  final bool isLoading;
  final String? userId;
  final UserRole role;

  const AuthState({
    this.isAuthenticated = false,
    this.isLoading = true,
    this.userId,
    this.role = UserRole.guest,
  });

  AuthState copyWith({
    bool? isAuthenticated,
    bool? isLoading,
    String? userId,
    UserRole? role,
  }) {
    return AuthState(
      isAuthenticated: isAuthenticated ?? this.isAuthenticated,
      isLoading: isLoading ?? this.isLoading,
      userId: userId ?? this.userId,
      role: role ?? this.role,
    );
  }
}

/// Notifier that GoRouter can listen to for auth changes.
class AuthNotifier extends ChangeNotifier {
  AuthState _state = const AuthState();

  AuthState get state => _state;
  bool get isAuthenticated => _state.isAuthenticated;
  bool get isLoading => _state.isLoading;
  UserRole get role => _state.role;

  void setAuthenticated(String userId, UserRole role) {
    _state = AuthState(
      isAuthenticated: true,
      isLoading: false,
      userId: userId,
      role: role,
    );
    notifyListeners();
  }

  void setUnauthenticated() {
    _state = const AuthState(
      isAuthenticated: false,
      isLoading: false,
    );
    notifyListeners();
  }

  void setLoading() {
    _state = _state.copyWith(isLoading: true);
    notifyListeners();
  }
}

/// Global auth notifier provider.
final authNotifierProvider = ChangeNotifierProvider<AuthNotifier>((ref) {
  return AuthNotifier();
});
```
