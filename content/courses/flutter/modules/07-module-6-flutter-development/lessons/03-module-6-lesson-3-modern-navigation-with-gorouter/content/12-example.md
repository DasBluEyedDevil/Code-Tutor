---
type: "EXAMPLE"
title: "Complete Authentication Example"
---



**Try it:**
1. App starts → Not logged in → Redirects to `/login`
2. Click "Login" → Redirects to `/`
3. Try to access `/profile` → Works (you're logged in)
4. Click "Logout" → Redirects to `/login`



```dart
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class AuthService extends ChangeNotifier {
  bool _isLoggedIn = false;

  bool get isLoggedIn => _isLoggedIn;

  void login() {
    _isLoggedIn = true;
    notifyListeners();
  }

  void logout() {
    _isLoggedIn = false;
    notifyListeners();
  }
}

void main() {
  final authService = AuthService();

  final router = GoRouter(
    refreshListenable: authService,
    redirect: (context, state) {
      final isLoggedIn = authService.isLoggedIn;
      final isGoingToLogin = state.matchedLocation == '/login';

      if (!isLoggedIn && !isGoingToLogin) {
        return '/login';
      }

      if (isLoggedIn && isGoingToLogin) {
        return '/';
      }

      return null;
    },
    routes: [
      GoRoute(
        path: '/login',
        builder: (context, state) => LoginScreen(authService: authService),
      ),
      GoRoute(
        path: '/',
        builder: (context, state) => HomeScreen(authService: authService),
      ),
      GoRoute(
        path: '/profile',
        builder: (context, state) => ProfileScreen(authService: authService),
      ),
    ],
  );

  runApp(MaterialApp.router(routerConfig: router));
}

class LoginScreen extends StatelessWidget {
  final AuthService authService;

  LoginScreen({required this.authService});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Login')),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            authService.login();
            // GoRouter automatically redirects to home!
          },
          child: Text('Login'),
        ),
      ),
    );
  }
}

class HomeScreen extends StatelessWidget {
  final AuthService authService;

  HomeScreen({required this.authService});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Home'),
        actions: [
          IconButton(
            icon: Icon(Icons.logout),
            onPressed: () {
              authService.logout();
              // Automatically redirected to login!
            },
          ),
        ],
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Welcome! You are logged in.'),
            SizedBox(height: 24),
            ElevatedButton(
              onPressed: () => context.go('/profile'),
              child: Text('View Profile'),
            ),
          ],
        ),
      ),
    );
  }
}

class ProfileScreen extends StatelessWidget {
  final AuthService authService;

  ProfileScreen({required this.authService});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Profile')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            CircleAvatar(radius: 50, child: Icon(Icons.person, size: 50)),
            SizedBox(height: 16),
            Text('Your Profile', style: TextStyle(fontSize: 24)),
            SizedBox(height: 24),
            ElevatedButton(
              onPressed: () {
                authService.logout();
              },
              child: Text('Logout'),
              style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
            ),
          ],
        ),
      ),
    );
  }
}
```
