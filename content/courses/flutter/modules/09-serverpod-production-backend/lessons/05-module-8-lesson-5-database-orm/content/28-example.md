---
type: "EXAMPLE"
title: "Transaction Examples"
---

Here are practical examples of using transactions:



```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class OrderEndpoint extends Endpoint {
  
  /// Create an order with items - all or nothing
  Future<Order> createOrder(
    Session session,
    Order order,
    List<OrderItem> items,
  ) async {
    // Use transaction to ensure order and items are created together
    return await session.db.transaction((transaction) async {
      // Create the order first
      final createdOrder = await Order.db.insertRow(session, order);
      
      // Create all order items with the order ID
      for (final item in items) {
        await OrderItem.db.insertRow(
          session,
          item.copyWith(orderId: createdOrder.id),
        );
        
        // Also decrement product stock
        final product = await Product.db.findById(session, item.productId);
        if (product == null) {
          throw Exception('Product ${item.productId} not found');
        }
        if (product.stockQuantity < item.quantity) {
          throw Exception('Insufficient stock for ${product.name}');
        }
        
        await Product.db.updateRow(
          session,
          product.copyWith(
            stockQuantity: product.stockQuantity - item.quantity,
          ),
        );
      }
      
      return createdOrder;
    });
    // If ANY operation fails, the entire transaction is rolled back:
    // - Order is not created
    // - No order items are created
    // - No stock is decremented
  }
  
  /// Transfer balance between accounts
  Future<void> transferBalance(
    Session session,
    int fromAccountId,
    int toAccountId,
    double amount,
  ) async {
    if (amount <= 0) {
      throw ArgumentError('Amount must be positive');
    }
    
    await session.db.transaction((transaction) async {
      // Get both accounts
      final fromAccount = await Account.db.findById(session, fromAccountId);
      final toAccount = await Account.db.findById(session, toAccountId);
      
      if (fromAccount == null || toAccount == null) {
        throw Exception('Account not found');
      }
      
      if (fromAccount.balance < amount) {
        throw Exception('Insufficient balance');
      }
      
      // Perform the transfer
      await Account.db.updateRow(
        session,
        fromAccount.copyWith(balance: fromAccount.balance - amount),
      );
      
      await Account.db.updateRow(
        session,
        toAccount.copyWith(balance: toAccount.balance + amount),
      );
      
      // Log the transfer
      await TransferLog.db.insertRow(session, TransferLog(
        fromAccountId: fromAccountId,
        toAccountId: toAccountId,
        amount: amount,
        timestamp: DateTime.now(),
      ));
    });
    // Either all three operations succeed, or none of them happen
  }
  
  /// Delete a user and all their content
  Future<void> deleteUserWithContent(
    Session session,
    int userId,
  ) async {
    await session.db.transaction((transaction) async {
      // Delete in order to respect foreign key constraints
      // (or use CASCADE in your database schema)
      
      // Delete comments by user
      await Comment.db.deleteWhere(
        session,
        where: (t) => t.authorId.equals(userId),
      );
      
      // Delete posts by user
      await Post.db.deleteWhere(
        session,
        where: (t) => t.authorId.equals(userId),
      );
      
      // Delete user profile
      await UserProfile.db.deleteWhere(
        session,
        where: (t) => t.userId.equals(userId),
      );
      
      // Finally delete the user
      await User.db.deleteWhere(
        session,
        where: (t) => t.id.equals(userId),
      );
    });
  }
}
```
