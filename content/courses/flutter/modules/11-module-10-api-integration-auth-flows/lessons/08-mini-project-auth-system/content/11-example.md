---
type: "EXAMPLE"
title: "Section 5: Router Integration"
---

The GoRouter configuration ties everything together with auth-based redirects:

```dart
// lib/core/router/app_router.dart

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../features/auth/providers/auth_provider.dart';
import '../../features/auth/domain/auth_state.dart';
import '../../features/auth/presentation/screens/login_screen.dart';
import '../../features/auth/presentation/screens/register_screen.dart';
import '../../features/auth/presentation/screens/splash_screen.dart';
import '../../features/auth/presentation/screens/profile_screen.dart';
import '../../home/home_screen.dart';

/// Routes that don't require authentication.
const publicRoutes = ['/login', '/register'];

/// Router provider with auth-based redirects.
final routerProvider = Provider<GoRouter>((ref) {
  final authState = ref.watch(authNotifierProvider);

  return GoRouter(
    initialLocation: '/splash',
    debugLogDiagnostics: true,

    // Rebuild router when auth state changes
    refreshListenable: _RouterRefreshNotifier(ref),

    redirect: (context, state) {
      final isInitial = authState.isInitial;
      final isAuthenticated = authState.isAuthenticated;
      final currentPath = state.matchedLocation;
      final isPublicRoute = publicRoutes.contains(currentPath);
      final isSplash = currentPath == '/splash';

      // Show splash while checking stored auth
      if (isInitial && !isSplash) {
        return '/splash';
      }

      // Redirect from splash once auth state is determined
      if (!isInitial && isSplash) {
        return isAuthenticated ? '/' : '/login';
      }

      // Redirect unauthenticated users to login
      if (!isAuthenticated && !isPublicRoute && !isSplash) {
        final destination = Uri.encodeComponent(state.uri.toString());
        return '/login?redirect=$destination';
      }

      // Redirect authenticated users away from auth screens
      if (isAuthenticated && isPublicRoute) {
        return '/';
      }

      return null; // No redirect
    },

    routes: [
      // Splash screen
      GoRoute(
        path: '/splash',
        builder: (context, state) => const SplashScreen(),
      ),

      // Public routes
      GoRoute(
        path: '/login',
        builder: (context, state) {
          final redirect = state.uri.queryParameters['redirect'];
          return LoginScreen(redirectPath: redirect);
        },
      ),
      GoRoute(
        path: '/register',
        builder: (context, state) => const RegisterScreen(),
      ),

      // Protected routes
      GoRoute(
        path: '/',
        builder: (context, state) => const HomeScreen(),
        routes: [
          GoRoute(
            path: 'profile',
            builder: (context, state) => const ProfileScreen(),
          ),
        ],
      ),
    ],

    errorBuilder: (context, state) => Scaffold(
      appBar: AppBar(title: const Text('Error')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Icon(Icons.error_outline, size: 64, color: Colors.red),
            const SizedBox(height: 16),
            Text('Page not found: ${state.uri}'),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => context.go('/'),
              child: const Text('Go Home'),
            ),
          ],
        ),
      ),
    ),
  );
});

/// Notifier that triggers router refresh when auth state changes.
class _RouterRefreshNotifier extends ChangeNotifier {
  _RouterRefreshNotifier(this._ref) {
    _ref.listen(authNotifierProvider, (previous, next) {
      if (previous?.status != next.status) {
        notifyListeners();
      }
    });
  }

  final Ref _ref;
}
```
