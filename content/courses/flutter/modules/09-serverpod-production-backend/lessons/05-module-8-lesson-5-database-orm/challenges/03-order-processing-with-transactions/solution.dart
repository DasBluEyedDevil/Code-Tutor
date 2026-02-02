import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class OrderEndpoint extends Endpoint {
  
  /// Place an order with items - uses transaction for atomicity
  Future<Order> placeOrder(
    Session session,
    int customerId,
    List<OrderItem> items,
  ) async {
    // Validate input
    if (items.isEmpty) {
      throw ArgumentError('Order must have at least one item');
    }
    
    for (final item in items) {
      if (item.quantity <= 0) {
        throw ArgumentError('Quantity must be positive');
      }
    }
    
    // Use transaction for atomic operation
    return await session.db.transaction((transaction) async {
      // Step 1: Verify customer exists
      final customer = await Customer.db.findById(session, customerId);
      if (customer == null) {
        throw Exception('Customer not found');
      }
      
      // Step 2: Validate all items and calculate total
      double totalAmount = 0;
      final productUpdates = <Product>[];
      final itemsToCreate = <OrderItem>[];
      
      for (final item in items) {
        // Get the product
        final product = await Product.db.findById(session, item.productId);
        if (product == null) {
          throw Exception('Product ${item.productId} not found');
        }
        
        // Check stock
        if (product.stockQuantity < item.quantity) {
          throw Exception(
            'Insufficient stock for ${product.name}. '
            'Requested: ${item.quantity}, Available: ${product.stockQuantity}'
          );
        }
        
        // Calculate item total
        final itemTotal = product.price * item.quantity;
        totalAmount += itemTotal;
        
        // Prepare updated product with decremented stock
        productUpdates.add(product.copyWith(
          stockQuantity: product.stockQuantity - item.quantity,
        ));
        
        // Prepare order item with price at time of purchase
        itemsToCreate.add(item.copyWith(
          priceAtPurchase: product.price,
        ));
      }
      
      // Step 3: Create the order
      final order = Order(
        customerId: customerId,
        totalAmount: totalAmount,
        createdAt: DateTime.now(),
      );
      final createdOrder = await Order.db.insertRow(session, order);
      
      // Step 4: Create all order items with the order ID
      for (final item in itemsToCreate) {
        await OrderItem.db.insertRow(
          session,
          item.copyWith(orderId: createdOrder.id),
        );
      }
      
      // Step 5: Update product stock quantities
      for (final product in productUpdates) {
        await Product.db.updateRow(session, product);
      }
      
      session.log(
        'Order ${createdOrder.id} placed by customer $customerId. '
        'Total: \$${totalAmount.toStringAsFixed(2)}'
      );
      
      return createdOrder;
    });
    // If any step fails, the entire transaction is rolled back:
    // - No order is created
    // - No order items are created
    // - No stock is decremented
  }
}