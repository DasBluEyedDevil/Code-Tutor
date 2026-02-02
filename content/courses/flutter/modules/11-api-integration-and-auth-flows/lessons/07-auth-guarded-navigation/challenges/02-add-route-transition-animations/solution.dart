// lib/router/page_transitions.dart

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

enum TransitionType { fade, slideRight, slideUp, none }

class AppPageTransitions {
  static const _duration = Duration(milliseconds: 300);

  static CustomTransitionPage<void> buildPage({
    required Widget child,
    required GoRouterState state,
    required TransitionType type,
  }) {
    return CustomTransitionPage<void>(
      key: state.pageKey,
      child: child,
      transitionDuration: type == TransitionType.none
          ? Duration.zero
          : _duration,
      transitionsBuilder: (context, animation, secondaryAnimation, child) {
        switch (type) {
          case TransitionType.fade:
            return _fadeTransition(
                context, animation, secondaryAnimation, child);
          case TransitionType.slideRight:
            return _slideRightTransition(
                context, animation, secondaryAnimation, child);
          case TransitionType.slideUp:
            return _slideUpTransition(
                context, animation, secondaryAnimation, child);
          case TransitionType.none:
            return child;
        }
      },
    );
  }

  static Widget _fadeTransition(
    BuildContext context,
    Animation<double> animation,
    Animation<double> secondaryAnimation,
    Widget child,
  ) {
    return FadeTransition(
      opacity: CurvedAnimation(
        parent: animation,
        curve: Curves.easeInOut,
      ),
      child: child,
    );
  }

  static Widget _slideRightTransition(
    BuildContext context,
    Animation<double> animation,
    Animation<double> secondaryAnimation,
    Widget child,
  ) {
    final offsetAnimation = Tween<Offset>(
      begin: const Offset(1.0, 0.0),
      end: Offset.zero,
    ).animate(CurvedAnimation(
      parent: animation,
      curve: Curves.easeOutCubic,
    ));

    return SlideTransition(
      position: offsetAnimation,
      child: child,
    );
  }

  static Widget _slideUpTransition(
    BuildContext context,
    Animation<double> animation,
    Animation<double> secondaryAnimation,
    Widget child,
  ) {
    final offsetAnimation = Tween<Offset>(
      begin: const Offset(0.0, 1.0),
      end: Offset.zero,
    ).animate(CurvedAnimation(
      parent: animation,
      curve: Curves.easeOutCubic,
    ));

    return SlideTransition(
      position: offsetAnimation,
      child: child,
    );
  }
}

// Usage in router:
// GoRoute(
//   path: '/login',
//   pageBuilder: (context, state) => AppPageTransitions.buildPage(
//     child: const LoginScreen(),
//     state: state,
//     type: TransitionType.fade,
//   ),
// ),