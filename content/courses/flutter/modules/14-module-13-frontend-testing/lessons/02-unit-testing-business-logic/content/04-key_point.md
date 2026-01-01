---
type: "KEY_POINT"
title: "Test Structure: AAA Pattern"
---


Follow the **Arrange-Act-Assert** pattern:

```dart
test('adds item to cart', () {
  // Arrange - Set up test data
  final item = CartItem(id: '1', name: 'Widget', price: 9.99);

  // Act - Perform the action
  notifier.addItem(item);

  // Assert - Verify the result
  expect(container.read(cartProvider).items, contains(item));
});
```

