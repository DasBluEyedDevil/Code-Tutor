---
type: "EXAMPLE"
title: "Section 2: Router Provider with Auth Redirects"
---

Now create the router provider that uses `refreshListenable` to react to auth state changes. The `redirect` callback runs on every navigation and auth state change.

```dart
// lib/router/app_router.dart

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../providers/auth_provider.dart';
import '../screens/screens.dart';

/// Routes that don't require authentication.
const publicRoutes = ['/login', '/register', '/forgot-password'];

/// Routes that require admin role.
const adminRoutes = ['/admin', '/admin/users', '/admin/settings'];

/// Routes that require premium role.
const premiumRoutes = ['/premium', '/premium/features'];

/// Router provider with auth-based redirects.
final routerProvider = Provider<GoRouter>((ref) {
  final authNotifier = ref.watch(authNotifierProvider);

  return GoRouter(
    initialLocation: '/',
    debugLogDiagnostics: true,

    // React to auth state changes
    refreshListenable: authNotifier,

    // Redirect logic runs on every navigation
    redirect: (context, state) {
      final isLoading = authNotifier.isLoading;
      final isAuthenticated = authNotifier.isAuthenticated;
      final currentPath = state.matchedLocation;
      final isPublicRoute = publicRoutes.contains(currentPath);

      // Show loading screen while checking auth
      if (isLoading) {
        return '/splash';
      }

      // Redirect unauthenticated users to login
      if (!isAuthenticated && !isPublicRoute && currentPath != '/splash') {
        // Preserve the intended destination
        final destination = Uri.encodeComponent(state.uri.toString());
        return '/login?redirect=$destination';
      }

      // Redirect authenticated users away from auth screens
      if (isAuthenticated && isPublicRoute) {
        return '/';
      }

      // Check role-based access
      final role = authNotifier.role;

      if (adminRoutes.any((r) => currentPath.startsWith(r))) {
        if (role != UserRole.admin) {
          return '/unauthorized';
        }
      }

      if (premiumRoutes.any((r) => currentPath.startsWith(r))) {
        if (role != UserRole.premium && role != UserRole.admin) {
          return '/upgrade';
        }
      }

      // No redirect needed
      return null;
    },

    routes: [
      // Splash/loading screen
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

      // Protected routes using ShellRoute
      ShellRoute(
        builder: (context, state, child) {
          return AppShell(child: child);
        },
        routes: [
          GoRoute(
            path: '/',
            builder: (context, state) => const HomeScreen(),
          ),
          GoRoute(
            path: '/profile',
            builder: (context, state) => const ProfileScreen(),
          ),
          GoRoute(
            path: '/settings',
            builder: (context, state) => const SettingsScreen(),
          ),
        ],
      ),

      // Admin routes
      GoRoute(
        path: '/admin',
        builder: (context, state) => const AdminDashboard(),
      ),

      // Premium routes
      GoRoute(
        path: '/premium',
        builder: (context, state) => const PremiumFeatures(),
      ),

      // Error routes
      GoRoute(
        path: '/unauthorized',
        builder: (context, state) => const UnauthorizedScreen(),
      ),
      GoRoute(
        path: '/upgrade',
        builder: (context, state) => const UpgradeScreen(),
      ),
    ],

    errorBuilder: (context, state) => NotFoundScreen(
      error: state.error?.toString(),
    ),
  );
});
```
