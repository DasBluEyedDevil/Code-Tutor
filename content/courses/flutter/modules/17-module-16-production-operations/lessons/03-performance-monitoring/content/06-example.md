---
type: "EXAMPLE"
title: "Measuring Critical User Flows"
---


Track the performance of important user journeys:



```dart
// lib/services/performance_service.dart

extension UserFlowTraces on PerformanceService {
  /// Measure the complete login flow
  Future<T> measureLogin<T>({
    required Future<T> Function() loginOperation,
    required String loginMethod,
  }) async {
    return measureAsync(
      'user_login',
      loginOperation,
      attributes: {
        'login_method': loginMethod,
      },
    );
  }
  
  /// Measure the checkout process
  Future<T> measureCheckout<T>({
    required Future<T> Function() checkoutOperation,
    required int itemCount,
    required String paymentMethod,
  }) async {
    final trace = FirebasePerformance.instance.newTrace('checkout_flow');
    trace.putAttribute('payment_method', paymentMethod);
    trace.incrementMetric('item_count', itemCount);
    
    await trace.start();
    try {
      return await checkoutOperation();
    } finally {
      await trace.stop();
    }
  }
  
  /// Measure data synchronization
  Future<void> measureDataSync({
    required Future<int> Function() syncOperation,
    required String syncType,
  }) async {
    final trace = FirebasePerformance.instance.newTrace('data_sync');
    trace.putAttribute('sync_type', syncType);
    
    await trace.start();
    try {
      final itemsSynced = await syncOperation();
      trace.incrementMetric('items_synced', itemsSynced);
    } finally {
      await trace.stop();
    }
  }
}

// Usage in authentication service:
class AuthService {
  final PerformanceService _performance = PerformanceService();
  
  Future<User> signInWithEmail(String email, String password) async {
    return _performance.measureLogin(
      loginMethod: 'email',
      loginOperation: () async {
        // Actual login implementation
        final credential = await FirebaseAuth.instance
            .signInWithEmailAndPassword(
              email: email,
              password: password,
            );
        return credential.user!;
      },
    );
  }
  
  Future<User> signInWithGoogle() async {
    return _performance.measureLogin(
      loginMethod: 'google',
      loginOperation: () async {
        // Google sign-in implementation
        final googleUser = await GoogleSignIn().signIn();
        final googleAuth = await googleUser!.authentication;
        final credential = GoogleAuthProvider.credential(
          accessToken: googleAuth.accessToken,
          idToken: googleAuth.idToken,
        );
        final userCredential = await FirebaseAuth.instance
            .signInWithCredential(credential);
        return userCredential.user!;
      },
    );
  }
}
```
