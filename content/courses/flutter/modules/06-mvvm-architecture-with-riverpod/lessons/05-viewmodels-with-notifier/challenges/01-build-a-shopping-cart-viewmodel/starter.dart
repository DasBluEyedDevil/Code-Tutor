import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

void main() {
  runApp(const ProviderScope(child: MyApp()));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Shopping Cart',
      theme: ThemeData(primarySwatch: Colors.blue),
      home: const CartScreen(),
    );
  }
}

// TODO 1: Create CartItem class
// - id (String)
// - name (String)
// - price (double)
// - quantity (int, default 1)
// - copyWith method for immutable updates
class CartItem {
  // Your code here
}

// TODO 2: Create CartNotifier
class CartNotifier extends Notifier<List<CartItem>> {
  @override
  List<CartItem> build() {
    // TODO: Return initial empty state
    return [];
  }

  // TODO 3: Implement addItem
  // If item with same id exists, increase quantity
  // Otherwise, add new item
  void addItem(CartItem item) {
    // Your code here
  }

  // TODO 4: Implement removeItem
  void removeItem(String id) {
    // Your code here
  }

  // TODO 5: Implement updateQuantity
  // If quantity <= 0, remove the item
  void updateQuantity(String id, int quantity) {
    // Your code here
  }

  // TODO 6: Implement clear
  void clear() {
    // Your code here
  }

  // TODO 7: Add totalItems getter (sum of all quantities)
  int get totalItems {
    return 0;
  }

  // TODO 8: Add totalPrice getter (sum of price * quantity)
  double get totalPrice {
    return 0.0;
  }
}

// TODO 9: Create the provider
// final cartProvider = NotifierProvider<CartNotifier, List<CartItem>>(...)

// TODO 10: Build the UI
class CartScreen extends ConsumerWidget {
  const CartScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // TODO: Watch cart items
    // TODO: Display items in a ListView
    // TODO: Show total items and total price
    // TODO: Add buttons to add sample items
    // TODO: Add clear cart button
    
    return Scaffold(
      appBar: AppBar(
        title: const Text('Shopping Cart'),
      ),
      body: const Center(
        child: Text('Implement cart UI here'),
      ),
    );
  }
}