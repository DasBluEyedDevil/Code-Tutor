import 'package:flutter/material.dart';

class Product {
  final String id;
  final String title;
  final double price;
  
  Product({required this.id, required this.title, required this.price});
}

class ProductGrid extends StatelessWidget {
  final List<Product> products;
  
  const ProductGrid({super.key, required this.products});
  
  // TODO: Calculate columns based on width
  int _getColumnCount(double width) {
    // Return 1, 2, 3, or 4 based on width breakpoints
    return 2;
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Use LayoutBuilder to get available width
    // TODO: Build GridView with calculated column count
    return Container();
  }
}

class ProductCard extends StatelessWidget {
  final Product product;
  
  const ProductCard({super.key, required this.product});

  @override
  Widget build(BuildContext context) {
    // TODO: Build a card with image placeholder, title, and price
    return Container();
  }
}