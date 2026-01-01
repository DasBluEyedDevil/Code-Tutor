---
type: "THEORY"
title: "Passing Arguments"
---


### Method 1: Via Navigator


### Method 2: Type-Safe Arguments


**Much safer!** Type errors caught at compile time.



```dart
// Define argument class
class ProductDetailArguments {
  final int productId;
  final String name;

  ProductDetailArguments({required this.productId, required this.name});
}

// Navigate
Navigator.pushNamed(
  context,
  '/detail',
  arguments: ProductDetailArguments(productId: 123, name: 'Laptop'),
);

// Receive
class DetailScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments as ProductDetailArguments;

    return Scaffold(
      appBar: AppBar(title: Text(args.name)),
      body: Center(child: Text('Product ID: ${args.productId}')),
    );
  }
}
```
