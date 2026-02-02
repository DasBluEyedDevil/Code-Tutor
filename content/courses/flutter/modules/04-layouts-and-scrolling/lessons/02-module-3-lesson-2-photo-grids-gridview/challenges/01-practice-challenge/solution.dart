// Solution: Product Grid with GridView
// Shows a 3-column grid of products with images and names

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

  // Sample product data
  static const List<Map<String, String>> products = [
    {'name': 'Laptop', 'image': 'https://picsum.photos/200?1'},
    {'name': 'Phone', 'image': 'https://picsum.photos/200?2'},
    {'name': 'Headphones', 'image': 'https://picsum.photos/200?3'},
    {'name': 'Watch', 'image': 'https://picsum.photos/200?4'},
    {'name': 'Camera', 'image': 'https://picsum.photos/200?5'},
    {'name': 'Tablet', 'image': 'https://picsum.photos/200?6'},
    {'name': 'Speaker', 'image': 'https://picsum.photos/200?7'},
    {'name': 'Keyboard', 'image': 'https://picsum.photos/200?8'},
    {'name': 'Mouse', 'image': 'https://picsum.photos/200?9'},
  ];

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8),
      child: GridView.builder(
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 3,       // 3 columns
          crossAxisSpacing: 8,     // Horizontal spacing
          mainAxisSpacing: 8,      // Vertical spacing
          childAspectRatio: 0.75,  // Height = width / 0.75
        ),
        itemCount: products.length,
        itemBuilder: (context, index) {
          final product = products[index];
          return ProductCard(
            name: product['name']!,
            imageUrl: product['image']!,
          );
        },
      ),
    );
  }
}

class ProductCard extends StatelessWidget {
  final String name;
  final String imageUrl;

  const ProductCard({
    super.key,
    required this.name,
    required this.imageUrl,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      clipBehavior: Clip.antiAlias,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          // Product image
          Expanded(
            child: Image.network(
              imageUrl,
              fit: BoxFit.cover,
              errorBuilder: (_, __, ___) => const Center(
                child: Icon(Icons.image_not_supported),
              ),
            ),
          ),
          // Product name
          Padding(
            padding: const EdgeInsets.all(8),
            child: Text(
              name,
              textAlign: TextAlign.center,
              style: const TextStyle(fontWeight: FontWeight.w500),
            ),
          ),
        ],
      ),
    );
  }
}

// Key concepts:
// - GridView.builder: Efficient grid for many items
// - SliverGridDelegateWithFixedCrossAxisCount: Fixed columns
// - crossAxisSpacing/mainAxisSpacing: Spacing between items
// - childAspectRatio: Controls item height relative to width