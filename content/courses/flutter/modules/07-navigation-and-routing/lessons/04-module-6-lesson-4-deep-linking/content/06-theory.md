---
type: "THEORY"
title: "Step 3: Basic Deep Link Handling"
---



**Test it:**
1. Run the app
2. Send yourself a link: `https://mycompany.com/product/456`
3. Tap the link → App opens to Product 456!



```dart
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:app_links/app_links.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  @override
  _MyAppState createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  late final AppLinks _appLinks;
  final GoRouter _router = GoRouter(
    routes: [
      GoRoute(
        path: '/',
        builder: (context, state) => HomeScreen(),
      ),
      GoRoute(
        path: '/product/:productId',
        builder: (context, state) {
          final productId = state.pathParameters['productId']!;
          return ProductScreen(productId: productId);
        },
      ),
    ],
  );

  @override
  void initState() {
    super.initState();
    _initDeepLinks();
  }

  Future<void> _initDeepLinks() async {
    _appLinks = AppLinks();

    // Handle deep link when app is already running
    _appLinks.uriLinkStream.listen((uri) {
      print('Deep link received: $uri');
      _router.go(uri.path);
    });

    // Handle deep link that opened the app
    final initialUri = await _appLinks.getInitialLink();
    if (initialUri != null) {
      print('App opened with: $initialUri');
      _router.go(initialUri.path);
    }
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      routerConfig: _router,
      title: 'Deep Linking Demo',
    );
  }
}

class HomeScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Home')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Home Screen', style: TextStyle(fontSize: 24)),
            SizedBox(height: 24),
            ElevatedButton(
              onPressed: () => context.go('/product/123'),
              child: Text('Go to Product 123'),
            ),
          ],
        ),
      ),
    );
  }
}

class ProductScreen extends StatelessWidget {
  final String productId;

  ProductScreen({required this.productId});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Product $productId')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(Icons.shopping_bag, size: 100),
            SizedBox(height: 16),
            Text(
              'Product ID: $productId',
              style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 16),
            Text('This screen was opened via deep link!'),
          ],
        ),
      ),
    );
  }
}
```
