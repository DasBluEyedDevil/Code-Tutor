---
type: "EXAMPLE"
title: "Your First GoRouter"
---



**Key differences:**
- Use `MaterialApp.router` instead of `MaterialApp`
- Pass `routerConfig` instead of `routes`
- Navigate with `context.go('/path')` instead of `Navigator.pushNamed`



```dart
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // Define router
  final GoRouter _router = GoRouter(
    routes: [
      GoRoute(
        path: '/',
        builder: (context, state) => HomeScreen(),
      ),
      GoRoute(
        path: '/details',
        builder: (context, state) => DetailsScreen(),
      ),
    ],
  );

  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      routerConfig: _router,  // Use router config!
      title: 'GoRouter Demo',
    );
  }
}

class HomeScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Home')),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            // Navigate with go()
            context.go('/details');
          },
          child: Text('Go to Details'),
        ),
      ),
    );
  }
}

class DetailsScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Details')),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            // Go back
            context.go('/');
          },
          child: Text('Back to Home'),
        ),
      ),
    );
  }
}
```
