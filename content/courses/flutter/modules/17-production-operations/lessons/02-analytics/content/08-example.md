---
type: "EXAMPLE"
title: "Screen Tracking with Navigation Observer"
---


Track screen views automatically using Firebase's NavigatorObserver:



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'package:firebase_analytics/firebase_analytics.dart';

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  static final FirebaseAnalytics analytics = FirebaseAnalytics.instance;
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'My App',
      navigatorObservers: [
        // This automatically logs screen views
        FirebaseAnalyticsObserver(analytics: analytics),
      ],
      routes: {
        '/': (context) => const HomeScreen(),
        '/products': (context) => const ProductsScreen(),
        '/cart': (context) => const CartScreen(),
        '/profile': (context) => const ProfileScreen(),
      },
    );
  }
}

// For named routes, screen name is automatically captured.
// For custom screen names, use RouteSettings:

class ProductDetailScreen extends StatelessWidget {
  final String productId;
  
  const ProductDetailScreen({super.key, required this.productId});
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Product $productId')),
      body: const Center(child: Text('Product Details')),
    );
  }
}

// Navigate with explicit route settings
void navigateToProduct(BuildContext context, String productId) {
  Navigator.of(context).push(
    MaterialPageRoute(
      settings: RouteSettings(name: '/product/$productId'),
      builder: (context) => ProductDetailScreen(productId: productId),
    ),
  );
}
```
