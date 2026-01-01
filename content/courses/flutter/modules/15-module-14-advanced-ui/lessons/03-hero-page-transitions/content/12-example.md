---
type: "EXAMPLE"
title: "GoRouter with Custom Transitions"
---


Implement custom transitions in GoRouter:



```dart
import 'package:go_router/go_router.dart';

// Define custom transition builders
CustomTransitionPage fadeTransitionPage({
  required LocalKey key,
  required Widget child,
}) {
  return CustomTransitionPage(
    key: key,
    child: child,
    transitionDuration: const Duration(milliseconds: 300),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      return FadeTransition(opacity: animation, child: child);
    },
  );
}

CustomTransitionPage slideUpTransitionPage({
  required LocalKey key,
  required Widget child,
}) {
  return CustomTransitionPage(
    key: key,
    child: child,
    transitionDuration: const Duration(milliseconds: 400),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      final tween = Tween<Offset>(
        begin: const Offset(0, 1),
        end: Offset.zero,
      ).chain(CurveTween(curve: Curves.easeOutCubic));
      
      return SlideTransition(
        position: animation.drive(tween),
        child: child,
      );
    },
  );
}

// GoRouter configuration with custom transitions
final router = GoRouter(
  routes: [
    GoRoute(
      path: '/',
      builder: (context, state) => const HomeScreen(),
    ),
    GoRoute(
      path: '/details/:id',
      pageBuilder: (context, state) {
        final id = state.pathParameters['id']!;
        return fadeTransitionPage(
          key: state.pageKey,
          child: DetailScreen(id: id),
        );
      },
    ),
    GoRoute(
      path: '/settings',
      pageBuilder: (context, state) {
        return slideUpTransitionPage(
          key: state.pageKey,
          child: const SettingsScreen(),
        );
      },
    ),
    // Modal-style presentation
    GoRoute(
      path: '/modal',
      pageBuilder: (context, state) {
        return CustomTransitionPage(
          key: state.pageKey,
          fullscreenDialog: true,
          child: const ModalScreen(),
          transitionsBuilder: (context, animation, secondary, child) {
            final curved = CurvedAnimation(
              parent: animation,
              curve: Curves.easeOutCubic,
            );
            return SlideTransition(
              position: Tween<Offset>(
                begin: const Offset(0, 1),
                end: Offset.zero,
              ).animate(curved),
              child: child,
            );
          },
        );
      },
    ),
  ],
);
```
