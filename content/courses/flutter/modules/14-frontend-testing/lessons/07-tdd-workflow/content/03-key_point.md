---
type: "KEY_POINT"
title: "TDD Best Practices"
---


**1. Write the simplest test first:**
```dart
test('initial cart is empty', () {
  final cart = container.read(cartProvider);
  expect(cart.items, isEmpty);
});
```

**2. One behavior per test:**
- Good: 'addItem increases item count'
- Bad: 'addItem works correctly' (too vague)

**3. Test edge cases:**
- Empty state
- Boundary values
- Error conditions

**4. Keep tests fast:**
- Mock external dependencies
- Avoid actual network calls
- Use ProviderContainer for isolation

