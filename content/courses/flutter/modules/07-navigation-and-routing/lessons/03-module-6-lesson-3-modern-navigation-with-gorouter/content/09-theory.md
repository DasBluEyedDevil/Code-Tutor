---
type: "THEORY"
title: "Redirects (Route Guards)"
---


Protect routes that require authentication:


**Automatic protection!** Try to access `/profile` without logging in → redirected to `/login`.



```dart
class AuthService {
  bool isLoggedIn = false;
}

final authService = AuthService();

final router = GoRouter(
  redirect: (context, state) {
    final isLoggedIn = authService.isLoggedIn;
    final isGoingToLogin = state.matchedLocation == '/login';

    // Not logged in and not going to login? Redirect to login!
    if (!isLoggedIn && !isGoingToLogin) {
      return '/login';
    }

    // Logged in and going to login? Redirect to home!
    if (isLoggedIn && isGoingToLogin) {
      return '/';
    }

    // No redirect needed
    return null;
  },
  routes: [
    GoRoute(
      path: '/login',
      builder: (context, state) => LoginScreen(),
    ),
    GoRoute(
      path: '/',
      builder: (context, state) => HomeScreen(),
    ),
    GoRoute(
      path: '/profile',
      builder: (context, state) => ProfileScreen(),
    ),
  ],
);
```
