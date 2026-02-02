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

// STEP 1: CartItem Model
class CartItem {
  final String id;
  final String name;
  final double price;
  final int quantity;

  const CartItem({
    required this.id,
    required this.name,
    required this.price,
    this.quantity = 1,
  });

  CartItem copyWith({
    String? id,
    String? name,
    double? price,
    int? quantity,
  }) {
    return CartItem(
      id: id ?? this.id,
      name: name ?? this.name,
      price: price ?? this.price,
      quantity: quantity ?? this.quantity,
    );
  }
}

// STEP 2: CartNotifier ViewModel
class CartNotifier extends Notifier<List<CartItem>> {
  @override
  List<CartItem> build() {
    return [];  // Start with empty cart
  }

  // STEP 3: Add item (or increase quantity if exists)
  void addItem(CartItem item) {
    final existingIndex = state.indexWhere((i) => i.id == item.id);
    
    if (existingIndex >= 0) {
      // Item exists - increase quantity
      state = [
        for (int i = 0; i < state.length; i++)
          if (i == existingIndex)
            state[i].copyWith(quantity: state[i].quantity + item.quantity)
          else
            state[i]
      ];
    } else {
      // New item - add to cart
      state = [...state, item];
    }
  }

  // STEP 4: Remove item
  void removeItem(String id) {
    state = state.where((item) => item.id != id).toList();
  }

  // STEP 5: Update quantity
  void updateQuantity(String id, int quantity) {
    if (quantity <= 0) {
      removeItem(id);
      return;
    }
    
    state = state.map((item) {
      if (item.id == id) {
        return item.copyWith(quantity: quantity);
      }
      return item;
    }).toList();
  }

  // STEP 6: Clear cart
  void clear() {
    state = [];
  }

  // STEP 7: Total items (sum of quantities)
  int get totalItems {
    return state.fold(0, (sum, item) => sum + item.quantity);
  }

  // STEP 8: Total price
  double get totalPrice {
    return state.fold(0.0, (sum, item) => sum + (item.price * item.quantity));
  }
}

// STEP 9: Create the provider
final cartProvider = NotifierProvider<CartNotifier, List<CartItem>>(() {
  return CartNotifier();
});

// Sample products for testing
final sampleProducts = [
  const CartItem(id: '1', name: 'Apple', price: 1.50),
  const CartItem(id: '2', name: 'Banana', price: 0.75),
  const CartItem(id: '3', name: 'Orange', price: 2.00),
  const CartItem(id: '4', name: 'Milk', price: 3.50),
];

// STEP 10: Cart Screen UI
class CartScreen extends ConsumerWidget {
  const CartScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch the cart items
    final cartItems = ref.watch(cartProvider);
    // Get the notifier for computed values and methods
    final cart = ref.watch(cartProvider.notifier);

    return Scaffold(
      appBar: AppBar(
        title: Text('Cart (${cart.totalItems} items)'),
        actions: [
          if (cartItems.isNotEmpty)
            IconButton(
              icon: const Icon(Icons.delete_sweep),
              onPressed: () => ref.read(cartProvider.notifier).clear(),
              tooltip: 'Clear Cart',
            ),
        ],
      ),
      body: Column(
        children: [
          // Product buttons
          Container(
            padding: const EdgeInsets.all(16),
            color: Colors.grey[100],
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Add Products:',
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 16),
                ),
                const SizedBox(height: 8),
                Wrap(
                  spacing: 8,
                  runSpacing: 8,
                  children: sampleProducts.map((product) {
                    return ElevatedButton(
                      onPressed: () {
                        ref.read(cartProvider.notifier).addItem(product);
                      },
                      child: Text('${product.name} (\$${product.price.toStringAsFixed(2)})'),
                    );
                  }).toList(),
                ),
              ],
            ),
          ),
          
          // Cart items list
          Expanded(
            child: cartItems.isEmpty
                ? const Center(
                    child: Text(
                      'Cart is empty\nTap a product to add it!',
                      textAlign: TextAlign.center,
                      style: TextStyle(fontSize: 18, color: Colors.grey),
                    ),
                  )
                : ListView.builder(
                    itemCount: cartItems.length,
                    itemBuilder: (context, index) {
                      final item = cartItems[index];
                      return ListTile(
                        title: Text(item.name),
                        subtitle: Text(
                          '\$${item.price.toStringAsFixed(2)} x ${item.quantity} = \$${(item.price * item.quantity).toStringAsFixed(2)}',
                        ),
                        trailing: Row(
                          mainAxisSize: MainAxisSize.min,
                          children: [
                            IconButton(
                              icon: const Icon(Icons.remove_circle_outline),
                              onPressed: () {
                                ref.read(cartProvider.notifier).updateQuantity(
                                  item.id,
                                  item.quantity - 1,
                                );
                              },
                            ),
                            Text(
                              '${item.quantity}',
                              style: const TextStyle(fontSize: 18),
                            ),
                            IconButton(
                              icon: const Icon(Icons.add_circle_outline),
                              onPressed: () {
                                ref.read(cartProvider.notifier).updateQuantity(
                                  item.id,
                                  item.quantity + 1,
                                );
                              },
                            ),
                            IconButton(
                              icon: const Icon(Icons.delete, color: Colors.red),
                              onPressed: () {
                                ref.read(cartProvider.notifier).removeItem(item.id);
                              },
                            ),
                          ],
                        ),
                      );
                    },
                  ),
          ),
          
          // Total bar
          if (cartItems.isNotEmpty)
            Container(
              padding: const EdgeInsets.all(16),
              decoration: BoxDecoration(
                color: Colors.blue[50],
                boxShadow: [
                  BoxShadow(
                    color: Colors.grey.withOpacity(0.3),
                    blurRadius: 4,
                    offset: const Offset(0, -2),
                  ),
                ],
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    'Total: ${cart.totalItems} items',
                    style: const TextStyle(fontSize: 16),
                  ),
                  Text(
                    '\$${cart.totalPrice.toStringAsFixed(2)}',
                    style: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                      color: Colors.blue,
                    ),
                  ),
                ],
              ),
            ),
        ],
      ),
    );
  }
}

// KEY CONCEPTS DEMONSTRATED:
//
// 1. CartItem Model: Pure data class with copyWith for immutability
//
// 2. CartNotifier: ViewModel that encapsulates all cart logic
//    - State is List<CartItem>
//    - Methods modify state immutably
//    - Getters provide computed values
//
// 3. NotifierProvider: Makes CartNotifier available app-wide
//
// 4. ref.watch(): Used to display cart items (rebuilds on change)
//
// 5. ref.read(): Used in callbacks to call notifier methods
//
// 6. Immutable updates: Always create new lists/objects, never mutate