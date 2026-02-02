---
type: "EXAMPLE"
title: "Sealed Classes for State Management"
---

Sealed classes are perfect for UI state management. Define all possible states (loading, success, error) as subclasses. The compiler ensures every state is handled, preventing forgotten edge cases in your UI.

```dart
// Perfect for representing UI states!
sealed class AuthState {}

class AuthInitial extends AuthState {}

class AuthLoading extends AuthState {}

class AuthSuccess extends AuthState {
  final String userName;
  final String token;
  AuthSuccess({required this.userName, required this.token});
}

class AuthError extends AuthState {
  final String message;
  AuthError(this.message);
}

// Build UI based on state - compiler checks all cases!
String buildUI(AuthState state) {
  return switch (state) {
    AuthInitial() => 'Welcome! Please log in.',
    AuthLoading() => 'Loading... Please wait.',
    AuthSuccess(userName: var name) => 'Welcome back, $name!',
    AuthError(message: var msg) => 'Error: $msg',
  };
}

void main() {
  var states = [
    AuthInitial(),
    AuthLoading(),
    AuthSuccess(userName: 'Alice', token: 'abc123'),
    AuthError('Invalid password'),
  ];
  
  for (var state in states) {
    print(buildUI(state));
  }
}
```
