// Scroll & Refresh Challenge
// Create horizontal categories and vertical product list

import 'package:flutter/material.dart';

void main() {
  runApp(const ShopApp());
}

class ShopApp extends StatelessWidget {
  const ShopApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Shop')),
        body: const ShopScreen(),
      ),
    );
  }
}

class ShopScreen extends StatefulWidget {
  const ShopScreen({super.key});

  @override
  State<ShopScreen> createState() => _ShopScreenState();
}

class _ShopScreenState extends State<ShopScreen> {
  final categories = ['All', 'Electronics', 'Clothing', 'Books', 'Home'];
  final products = ['Product 1', 'Product 2', 'Product 3', 'Product 4', 'Product 5'];

  Future<void> _handleRefresh() async {
    // TODO: Simulate refresh delay
    await Future.delayed(const Duration(seconds: 1));
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // TODO 1: Horizontal scrolling category chips
        // Hint: SizedBox with height + horizontal ListView
        SizedBox(
          height: 50,
          child: ListView.builder(
            scrollDirection: Axis.horizontal,
            itemCount: categories.length,
            itemBuilder: (context, index) {
              return Chip(label: Text(categories[index]));
            },
          ),
        ),
        
        // TODO 2 & 3: Vertical product list with RefreshIndicator
        // Wrap ListView in RefreshIndicator for pull-to-refresh
        Expanded(
          child: ListView.builder(
            itemCount: products.length,
            itemBuilder: (context, index) {
              return ListTile(title: Text(products[index]));
            },
          ),
        ),
      ],
    );
  }
}