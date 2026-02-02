---
type: "EXAMPLE"
title: "Basic Integration Test"
---




```dart
// integration_test/app_test.dart
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:my_app/main.dart' as app;

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  group('end-to-end test', () {
    testWidgets('complete checkout flow', (tester) async {
      // Start the app
      app.main();
      await tester.pumpAndSettle();

      // Navigate to products
      await tester.tap(find.text('Products'));
      await tester.pumpAndSettle();

      // Add item to cart
      await tester.tap(find.byKey(const Key('add-to-cart-1')));
      await tester.pumpAndSettle();

      // Go to cart
      await tester.tap(find.byIcon(Icons.shopping_cart));
      await tester.pumpAndSettle();

      // Verify cart has item
      expect(find.text('Widget Pro'), findsOneWidget);
      expect(find.text('\$29.99'), findsOneWidget);

      // Proceed to checkout
      await tester.tap(find.text('Checkout'));
      await tester.pumpAndSettle();

      // Verify on checkout screen
      expect(find.text('Complete Order'), findsOneWidget);
    });
  });
}
```
