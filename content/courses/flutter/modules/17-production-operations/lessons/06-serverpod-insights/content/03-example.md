---
type: "EXAMPLE"
title: "Advanced Logging Patterns"
---


Implement custom logging utilities for consistent, searchable logs:



```dart
// lib/src/util/app_logger.dart
import 'package:serverpod/serverpod.dart';

/// Structured logging utility for consistent log formatting
class AppLogger {
  final Session session;
  final String component;
  
  AppLogger(this.session, this.component);
  
  /// Log with automatic component tagging
  void debug(String message, {Map<String, dynamic>? data}) {
    _log(LogLevel.debug, message, data: data);
  }
  
  void info(String message, {Map<String, dynamic>? data}) {
    _log(LogLevel.info, message, data: data);
  }
  
  void warning(String message, {Map<String, dynamic>? data}) {
    _log(LogLevel.warning, message, data: data);
  }
  
  void error(
    String message, {
    Object? exception,
    StackTrace? stackTrace,
    Map<String, dynamic>? data,
  }) {
    _log(
      LogLevel.error,
      message,
      exception: exception,
      stackTrace: stackTrace,
      data: data,
    );
  }
  
  void _log(
    LogLevel level,
    String message, {
    Object? exception,
    StackTrace? stackTrace,
    Map<String, dynamic>? data,
  }) {
    // Format structured data as string
    final dataStr = data != null ? ' | ${_formatData(data)}' : '';
    final formattedMessage = '[$component] $message$dataStr';
    
    session.log(
      formattedMessage,
      level: level,
      exception: exception,
      stackTrace: stackTrace,
    );
  }
  
  String _formatData(Map<String, dynamic> data) {
    return data.entries
        .map((e) => '${e.key}=${e.value}')
        .join(', ');
  }
}

/// Request timing logger
class TimingLogger {
  final Session session;
  final String operation;
  final Stopwatch _stopwatch;
  
  TimingLogger(this.session, this.operation)
      : _stopwatch = Stopwatch()..start();
  
  /// Log checkpoint during operation
  void checkpoint(String label) {
    session.log(
      'TIMING [$operation] $label: ${_stopwatch.elapsedMilliseconds}ms',
      level: LogLevel.debug,
    );
  }
  
  /// Complete timing and log total duration
  void complete({bool success = true}) {
    _stopwatch.stop();
    final status = success ? 'completed' : 'failed';
    session.log(
      'TIMING [$operation] $status in ${_stopwatch.elapsedMilliseconds}ms',
      level: success ? LogLevel.info : LogLevel.warning,
    );
  }
}

// Usage in endpoints:
class OrderEndpoint extends Endpoint {
  Future<Order> createOrder(
    Session session,
    List<OrderItem> items,
  ) async {
    final logger = AppLogger(session, 'OrderEndpoint');
    final timing = TimingLogger(session, 'createOrder');
    
    logger.info('Creating order', data: {
      'itemCount': items.length,
      'userId': session.auth?.userId,
    });
    
    try {
      // Validate items
      timing.checkpoint('validation_start');
      await _validateItems(session, items);
      timing.checkpoint('validation_complete');
      
      // Calculate totals
      final total = items.fold<double>(
        0,
        (sum, item) => sum + (item.price * item.quantity),
      );
      
      logger.debug('Order total calculated', data: {
        'total': total,
        'itemCount': items.length,
      });
      
      // Create order in database
      timing.checkpoint('db_insert_start');
      final order = await Order.db.insertRow(
        session,
        Order(
          userId: session.auth!.userId!,
          total: total,
          status: OrderStatus.pending,
          createdAt: DateTime.now(),
        ),
      );
      timing.checkpoint('db_insert_complete');
      
      // Insert order items
      for (final item in items) {
        await OrderItem.db.insertRow(
          session,
          item.copyWith(orderId: order.id),
        );
      }
      
      timing.complete(success: true);
      
      logger.info('Order created successfully', data: {
        'orderId': order.id,
        'total': total,
      });
      
      return order;
    } catch (e, stackTrace) {
      timing.complete(success: false);
      
      logger.error(
        'Failed to create order',
        exception: e,
        stackTrace: stackTrace,
        data: {'itemCount': items.length},
      );
      
      rethrow;
    }
  }
  
  Future<void> _validateItems(Session session, List<OrderItem> items) async {
    // Validation logic
  }
}
```
