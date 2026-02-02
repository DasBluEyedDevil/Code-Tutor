---
type: "EXAMPLE"
title: "TDD in Action: Building a Cart"
---




```dart
// Step 1: RED - Write failing test
test('addItem adds product to cart', () {
  final container = ProviderContainer();
  final notifier = container.read(cartProvider.notifier);

  notifier.addItem(Product(id: '1', name: 'Widget', price: 9.99));

  expect(container.read(cartProvider).items, hasLength(1));
});

// Run test - it fails (CartNotifier doesn't exist)

// Step 2: GREEN - Minimum code to pass
class CartNotifier extends Notifier<CartState> {
  @override
  CartState build() => const CartState();

  void addItem(Product product) {
    state = state.copyWith(
      items: [...state.items, CartItem(product: product)],
    );
  }
}

// Run test - it passes!

// Step 3: REFACTOR - Improve without breaking tests
void addItem(Product product) {
  final existingIndex = state.items.indexWhere(
    (item) => item.product.id == product.id,
  );

  if (existingIndex >= 0) {
    // Increase quantity if exists
    final updated = state.items[existingIndex].copyWith(
      quantity: state.items[existingIndex].quantity + 1,
    );
    state = state.copyWith(
      items: [...state.items]..replaceRange(existingIndex, existingIndex + 1, [updated]),
    );
  } else {
    state = state.copyWith(
      items: [...state.items, CartItem(product: product)],
    );
  }
}

// Run tests - still passes!
```
