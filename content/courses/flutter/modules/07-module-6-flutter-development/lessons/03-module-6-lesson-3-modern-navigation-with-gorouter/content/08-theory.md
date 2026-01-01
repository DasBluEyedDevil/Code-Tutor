---
type: "THEORY"
title: "Named Routes (Type-Safe)"
---


Instead of string paths everywhere, use named routes:




```dart
final router = GoRouter(
  routes: [
    GoRoute(
      path: '/',
      name: 'home',  // Give it a name!
      builder: (context, state) => HomeScreen(),
    ),
    GoRoute(
      path: '/product/:id',
      name: 'product',
      builder: (context, state) {
        final id = state.pathParameters['id']!;
        return ProductScreen(productId: id);
      },
    ),
  ],
);

// Navigate by name
context.goNamed('home');
context.goNamed('product', pathParameters: {'id': '123'});

// With query parameters
context.goNamed('search', queryParameters: {'q': 'flutter', 'sort': 'newest'});
```
