---
type: "THEORY"
title: "onGenerateRoute (Advanced)"
---


For dynamic routes or custom logic:




```dart
MaterialApp(
  onGenerateRoute: (settings) {
    // Handle /product/:id
    if (settings.name?.startsWith('/product/') == true) {
      final productId = settings.name!.split('/').last;

      return MaterialPageRoute(
        builder: (context) => ProductDetailScreen(productId: productId),
      );
    }

    // Handle /user/:username
    if (settings.name?.startsWith('/user/') == true) {
      final username = settings.name!.split('/').last;

      return MaterialPageRoute(
        builder: (context) => UserProfileScreen(username: username),
      );
    }

    // Default route
    return MaterialPageRoute(builder: (context) => HomeScreen());
  },
);

// Navigate
Navigator.pushNamed(context, '/product/123');
Navigator.pushNamed(context, '/user/john_doe');
```
