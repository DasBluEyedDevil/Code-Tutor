---
type: "EXAMPLE"
title: "TDD in Action: Building a Discount Calculator"
---


Let's walk through TDD for a discount calculator feature:



```dart
// === STEP 1: RED - Write a failing test ===

// test/discount_calculator_test.dart
import 'package:test/test.dart';
import 'package:my_api/services/discount_calculator.dart';

void main() {
  group('DiscountCalculator', () {
    test('applies 10% discount for orders over \$100', () {
      final calculator = DiscountCalculator();
      final result = calculator.calculateDiscount(150.0);
      
      expect(result, 135.0); // 10% off = $15 discount
    });
  });
}

// Run: dart test
// Result: RED - DiscountCalculator does not exist!

// === STEP 2: GREEN - Minimal implementation ===

// lib/services/discount_calculator.dart
class DiscountCalculator {
  double calculateDiscount(double orderTotal) {
    if (orderTotal > 100) {
      return orderTotal * 0.9; // 10% discount
    }
    return orderTotal;
  }
}

// Run: dart test
// Result: GREEN - Test passes!

// === STEP 3: REFACTOR - Improve the code ===

class DiscountCalculator {
  static const double _discountThreshold = 100.0;
  static const double _discountPercentage = 0.10;
  
  double calculateDiscount(double orderTotal) {
    if (orderTotal > _discountThreshold) {
      final discount = orderTotal * _discountPercentage;
      return orderTotal - discount;
    }
    return orderTotal;
  }
}

// Run: dart test
// Result: GREEN - Still passes!

// === ADD MORE TESTS ===

test('no discount for orders under \$100', () {
  final calculator = DiscountCalculator();
  expect(calculator.calculateDiscount(50.0), 50.0);
});

test('no discount for exactly \$100', () {
  final calculator = DiscountCalculator();
  expect(calculator.calculateDiscount(100.0), 100.0);
});

test('handles zero order total', () {
  final calculator = DiscountCalculator();
  expect(calculator.calculateDiscount(0.0), 0.0);
});
```
