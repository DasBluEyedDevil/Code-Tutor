---
type: "THEORY"
title: "Overriding Providers for Testing"
---

One of the biggest benefits of dependency injection is testability. Riverpod makes this exceptionally easy with **provider overrides**.

### The Testing Problem

Without DI, testing a ViewModel means testing everything it depends on:
- ViewModel test accidentally tests the Repository
- Repository test accidentally tests the ApiClient
- ApiClient test requires real network calls

Your "unit test" becomes an integration test, and it is slow, flaky, and hard to debug.

### The Solution: Override Providers

Riverpod's `ProviderScope` accepts an `overrides` parameter. You can replace any provider with a different implementation:

```dart
ProviderScope(
  overrides: [
    // Replace real repository with mock
    productRepositoryProvider.overrideWith((ref) => MockProductRepository()),
  ],
  child: MyApp(),
)
```

### How Overrides Work

When you override a provider:
1. Any code watching that provider gets the overridden value
2. Other providers that depend on it also get the overridden value
3. The override cascades through the dependency chain

For example, if you override `apiClientProvider` with a mock, then `productRepositoryProvider` (which depends on it) automatically uses the mock API client.

### Testing Patterns

**Pattern 1: Override at the lowest level**
Override the API client to return mock data:
```dart
apiClientProvider.overrideWith((ref) => MockApiClient())
```

**Pattern 2: Override at the repository level**
Override the repository directly:
```dart
productRepositoryProvider.overrideWith((ref) => MockProductRepository())
```

**Pattern 3: Override with specific values**
For Notifiers, you can override the initial state:
```dart
productListProvider.overrideWith(() => MockProductList())
```

### Multiple Environments

Overrides are not just for testing. Use them for different environments:

```dart
// Development
ProviderScope(
  overrides: [
    apiClientProvider.overrideWith((ref) => ApiClient(baseUrl: 'http://localhost:8080')),
  ],
  child: MyApp(),
)

// Staging
ProviderScope(
  overrides: [
    apiClientProvider.overrideWith((ref) => ApiClient(baseUrl: 'https://staging.example.com')),
  ],
  child: MyApp(),
)

// Production (no overrides - uses default)
ProviderScope(
  child: MyApp(),
)
```

```dart
// =====================================================
// TESTING WITH PROVIDER OVERRIDES
// =====================================================

import 'package:flutter_test/flutter_test.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

// Mock repository for testing
class MockProductRepository implements ProductRepository {
  final List<Product> _products;
  final bool shouldFail;

  MockProductRepository({
    List<Product>? products,
    this.shouldFail = false,
  }) : _products = products ?? [
         Product(id: '1', name: 'Test Product 1', price: 9.99, imageUrl: ''),
         Product(id: '2', name: 'Test Product 2', price: 19.99, imageUrl: ''),
       ];

  @override
  Future<List<Product>> getProducts() async {
    // Simulate network delay
    await Future.delayed(Duration(milliseconds: 10));

    if (shouldFail) {
      throw Exception('Network error');
    }

    return _products;
  }

  @override
  Future<Product> getProduct(String id) async {
    await Future.delayed(Duration(milliseconds: 10));

    if (shouldFail) {
      throw Exception('Network error');
    }

    return _products.firstWhere((p) => p.id == id);
  }

  @override
  Future<void> addToCart(String productId, int quantity) async {
    await Future.delayed(Duration(milliseconds: 10));
    if (shouldFail) {
      throw Exception('Failed to add to cart');
    }
  }
}

// =====================================================
// WIDGET TEST: Override repository
// =====================================================

void main() {
  group('ProductListScreen', () {
    testWidgets('displays list of products', (tester) async {
      await tester.pumpWidget(
        ProviderScope(
          overrides: [
            // Override with mock that returns specific products
            productRepositoryProvider.overrideWith((ref) => MockProductRepository(
              products: [
                Product(id: '1', name: 'Widget', price: 5.00, imageUrl: ''),
                Product(id: '2', name: 'Gadget', price: 10.00, imageUrl: ''),
              ],
            )),
          ],
          child: MaterialApp(home: ProductListScreen()),
        ),
      );

      // Initially shows loading
      expect(find.byType(CircularProgressIndicator), findsOneWidget);

      // Wait for async data to load
      await tester.pumpAndSettle();

      // Now shows products
      expect(find.text('Widget'), findsOneWidget);
      expect(find.text('Gadget'), findsOneWidget);
      expect(find.text('\$5.00'), findsOneWidget);
      expect(find.text('\$10.00'), findsOneWidget);
    });

    testWidgets('shows error state when fetch fails', (tester) async {
      await tester.pumpWidget(
        ProviderScope(
          overrides: [
            // Override with mock that always fails
            productRepositoryProvider.overrideWith((ref) => MockProductRepository(
              shouldFail: true,
            )),
          ],
          child: MaterialApp(home: ProductListScreen()),
        ),
      );

      // Wait for async operation
      await tester.pumpAndSettle();

      // Shows error message and retry button
      expect(find.textContaining('Error'), findsOneWidget);
      expect(find.text('Retry'), findsOneWidget);
    });

    testWidgets('retry button refreshes data', (tester) async {
      final mockRepo = MockProductRepository(shouldFail: true);

      await tester.pumpWidget(
        ProviderScope(
          overrides: [
            productRepositoryProvider.overrideWith((ref) => mockRepo),
          ],
          child: MaterialApp(home: ProductListScreen()),
        ),
      );

      await tester.pumpAndSettle();

      // Tap retry button
      await tester.tap(find.text('Retry'));
      await tester.pump();

      // Should show loading indicator during refresh
      expect(find.byType(CircularProgressIndicator), findsOneWidget);
    });
  });
}

// =====================================================
// UNIT TEST: Test ViewModel in isolation
// =====================================================

void main() {
  group('ProductList ViewModel', () {
    test('loads products on initialization', () async {
      // Create a container with mock repository
      final container = ProviderContainer(
        overrides: [
          productRepositoryProvider.overrideWith((ref) => MockProductRepository()),
        ],
      );

      // Read the provider
      final notifier = container.read(productListProvider.notifier);

      // Wait for async initialization
      await container.read(productListProvider.future);

      // Check state
      final state = container.read(productListProvider);
      expect(state.hasValue, isTrue);
      expect(state.value!.length, equals(2));

      // Clean up
      container.dispose();
    });

    test('refresh updates products', () async {
      final container = ProviderContainer(
        overrides: [
          productRepositoryProvider.overrideWith((ref) => MockProductRepository()),
        ],
      );

      // Wait for initial load
      await container.read(productListProvider.future);

      // Refresh
      await container.read(productListProvider.notifier).refresh();

      // Verify state after refresh
      final state = container.read(productListProvider);
      expect(state.hasValue, isTrue);

      container.dispose();
    });
  });
}

// =====================================================
// ENVIRONMENT-SPECIFIC CONFIGURATION
// =====================================================

enum Environment { development, staging, production }

ProviderScope buildApp(Environment env) {
  final overrides = <Override>[];

  switch (env) {
    case Environment.development:
      overrides.add(
        apiClientProvider.overrideWith((ref) => ApiClient(
          baseUrl: 'http://localhost:8080',
        )),
      );
      break;
    case Environment.staging:
      overrides.add(
        apiClientProvider.overrideWith((ref) => ApiClient(
          baseUrl: 'https://staging.example.com',
        )),
      );
      break;
    case Environment.production:
      // Use default production URL from provider
      break;
  }

  return ProviderScope(
    overrides: overrides,
    child: MyApp(),
  );
}

// main_development.dart
void main() {
  runApp(buildApp(Environment.development));
}

// main_staging.dart
void main() {
  runApp(buildApp(Environment.staging));
}

// main_production.dart
void main() {
  runApp(buildApp(Environment.production));
}
```
