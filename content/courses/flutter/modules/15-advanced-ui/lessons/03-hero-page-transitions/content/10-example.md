---
type: "EXAMPLE"
title: "Reusable Custom Page Route Class"
---


Create a reusable class for consistent transitions:



```dart
enum PageTransitionType {
  fade,
  slideUp,
  slideRight,
  scale,
  rotation,
}

class CustomPageRoute<T> extends PageRouteBuilder<T> {
  final Widget page;
  final PageTransitionType transitionType;
  
  CustomPageRoute({
    required this.page,
    this.transitionType = PageTransitionType.fade,
  }) : super(
    pageBuilder: (context, animation, secondaryAnimation) => page,
    transitionDuration: const Duration(milliseconds: 300),
    reverseTransitionDuration: const Duration(milliseconds: 300),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      switch (transitionType) {
        case PageTransitionType.fade:
          return FadeTransition(opacity: animation, child: child);
          
        case PageTransitionType.slideUp:
          return SlideTransition(
            position: Tween<Offset>(
              begin: const Offset(0, 1),
              end: Offset.zero,
            ).animate(CurvedAnimation(
              parent: animation,
              curve: Curves.easeOutCubic,
            )),
            child: child,
          );
          
        case PageTransitionType.slideRight:
          return SlideTransition(
            position: Tween<Offset>(
              begin: const Offset(1, 0),
              end: Offset.zero,
            ).animate(CurvedAnimation(
              parent: animation,
              curve: Curves.easeOutCubic,
            )),
            child: child,
          );
          
        case PageTransitionType.scale:
          return ScaleTransition(
            scale: Tween<double>(begin: 0.0, end: 1.0).animate(
              CurvedAnimation(
                parent: animation,
                curve: Curves.easeOutBack,
              ),
            ),
            child: FadeTransition(opacity: animation, child: child),
          );
          
        case PageTransitionType.rotation:
          return RotationTransition(
            turns: Tween<double>(begin: 0.5, end: 1.0).animate(
              CurvedAnimation(
                parent: animation,
                curve: Curves.easeOutCubic,
              ),
            ),
            child: FadeTransition(opacity: animation, child: child),
          );
      }
    },
  );
}

// Usage:
Navigator.push(
  context,
  CustomPageRoute(
    page: DetailScreen(),
    transitionType: PageTransitionType.slideUp,
  ),
);
```
