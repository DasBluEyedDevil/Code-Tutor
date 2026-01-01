// Solution: Custom Page Transition - Slide from Left
// Uses PageRouteBuilder with custom SlideTransition

import 'package:flutter/material.dart';

void main() {
  runApp(const TransitionApp());
}

class TransitionApp extends StatelessWidget {
  const TransitionApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const HomeScreen(),
    );
  }
}

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            Navigator.push(
              context,
              SlideFromLeftRoute(page: const DetailScreen()),
            );
          },
          child: const Text('Open Detail (Slide from Left)'),
        ),
      ),
    );
  }
}

class DetailScreen extends StatelessWidget {
  const DetailScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Detail')),
      body: const Center(
        child: Text('This screen slid in from the left!'),
      ),
    );
  }
}

// Custom route that slides from left
class SlideFromLeftRoute extends PageRouteBuilder {
  final Widget page;

  SlideFromLeftRoute({required this.page})
      : super(
          pageBuilder: (context, animation, secondaryAnimation) => page,
          transitionsBuilder: (context, animation, secondaryAnimation, child) {
            // Slide from left: start at -1.0 (off-screen left), end at 0.0 (center)
            const begin = Offset(-1.0, 0.0);
            const end = Offset.zero;
            const curve = Curves.easeInOut;

            final tween = Tween(begin: begin, end: end).chain(
              CurveTween(curve: curve),
            );

            return SlideTransition(
              position: animation.drive(tween),
              child: child,
            );
          },
          transitionDuration: const Duration(milliseconds: 300),
        );
}

// Alternative: Reusable function
Route slideFromLeftRoute(Widget page) {
  return PageRouteBuilder(
    pageBuilder: (_, __, ___) => page,
    transitionsBuilder: (_, animation, __, child) {
      return SlideTransition(
        position: Tween<Offset>(
          begin: const Offset(-1.0, 0.0),
          end: Offset.zero,
        ).animate(CurvedAnimation(
          parent: animation,
          curve: Curves.easeInOut,
        )),
        child: child,
      );
    },
  );
}

// Key concepts:
// - PageRouteBuilder: Custom route with transitions
// - SlideTransition: Animates position with Offset
// - Offset(-1.0, 0): Left of screen
// - Offset(1.0, 0): Right of screen
// - Offset(0, -1.0): Top of screen
// - Tween + CurveTween: Smooth animation curve