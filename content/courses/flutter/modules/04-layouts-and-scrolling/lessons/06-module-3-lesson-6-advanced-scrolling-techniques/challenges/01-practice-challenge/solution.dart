// Solution: Categories with Pull-to-Refresh Product List
// Horizontal chips, vertical list, and RefreshIndicator

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
  String selectedCategory = 'All';
  final categories = ['All', 'Electronics', 'Clothing', 'Books', 'Home', 'Sports'];
  
  final products = List.generate(
    10,
    (i) => {'name': 'Product ${i + 1}', 'price': (i + 1) * 9.99},
  );

  Future<void> _handleRefresh() async {
    // Simulate network delay
    await Future.delayed(const Duration(seconds: 1));
    // In real app, fetch new data here
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // 1. Horizontal scrolling category chips
        SizedBox(
          height: 50,
          child: ListView.builder(
            scrollDirection: Axis.horizontal,
            padding: const EdgeInsets.symmetric(horizontal: 8),
            itemCount: categories.length,
            itemBuilder: (context, index) {
              final category = categories[index];
              final isSelected = category == selectedCategory;
              return Padding(
                padding: const EdgeInsets.symmetric(horizontal: 4),
                child: FilterChip(
                  label: Text(category),
                  selected: isSelected,
                  onSelected: (_) => setState(() => selectedCategory = category),
                  backgroundColor: Colors.grey.shade200,
                  selectedColor: Colors.blue.shade100,
                ),
              );
            },
          ),
        ),
        const Divider(),
        
        // 2 & 3. Vertical product list with pull-to-refresh
        Expanded(
          child: RefreshIndicator(
            onRefresh: _handleRefresh,
            child: ListView.builder(
              itemCount: products.length,
              itemBuilder: (context, index) {
                final product = products[index];
                return ListTile(
                  leading: Container(
                    width: 50,
                    height: 50,
                    color: Colors.grey.shade300,
                    child: const Icon(Icons.shopping_bag),
                  ),
                  title: Text(product['name'] as String),
                  subtitle: Text('\$${(product['price'] as double).toStringAsFixed(2)}'),
                  trailing: IconButton(
                    icon: const Icon(Icons.add_shopping_cart),
                    onPressed: () {},
                  ),
                );
              },
            ),
          ),
        ),
      ],
    );
  }
}

// Key concepts:
// - Horizontal ListView with scrollDirection: Axis.horizontal
// - FilterChip for selectable category pills
// - RefreshIndicator wraps scrollable for pull-to-refresh
// - onRefresh must return a Future (async function)