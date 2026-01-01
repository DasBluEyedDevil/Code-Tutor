---
type: "THEORY"
title: "Path Parameters (Dynamic Routes)"
---


Handle URLs like `/user/123` or `/product/456`:




```dart
final router = GoRouter(
  routes: [
    GoRoute(
      path: '/',
      builder: (context, state) => HomeScreen(),
    ),
    GoRoute(
      path: '/user/:userId',  // :userId is a path parameter
      builder: (context, state) {
        final userId = state.pathParameters['userId']!;
        return UserScreen(userId: userId);
      },
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

// Navigate
context.go('/user/42');
context.go('/product/laptop-123');
```
