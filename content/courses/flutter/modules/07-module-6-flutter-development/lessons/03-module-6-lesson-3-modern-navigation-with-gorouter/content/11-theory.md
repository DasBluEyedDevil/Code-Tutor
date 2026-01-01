---
type: "THEORY"
title: "Refresh Listener (React to Changes)"
---



**When user logs out → GoRouter automatically redirects!**



```dart
class AuthNotifier extends ChangeNotifier {
  bool _isLoggedIn = false;

  bool get isLoggedIn => _isLoggedIn;

  void login() {
    _isLoggedIn = true;
    notifyListeners();  // GoRouter will refresh!
  }

  void logout() {
    _isLoggedIn = false;
    notifyListeners();
  }
}

final authNotifier = AuthNotifier();

final router = GoRouter(
  refreshListenable: authNotifier,  // Listen to auth changes!
  redirect: (context, state) {
    if (!authNotifier.isLoggedIn && state.matchedLocation != '/login') {
      return '/login';
    }
    return null;
  },
  routes: [...],
);
```
