---
type: "EXAMPLE"
title: "Writing a Golden Test"
---




```dart
import 'package:flutter_test/flutter_test.dart';

void main() {
  testWidgets('ProductCard matches golden', (tester) async {
    await tester.pumpWidget(
      MaterialApp(
        home: Scaffold(
          body: ProductCard(
            product: Product(
              name: 'Widget Pro',
              price: 29.99,
              imageUrl: 'assets/product.png',
            ),
          ),
        ),
      ),
    );

    await expectLater(
      find.byType(ProductCard),
      matchesGoldenFile('goldens/product_card.png'),
    );
  });
}
```
