---
type: "EXAMPLE"
title: "Tracking E-Commerce Events"
---


Firebase has predefined e-commerce events for better integration with Google Analytics:



```dart
// lib/services/analytics_service.dart

extension EcommerceAnalytics on AnalyticsService {
  /// Log when user views product details
  Future<void> logViewItem({
    required String itemId,
    required String itemName,
    required String itemCategory,
    required double price,
    String currency = 'USD',
  }) async {
    await logEvent(
      name: 'view_item',
      parameters: {
        'currency': currency,
        'value': price,
        'items': [
          {
            'item_id': itemId,
            'item_name': itemName,
            'item_category': itemCategory,
            'price': price,
          }
        ].toString(),
      },
    );
  }
  
  /// Log when user adds item to cart
  Future<void> logAddToCart({
    required String itemId,
    required String itemName,
    required double price,
    int quantity = 1,
  }) async {
    await logEvent(
      name: 'add_to_cart',
      parameters: {
        'currency': 'USD',
        'value': price * quantity,
        'item_id': itemId,
        'item_name': itemName,
        'price': price,
        'quantity': quantity,
      },
    );
  }
  
  /// Log when user starts checkout
  Future<void> logBeginCheckout({
    required double cartTotal,
    required int itemCount,
    String? couponCode,
  }) async {
    await logEvent(
      name: 'begin_checkout',
      parameters: {
        'currency': 'USD',
        'value': cartTotal,
        'item_count': itemCount,
        if (couponCode != null) 'coupon': couponCode,
      },
    );
  }
  
  /// Log completed purchase
  Future<void> logPurchase({
    required String transactionId,
    required double total,
    required double shipping,
    required double tax,
    String? couponCode,
  }) async {
    await logEvent(
      name: 'purchase',
      parameters: {
        'transaction_id': transactionId,
        'currency': 'USD',
        'value': total,
        'shipping': shipping,
        'tax': tax,
        if (couponCode != null) 'coupon': couponCode,
      },
    );
  }
}

// Usage in a product screen:
class ProductScreen extends StatelessWidget {
  final Product product;
  final AnalyticsService _analytics = AnalyticsService();
  
  ProductScreen({super.key, required this.product}) {
    // Log product view when screen opens
    _analytics.logViewItem(
      itemId: product.id,
      itemName: product.name,
      itemCategory: product.category,
      price: product.price,
    );
  }
  
  void _addToCart() {
    _analytics.logAddToCart(
      itemId: product.id,
      itemName: product.name,
      price: product.price,
    );
    // ... add to cart logic
  }
}
```
