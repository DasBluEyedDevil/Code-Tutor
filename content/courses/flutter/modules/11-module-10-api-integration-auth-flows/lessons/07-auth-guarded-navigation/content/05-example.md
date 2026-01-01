---
type: "EXAMPLE"
title: "Section 4: Role-Based Access Control"
---

For more granular control, create a route guard widget that checks permissions at the widget level. This complements the router-level redirects.

```dart
// lib/widgets/role_guard.dart

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';

/// Guard widget that checks user roles before rendering content.
class RoleGuard extends ConsumerWidget {
  final List<UserRole> allowedRoles;
  final Widget child;
  final Widget? fallback;

  const RoleGuard({
    super.key,
    required this.allowedRoles,
    required this.child,
    this.fallback,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authNotifierProvider).state;

    if (allowedRoles.contains(authState.role)) {
      return child;
    }

    return fallback ?? const SizedBox.shrink();
  }
}

/// Guard that requires admin role.
class AdminOnly extends StatelessWidget {
  final Widget child;
  final Widget? fallback;

  const AdminOnly({super.key, required this.child, this.fallback});

  @override
  Widget build(BuildContext context) {
    return RoleGuard(
      allowedRoles: const [UserRole.admin],
      fallback: fallback,
      child: child,
    );
  }
}

/// Guard that requires premium or admin role.
class PremiumOnly extends StatelessWidget {
  final Widget child;
  final Widget? fallback;

  const PremiumOnly({super.key, required this.child, this.fallback});

  @override
  Widget build(BuildContext context) {
    return RoleGuard(
      allowedRoles: const [UserRole.premium, UserRole.admin],
      fallback: fallback,
      child: child,
    );
  }
}

// Usage in a screen:
class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: Column(
        children: [
          const Text('Welcome to the app!'),

          // Only visible to admins
          const AdminOnly(
            fallback: SizedBox.shrink(),
            child: ListTile(
              leading: Icon(Icons.admin_panel_settings),
              title: Text('Admin Dashboard'),
            ),
          ),

          // Shows upgrade prompt for non-premium users
          PremiumOnly(
            fallback: ListTile(
              leading: const Icon(Icons.lock),
              title: const Text('Unlock Premium Features'),
              onTap: () => context.go('/upgrade'),
            ),
            child: const ListTile(
              leading: Icon(Icons.star),
              title: Text('Premium Features'),
            ),
          ),
        ],
      ),
    );
  }
}
```
