---
type: "EXAMPLE"
title: "Comprehensive Error Handling Example"
---

Here's a complete example with proper error handling.


```dart
// SERVER: lib/src/endpoints/order_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class OrderEndpoint extends Endpoint {

  /// Create a new order with validation.
  Future<Order> createOrder(
    Session session,
    Order order,
  ) async {
    // Validate the order
    if (order.items.isEmpty) {
      throw ArgumentError('Order must have at least one item');
    }

    if (order.totalAmount <= 0) {
      throw ArgumentError('Order total must be positive');
    }

    // Check if customer exists
    final customer = await Customer.db.findById(
      session,
      order.customerId,
    );

    if (customer == null) {
      throw CustomerNotFoundException(
        customerId: order.customerId,
        message: 'Customer not found',
      );
    }

    // Check stock for all items
    for (final item in order.items) {
      final product = await Product.db.findById(session, item.productId);

      if (product == null) {
        throw ProductNotFoundException(
          productId: item.productId,
          message: 'Product not found',
        );
      }

      if (product.stockQuantity < item.quantity) {
        throw InsufficientStockException(
          productId: item.productId,
          requested: item.quantity,
          available: product.stockQuantity,
        );
      }
    }

    // All validation passed - create the order
    try {
      final createdOrder = await Order.db.insertRow(session, order);

      // Update stock quantities
      for (final item in order.items) {
        await Product.db.updateRow(
          session,
          Product(
            id: item.productId,
            stockQuantity: -item.quantity, // Will be added to current
          ),
        );
      }

      session.log('Order created: ${createdOrder.id}');
      return createdOrder;

    } catch (e) {
      session.log('Failed to create order: $e', level: LogLevel.error);
      throw Exception('Failed to create order. Please try again.');
    }
  }
}

// CLIENT: Using the endpoint in Flutter
class OrderService {
  final Client client;

  OrderService(this.client);

  Future<OrderResult> placeOrder(Order order) async {
    try {
      final createdOrder = await client.order.createOrder(order);
      return OrderResult.success(createdOrder);

    } on CustomerNotFoundException catch (e) {
      return OrderResult.error(
        'Customer account not found. Please log in again.',
      );

    } on ProductNotFoundException catch (e) {
      return OrderResult.error(
        'Product ${e.productId} is no longer available.',
      );

    } on InsufficientStockException catch (e) {
      return OrderResult.error(
        'Only ${e.available} items available (you requested ${e.requested}).',
      );

    } on ArgumentError catch (e) {
      return OrderResult.error('Invalid order: ${e.message}');

    } catch (e) {
      return OrderResult.error('Something went wrong. Please try again.');
    }
  }
}

// Simple result wrapper
class OrderResult {
  final Order? order;
  final String? errorMessage;
  final bool isSuccess;

  OrderResult.success(this.order)
      : errorMessage = null, isSuccess = true;

  OrderResult.error(this.errorMessage)
      : order = null, isSuccess = false;
}
```
