---
type: "EXAMPLE"
title: "Custom Page Transitions"
---


Various custom page transition effects:



```dart
// Fade Transition
Route fadeRoute(Widget page) {
  return PageRouteBuilder(
    pageBuilder: (context, animation, secondaryAnimation) => page,
    transitionDuration: const Duration(milliseconds: 400),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      return FadeTransition(
        opacity: animation,
        child: child,
      );
    },
  );
}

// Slide from Bottom Transition
Route slideUpRoute(Widget page) {
  return PageRouteBuilder(
    pageBuilder: (context, animation, secondaryAnimation) => page,
    transitionDuration: const Duration(milliseconds: 300),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      final tween = Tween<Offset>(
        begin: const Offset(0, 1), // Start from bottom
        end: Offset.zero,
      ).chain(CurveTween(curve: Curves.easeOutCubic));
      
      return SlideTransition(
        position: animation.drive(tween),
        child: child,
      );
    },
  );
}

// Scale with Fade Transition
Route scaleRoute(Widget page) {
  return PageRouteBuilder(
    pageBuilder: (context, animation, secondaryAnimation) => page,
    transitionDuration: const Duration(milliseconds: 400),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      final curvedAnimation = CurvedAnimation(
        parent: animation,
        curve: Curves.easeOutBack,
      );
      
      return ScaleTransition(
        scale: Tween<double>(begin: 0.8, end: 1.0).animate(curvedAnimation),
        child: FadeTransition(
          opacity: animation,
          child: child,
        ),
      );
    },
  );
}

// Slide with Parallax Effect (current page slides slightly)
Route parallaxRoute(Widget page) {
  return PageRouteBuilder(
    pageBuilder: (context, animation, secondaryAnimation) => page,
    transitionDuration: const Duration(milliseconds: 400),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      // New page slides in from right
      final slideIn = Tween<Offset>(
        begin: const Offset(1, 0),
        end: Offset.zero,
      ).animate(CurvedAnimation(
        parent: animation,
        curve: Curves.easeOutCubic,
      ));
      
      // Old page slides slightly left (parallax)
      final slideOut = Tween<Offset>(
        begin: Offset.zero,
        end: const Offset(-0.3, 0),
      ).animate(CurvedAnimation(
        parent: secondaryAnimation,
        curve: Curves.easeOutCubic,
      ));
      
      return SlideTransition(
        position: slideIn,
        child: SlideTransition(
          position: slideOut,
          child: child,
        ),
      );
    },
  );
}

// Usage:
Navigator.push(context, fadeRoute(DetailScreen()));
Navigator.push(context, slideUpRoute(ModalScreen()));
Navigator.push(context, scaleRoute(PopupScreen()));
```
