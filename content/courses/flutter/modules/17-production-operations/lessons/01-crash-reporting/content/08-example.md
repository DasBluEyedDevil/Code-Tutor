---
type: "EXAMPLE"
title: "Adding Breadcrumbs for User Journey"
---


Breadcrumbs are a trail of events leading up to a crash, helping you understand what the user was doing:



```dart
// lib/core/error_reporting.dart

class ErrorReporting {
  /// Log a breadcrumb for debugging crashes
  static void logBreadcrumb(
    String message, {
    String? category,
    Map<String, dynamic>? data,
  }) {
    // Firebase Crashlytics
    FirebaseCrashlytics.instance.log(
      '[$category] $message ${data != null ? '- $data' : ''}',
    );
    
    // Sentry (more structured)
    // Sentry.addBreadcrumb(Breadcrumb(
    //   message: message,
    //   category: category,
    //   data: data,
    //   level: SentryLevel.info,
    // ));
  }
}

// Usage throughout your app:

// Navigation breadcrumbs
class AppNavigatorObserver extends NavigatorObserver {
  @override
  void didPush(Route<dynamic> route, Route<dynamic>? previousRoute) {
    ErrorReporting.logBreadcrumb(
      'Navigated to ${route.settings.name}',
      category: 'navigation',
      data: {'from': previousRoute?.settings.name},
    );
  }
}

// User action breadcrumbs
class ProductScreen extends StatelessWidget {
  void _addToCart(Product product) {
    ErrorReporting.logBreadcrumb(
      'Added product to cart',
      category: 'user_action',
      data: {
        'product_id': product.id,
        'product_name': product.name,
        'price': product.price,
      },
    );
    
    // ... add to cart logic ...
  }
}

// API call breadcrumbs
class ApiClient {
  Future<Response> get(String path) async {
    ErrorReporting.logBreadcrumb(
      'API Request: GET $path',
      category: 'http',
    );
    
    try {
      final response = await _dio.get(path);
      ErrorReporting.logBreadcrumb(
        'API Response: ${response.statusCode}',
        category: 'http',
        data: {'path': path},
      );
      return response;
    } catch (e) {
      ErrorReporting.logBreadcrumb(
        'API Error: $e',
        category: 'http',
        data: {'path': path},
      );
      rethrow;
    }
  }
}
```
