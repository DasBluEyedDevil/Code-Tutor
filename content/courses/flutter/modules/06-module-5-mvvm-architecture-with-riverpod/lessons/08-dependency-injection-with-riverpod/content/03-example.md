---
type: "EXAMPLE"
title: "Complete Dependency Injection Example"
---

Let us build a complete example showing proper dependency injection with Riverpod. We will create a layered architecture where each layer depends on the one below it:

**Layer 1: ApiClient** - Handles raw HTTP communication
**Layer 2: ProductRepository** - Handles product data operations
**Layer 3: ProductViewModel** - Handles UI state and business logic
**Layer 4: ProductScreen** - The UI widget

Each layer receives its dependencies through Riverpod providers, not by creating them directly. This makes every layer testable in isolation.

```dart
// =====================================================
// COMPLETE DEPENDENCY INJECTION EXAMPLE
// =====================================================

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:riverpod_annotation/riverpod_annotation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

part 'product_feature.g.dart';

// =====================================================
// LAYER 1: API CLIENT (lowest level)
// =====================================================

class ApiClient {
  final String baseUrl;
  final http.Client httpClient;

  ApiClient({required this.baseUrl, http.Client? client})
      : httpClient = client ?? http.Client();

  Future<Map<String, dynamic>> get(String endpoint) async {
    final response = await httpClient.get(Uri.parse('$baseUrl$endpoint'));
    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }
    throw Exception('API Error: ${response.statusCode}');
  }

  Future<Map<String, dynamic>> post(String endpoint, Map<String, dynamic> body) async {
    final response = await httpClient.post(
      Uri.parse('$baseUrl$endpoint'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(body),
    );
    if (response.statusCode == 200 || response.statusCode == 201) {
      return jsonDecode(response.body);
    }
    throw Exception('API Error: ${response.statusCode}');
  }
}

// Provider for ApiClient
@Riverpod(keepAlive: true)  // Keep alive - used throughout app
ApiClient apiClient(Ref ref) {
  return ApiClient(baseUrl: 'https://api.example.com');
}

// =====================================================
// LAYER 2: REPOSITORY (data access)
// =====================================================

class Product {
  final String id;
  final String name;
  final double price;
  final String imageUrl;

  Product({
    required this.id,
    required this.name,
    required this.price,
    required this.imageUrl,
  });

  factory Product.fromJson(Map<String, dynamic> json) {
    return Product(
      id: json['id'].toString(),
      name: json['name'],
      price: (json['price'] as num).toDouble(),
      imageUrl: json['image_url'] ?? '',
    );
  }
}

abstract class ProductRepository {
  Future<List<Product>> getProducts();
  Future<Product> getProduct(String id);
  Future<void> addToCart(String productId, int quantity);
}

class ApiProductRepository implements ProductRepository {
  final ApiClient _client;

  ApiProductRepository(this._client);  // Dependency injected!

  @override
  Future<List<Product>> getProducts() async {
    final data = await _client.get('/products');
    final items = data['products'] as List;
    return items.map((item) => Product.fromJson(item)).toList();
  }

  @override
  Future<Product> getProduct(String id) async {
    final data = await _client.get('/products/$id');
    return Product.fromJson(data);
  }

  @override
  Future<void> addToCart(String productId, int quantity) async {
    await _client.post('/cart', {
      'product_id': productId,
      'quantity': quantity,
    });
  }
}

// Provider for ProductRepository - INJECTS ApiClient
@riverpod
ProductRepository productRepository(Ref ref) {
  final apiClient = ref.watch(apiClientProvider);  // DI!
  return ApiProductRepository(apiClient);
}

// =====================================================
// LAYER 3: VIEWMODEL (business logic)
// =====================================================

@riverpod
class ProductList extends _$ProductList {
  @override
  Future<List<Product>> build() async {
    // DEPENDENCY INJECTION: Get repository from Riverpod
    final repository = ref.watch(productRepositoryProvider);
    return await repository.getProducts();
  }

  Future<void> refresh() async {
    state = const AsyncLoading();
    final repository = ref.read(productRepositoryProvider);
    state = await AsyncValue.guard(() => repository.getProducts());
  }

  Future<void> addToCart(String productId) async {
    final repository = ref.read(productRepositoryProvider);
    await repository.addToCart(productId, 1);
    // Could show a snackbar or update cart count here
  }
}

// Single product detail (with parameter)
@riverpod
class ProductDetail extends _$ProductDetail {
  @override
  Future<Product> build(String productId) async {
    final repository = ref.watch(productRepositoryProvider);
    return await repository.getProduct(productId);
  }
}

// =====================================================
// LAYER 4: VIEW (UI)
// =====================================================

class ProductListScreen extends ConsumerWidget {
  const ProductListScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Widget has NO knowledge of repositories or API clients!
    // It just asks for the ViewModel.
    final productsAsync = ref.watch(productListProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Products'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => ref.read(productListProvider.notifier).refresh(),
          ),
        ],
      ),
      body: productsAsync.when(
        loading: () => const Center(child: CircularProgressIndicator()),
        error: (error, _) => Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text('Error: $error'),
              ElevatedButton(
                onPressed: () => ref.read(productListProvider.notifier).refresh(),
                child: const Text('Retry'),
              ),
            ],
          ),
        ),
        data: (products) => ListView.builder(
          itemCount: products.length,
          itemBuilder: (context, index) {
            final product = products[index];
            return ListTile(
              title: Text(product.name),
              subtitle: Text('\$${product.price.toStringAsFixed(2)}'),
              trailing: IconButton(
                icon: const Icon(Icons.add_shopping_cart),
                onPressed: () {
                  ref.read(productListProvider.notifier).addToCart(product.id);
                },
              ),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (_) => ProductDetailScreen(productId: product.id),
                  ),
                );
              },
            );
          },
        ),
      ),
    );
  }
}

class ProductDetailScreen extends ConsumerWidget {
  final String productId;

  const ProductDetailScreen({super.key, required this.productId});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final productAsync = ref.watch(productDetailProvider(productId));

    return Scaffold(
      appBar: AppBar(title: const Text('Product Detail')),
      body: productAsync.when(
        loading: () => const Center(child: CircularProgressIndicator()),
        error: (e, _) => Center(child: Text('Error: $e')),
        data: (product) => Padding(
          padding: const EdgeInsets.all(16),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(product.name, style: Theme.of(context).textTheme.headlineMedium),
              const SizedBox(height: 8),
              Text('\$${product.price.toStringAsFixed(2)}',
                  style: Theme.of(context).textTheme.titleLarge),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: () {
                  ref.read(productListProvider.notifier).addToCart(product.id);
                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(content: Text('Added to cart!')),
                  );
                },
                child: const Text('Add to Cart'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

// =====================================================
// THE DEPENDENCY CHAIN IS CLEAR AND TESTABLE
// =====================================================
//
// ProductListScreen / ProductDetailScreen
//           |
//           | ref.watch(productListProvider / productDetailProvider)
//           v
// ProductList / ProductDetail (ViewModels)
//           |
//           | ref.watch(productRepositoryProvider)
//           v
//    ProductRepository
//           |
//           | ref.watch(apiClientProvider)
//           v
//       ApiClient
//
// Each layer can be tested independently by overriding providers!
```
