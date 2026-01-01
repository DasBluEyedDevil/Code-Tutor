import 'package:firebase_analytics/firebase_analytics.dart';

class CheckoutFunnelAnalytics {
  final FirebaseAnalytics _analytics = FirebaseAnalytics.instance;
  
  /// Step 1: User views their cart
  Future<void> viewCart({
    required int itemCount,
    required double cartTotal,
  }) async {
    await _analytics.logEvent(
      name: 'checkout_view_cart',
      parameters: {
        'funnel_step': 1,
        'item_count': itemCount,
        'cart_total': cartTotal,
        'currency': 'USD',
      },
    );
  }
  
  /// Step 2: User begins checkout
  Future<void> beginCheckout({
    required double cartTotal,
    String? couponCode,
  }) async {
    await _analytics.logEvent(
      name: 'checkout_begin',
      parameters: {
        'funnel_step': 2,
        'cart_total': cartTotal,
        'currency': 'USD',
        if (couponCode != null) 'coupon_code': couponCode,
      },
    );
  }
  
  /// Step 3: User adds payment information
  Future<void> addPaymentInfo({
    required String paymentType,
  }) async {
    await _analytics.logEvent(
      name: 'checkout_add_payment',
      parameters: {
        'funnel_step': 3,
        'payment_type': paymentType,
      },
    );
  }
  
  /// Step 4: User completes purchase
  Future<void> completePurchase({
    required String orderId,
    required double orderTotal,
    required int itemCount,
  }) async {
    await _analytics.logEvent(
      name: 'checkout_purchase',
      parameters: {
        'funnel_step': 4,
        'order_id': orderId,
        'order_total': orderTotal,
        'item_count': itemCount,
        'currency': 'USD',
      },
    );
  }
  
  /// Calculate dropoff percentages between funnel steps
  /// Returns map of step transitions to dropoff percentage
  Map<String, double> calculateFunnelDropoff(Map<int, int> stepCounts) {
    final dropoffs = <String, double>{};
    
    // Calculate dropoff for each transition
    for (int step = 1; step < 4; step++) {
      final currentCount = stepCounts[step] ?? 0;
      final nextCount = stepCounts[step + 1] ?? 0;
      
      if (currentCount > 0) {
        final dropoffPercent = ((currentCount - nextCount) / currentCount) * 100;
        dropoffs['step_${step}_to_${step + 1}'] = 
            double.parse(dropoffPercent.toStringAsFixed(1));
      }
    }
    
    // Also calculate overall conversion rate
    final firstStep = stepCounts[1] ?? 0;
    final lastStep = stepCounts[4] ?? 0;
    if (firstStep > 0) {
      final conversionRate = (lastStep / firstStep) * 100;
      dropoffs['overall_conversion'] = 
          double.parse(conversionRate.toStringAsFixed(1));
    }
    
    return dropoffs;
  }
}

void main() {
  print('CheckoutFunnelAnalytics created');
  
  // Test dropoff calculation
  final analytics = CheckoutFunnelAnalytics();
  final stepCounts = {
    1: 1000,  // Viewed cart
    2: 600,   // Started checkout
    3: 400,   // Added payment
    4: 350,   // Completed purchase
  };
  
  final dropoffs = analytics.calculateFunnelDropoff(stepCounts);
  print('Dropoffs: $dropoffs');
  // Expected: {step_1_to_2: 40.0, step_2_to_3: 33.3, step_3_to_4: 12.5, overall_conversion: 35.0}
}