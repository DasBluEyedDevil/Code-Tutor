---
type: "EXAMPLE"
title: "Return Types and Automatic Serialization"
---

Serverpod automatically handles serialization for all supported types.


```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ProductEndpoint extends Endpoint {

  // Return a single model - automatically serialized to JSON
  Future<Product> getProduct(Session session, int id) async {
    final product = await Product.db.findById(session, id);
    if (product == null) {
      throw Exception('Product not found');
    }
    return product;  // Sent as JSON to client
  }

  // Return a nullable model - null becomes JSON null
  Future<Product?> findProductByName(Session session, String name) async {
    return await Product.db.findFirstRow(
      session,
      where: (t) => t.name.equals(name),
    );  // Returns null if not found
  }

  // Return a List - automatically becomes JSON array
  Future<List<Product>> getAllProducts(Session session) async {
    return await Product.db.find(session);  // List<Product> -> JSON array
  }

  // Return primitive types
  Future<int> countProducts(Session session) async {
    return await Product.db.count(session);  // int -> JSON number
  }

  Future<bool> isInStock(Session session, int productId) async {
    final product = await Product.db.findById(session, productId);
    return product?.stockQuantity != null && product!.stockQuantity > 0;
  }

  Future<String> getProductName(Session session, int productId) async {
    final product = await Product.db.findById(session, productId);
    return product?.name ?? 'Unknown';  // String -> JSON string
  }

  Future<double> getAveragePrice(Session session) async {
    final products = await Product.db.find(session);
    if (products.isEmpty) return 0.0;

    final total = products.fold<double>(
      0.0,
      (sum, p) => sum + p.price,
    );
    return total / products.length;  // double -> JSON number
  }

  // Return complex nested structures
  Future<Map<String, dynamic>> getProductStats(Session session) async {
    final products = await Product.db.find(session);

    return {
      'totalProducts': products.length,
      'averagePrice': products.isEmpty
          ? 0.0
          : products.fold<double>(0, (s, p) => s + p.price) / products.length,
      'inStockCount': products.where((p) => p.stockQuantity > 0).length,
    };
  }

  // Void methods - no return value
  Future<void> logProductView(Session session, int productId) async {
    session.log('Product viewed: $productId');
    // Update view count, analytics, etc.
  }
}
```
