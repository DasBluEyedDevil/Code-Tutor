---
type: "THEORY"
title: "Nested Navigation (Sub-routes)"
---


Create child routes:




```dart
GoRoute(
  path: '/settings',
  builder: (context, state) => SettingsScreen(),
  routes: [
    // Child route: /settings/account
    GoRoute(
      path: 'account',
      builder: (context, state) => AccountSettingsScreen(),
    ),
    // Child route: /settings/notifications
    GoRoute(
      path: 'notifications',
      builder: (context, state) => NotificationSettingsScreen(),
    ),
  ],
),

// Navigate
context.go('/settings/account');
context.go('/settings/notifications');
```
