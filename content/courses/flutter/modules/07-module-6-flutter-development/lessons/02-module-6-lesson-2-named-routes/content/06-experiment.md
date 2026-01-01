---
type: "EXPERIMENT"
title: "Route Constants (Best Practice)"
---


Avoid typos with constants:


**Benefits:**
- Autocomplete works
- Refactoring is easy
- Typos caught at compile time



```dart
// routes.dart
class AppRoutes {
  static const String home = '/';
  static const String products = '/products';
  static const String productDetail = '/product-detail';
  static const String cart = '/cart';
  static const String checkout = '/checkout';
  static const String profile = '/profile';
  static const String settings = '/settings';
}

// main.dart
MaterialApp(
  routes: {
    AppRoutes.home: (context) => HomeScreen(),
    AppRoutes.products: (context) => ProductsScreen(),
    AppRoutes.productDetail: (context) => ProductDetailScreen(),
    AppRoutes.cart: (context) => CartScreen(),
    AppRoutes.checkout: (context) => CheckoutScreen(),
  },
);

// Usage
Navigator.pushNamed(context, AppRoutes.productDetail);
Navigator.pushNamed(context, AppRoutes.cart);
```
