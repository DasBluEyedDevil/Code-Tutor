// Product Grid Challenge
// Create a 3-column grid of products

import 'package:flutter/material.dart';

void main() {
  runApp(const ProductGridApp());
}

class ProductGridApp extends StatelessWidget {
  const ProductGridApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Product Grid')),
        body: const ProductGrid(),
      ),
    );
  }
}

class ProductGrid extends StatelessWidget {
  const ProductGrid({super.key});

  @override
  Widget build(BuildContext context) {
    // Sample product names
    final products = ['Laptop', 'Phone', 'Headphones', 'Watch', 
                      'Camera', 'Tablet', 'Speaker', 'Keyboard', 'Mouse'];

    return Padding(
      padding: const EdgeInsets.all(8),
      // TODO: Use GridView.builder with SliverGridDelegateWithFixedCrossAxisCount
      child: GridView.builder(
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 3, // TODO: 3 columns
          crossAxisSpacing: 8, // TODO: horizontal spacing
          mainAxisSpacing: 8, // TODO: vertical spacing
        ),
        itemCount: products.length,
        itemBuilder: (context, index) {
          // TODO: Return a Card with image and product name
          return Card(
            child: Center(child: Text(products[index])),
          );
        },
      ),
    );
  }
}