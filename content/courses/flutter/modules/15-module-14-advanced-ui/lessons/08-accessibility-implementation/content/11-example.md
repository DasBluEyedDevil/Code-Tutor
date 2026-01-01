---
type: "EXAMPLE"
title: "Accessibility Testing Implementation"
---


Writing tests to verify accessibility:



```dart
import 'package:flutter/material.dart';
import 'package:flutter/semantics.dart';
import 'package:flutter_test/flutter_test.dart';

// Example widget to test
class ProductCard extends StatelessWidget {
  final String name;
  final String price;
  final double rating;
  final VoidCallback onTap;

  const ProductCard({
    super.key,
    required this.name,
    required this.price,
    required this.rating,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return MergeSemantics(
      child: Semantics(
        button: true,
        child: GestureDetector(
          onTap: onTap,
          child: Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(name, style: const TextStyle(fontSize: 18)),
                  Text(price),
                  Semantics(
                    label: 'Rating: ${rating.toStringAsFixed(1)} out of 5',
                    excludeSemantics: true,
                    child: Row(
                      children: List.generate(5, (i) => Icon(
                        i < rating ? Icons.star : Icons.star_border,
                        color: Colors.amber,
                        size: 16,
                      )),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

// Accessibility tests
void main() {
  group('ProductCard Accessibility Tests', () {
    testWidgets('has button semantics', (tester) async {
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: ProductCard(
              name: 'Test Product',
              price: '\$29.99',
              rating: 4.5,
              onTap: () {},
            ),
          ),
        ),
      );

      // Get semantics for the card
      final semantics = tester.getSemantics(find.byType(ProductCard));
      
      // Verify it's marked as a button
      expect(semantics.hasFlag(SemanticsFlag.isButton), true);
    });

    testWidgets('includes product name in semantics', (tester) async {
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: ProductCard(
              name: 'Wireless Headphones',
              price: '\$99.99',
              rating: 4.0,
              onTap: () {},
            ),
          ),
        ),
      );

      // Find by semantics label
      expect(
        find.bySemanticsLabel(RegExp('Wireless Headphones')),
        findsOneWidget,
      );
    });

    testWidgets('announces rating correctly', (tester) async {
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: ProductCard(
              name: 'Test',
              price: '\$10',
              rating: 3.5,
              onTap: () {},
            ),
          ),
        ),
      );

      // Find rating semantics
      expect(
        find.bySemanticsLabel('Rating: 3.5 out of 5'),
        findsOneWidget,
      );
    });

    testWidgets('meets minimum touch target size', (tester) async {
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: Center(
              child: ProductCard(
                name: 'Test',
                price: '\$10',
                rating: 4.0,
                onTap: () {},
              ),
            ),
          ),
        ),
      );

      // Get the size of the card
      final cardFinder = find.byType(ProductCard);
      final cardSize = tester.getSize(cardFinder);
      
      // Minimum touch target is 48x48
      expect(cardSize.width, greaterThanOrEqualTo(48));
      expect(cardSize.height, greaterThanOrEqualTo(48));
    });
  });

  group('Form Accessibility Tests', () {
    testWidgets('text field has associated label', (tester) async {
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: Padding(
              padding: const EdgeInsets.all(16),
              child: TextFormField(
                decoration: const InputDecoration(
                  labelText: 'Email Address',
                  hintText: 'you@example.com',
                ),
              ),
            ),
          ),
        ),
      );

      // Verify label is accessible
      expect(
        find.bySemanticsLabel(RegExp('Email Address')),
        findsOneWidget,
      );
    });

    testWidgets('error state is announced', (tester) async {
      await tester.pumpWidget(
        MaterialApp(
          home: Scaffold(
            body: Padding(
              padding: const EdgeInsets.all(16),
              child: TextFormField(
                decoration: const InputDecoration(
                  labelText: 'Email',
                  errorText: 'Please enter a valid email',
                ),
              ),
            ),
          ),
        ),
      );

      // Error should be in semantics
      expect(
        find.bySemanticsLabel(RegExp('Please enter a valid email')),
        findsOneWidget,
      );
    });
  });
}

/// Helper class for accessibility assertions
class AccessibilityChecker {
  /// Check if contrast ratio meets WCAG AA
  static bool meetsContrastAA(Color foreground, Color background) {
    final ratio = _contrastRatio(foreground, background);
    return ratio >= 4.5; // WCAG AA for normal text
  }

  /// Check if contrast ratio meets WCAG AAA
  static bool meetsContrastAAA(Color foreground, Color background) {
    final ratio = _contrastRatio(foreground, background);
    return ratio >= 7.0; // WCAG AAA for normal text
  }

  static double _contrastRatio(Color foreground, Color background) {
    final fgLuminance = foreground.computeLuminance();
    final bgLuminance = background.computeLuminance();
    final lighter = fgLuminance > bgLuminance ? fgLuminance : bgLuminance;
    final darker = fgLuminance > bgLuminance ? bgLuminance : fgLuminance;
    return (lighter + 0.05) / (darker + 0.05);
  }
}

// Usage in tests:
void contrastTests() {
  test('text color meets contrast requirements', () {
    const textColor = Color(0xFF1A1A1A);
    const backgroundColor = Colors.white;
    
    expect(
      AccessibilityChecker.meetsContrastAA(textColor, backgroundColor),
      true,
    );
  });
}
```
