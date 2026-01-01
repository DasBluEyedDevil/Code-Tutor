---
type: "EXAMPLE"
title: "Structured Logging in Serverpod"
---


**Built-in Logging System**

Serverpod provides a comprehensive logging system through the Session object:



```dart
// lib/src/endpoints/product_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ProductEndpoint extends Endpoint {
  /// Get a product by ID with comprehensive logging
  Future<Product?> getProduct(Session session, int productId) async {
    // Log at different levels
    session.log(
      'Fetching product: $productId',
      level: LogLevel.debug,
    );
    
    try {
      final product = await Product.db.findById(session, productId);
      
      if (product == null) {
        session.log(
          'Product not found: $productId',
          level: LogLevel.warning,
        );
        return null;
      }
      
      session.log(
        'Product retrieved successfully',
        level: LogLevel.info,
      );
      
      return product;
    } catch (e, stackTrace) {
      session.log(
        'Error fetching product: $productId',
        level: LogLevel.error,
        exception: e,
        stackTrace: stackTrace,
      );
      rethrow;
    }
  }
  
  /// Create a product with structured logging
  Future<Product> createProduct(
    Session session,
    String name,
    double price,
    int categoryId,
  ) async {
    // Log with structured data using a map
    session.log(
      'Creating product',
      level: LogLevel.info,
    );
    
    // Validate inputs
    if (price < 0) {
      session.log(
        'Invalid price: $price for product: $name',
        level: LogLevel.warning,
      );
      throw ArgumentError('Price cannot be negative');
    }
    
    // Check category exists
    final category = await Category.db.findById(session, categoryId);
    if (category == null) {
      session.log(
        'Category not found: $categoryId',
        level: LogLevel.error,
      );
      throw ArgumentError('Invalid category');
    }
    
    // Create product
    final product = Product(
      name: name,
      price: price,
      categoryId: categoryId,
      createdAt: DateTime.now(),
    );
    
    final created = await Product.db.insertRow(session, product);
    
    session.log(
      'Product created: ${created.id} - $name',
      level: LogLevel.info,
    );
    
    return created;
  }
}

// Log levels available in Serverpod:
// LogLevel.debug    - Detailed debugging information
// LogLevel.info     - General operational messages
// LogLevel.warning  - Warning conditions
// LogLevel.error    - Error conditions
// LogLevel.fatal    - Critical failures
```
