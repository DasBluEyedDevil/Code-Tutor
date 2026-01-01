// Custom Page Transition Challenge
// Make a screen slide in from the left

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
            // TODO 1: Use Navigator.push with SlideFromLeftRoute
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

// TODO 2: Create SlideFromLeftRoute extending PageRouteBuilder
class SlideFromLeftRoute extends PageRouteBuilder {
  final Widget page;

  SlideFromLeftRoute({required this.page})
      : super(
          pageBuilder: (context, animation, secondaryAnimation) => page,
          transitionsBuilder: (context, animation, secondaryAnimation, child) {
            // TODO 3: Define the slide animation
            // begin: Offset(-1.0, 0.0) = starts off-screen left
            // end: Offset.zero = ends at center
            const begin = Offset(-1.0, 0.0);
            const end = Offset.zero;
            const curve = Curves.easeInOut;

            final tween = Tween(begin: begin, end: end).chain(
              CurveTween(curve: curve),
            );

            // TODO 4: Return SlideTransition with the animation
            return SlideTransition(
              position: animation.drive(tween),
              child: child,
            );
          },
          transitionDuration: const Duration(milliseconds: 300),
        );
}