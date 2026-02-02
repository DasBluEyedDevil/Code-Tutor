---
type: "EXAMPLE"
title: "Debugging Production Issues"
---


Implement comprehensive debugging utilities for production troubleshooting:



```dart
// lib/src/util/session_inspector.dart
import 'package:serverpod/serverpod.dart';

/// Session inspection and debugging utilities
class SessionInspector {
  final Session session;
  
  SessionInspector(this.session);
  
  /// Log detailed session information for debugging
  void logSessionDetails() {
    session.log(
      'SESSION_INFO: '
      'endpoint=${session.endpoint ?? "unknown"}, '
      'method=${session.method ?? "unknown"}, '
      'userId=${session.auth?.userId ?? "anonymous"}',
      level: LogLevel.debug,
    );
  }
  
  /// Create a debug snapshot of current request
  Map<String, dynamic> createDebugSnapshot() {
    return {
      'timestamp': DateTime.now().toIso8601String(),
      'endpoint': session.endpoint,
      'method': session.method,
      'authenticated': session.auth != null,
      'userId': session.auth?.userId,
    };
  }
}

// lib/src/util/error_reporter.dart
import 'package:serverpod/serverpod.dart';

/// Centralized error reporting
class ErrorReporter {
  /// Report an error with full context
  static void report(
    Session session,
    Object error,
    StackTrace stackTrace, {
    String? context,
    Map<String, dynamic>? metadata,
  }) {
    // Log to Serverpod Insights
    session.log(
      'ERROR_REPORT: ${context ?? error.toString()}',
      level: LogLevel.error,
      exception: error,
      stackTrace: stackTrace,
    );
    
    // In production, also send to external service:
    // await _sendToSentry(error, stackTrace, metadata);
    // await _sendToSlack(error, context);
  }
  
  /// Report a handled error (non-critical)
  static void reportHandled(
    Session session,
    Object error, {
    String? context,
  }) {
    session.log(
      'HANDLED_ERROR: ${context ?? error.toString()}',
      level: LogLevel.warning,
    );
  }
}

// lib/src/util/request_tracer.dart
import 'package:serverpod/serverpod.dart';
import 'package:uuid/uuid.dart';

/// Distributed tracing for requests
class RequestTracer {
  final Session session;
  final String traceId;
  final String spanId;
  final List<TraceSpan> _spans = [];
  
  RequestTracer(this.session)
      : traceId = const Uuid().v4(),
        spanId = const Uuid().v4().substring(0, 8);
  
  /// Start a new span for an operation
  TraceSpan startSpan(String operationName) {
    final span = TraceSpan(
      traceId: traceId,
      spanId: const Uuid().v4().substring(0, 8),
      parentSpanId: spanId,
      operationName: operationName,
      startTime: DateTime.now(),
    );
    _spans.add(span);
    
    session.log(
      'TRACE [$traceId] START $operationName',
      level: LogLevel.debug,
    );
    
    return span;
  }
  
  /// End a span
  void endSpan(TraceSpan span, {bool success = true}) {
    span.endTime = DateTime.now();
    span.success = success;
    
    final duration = span.endTime!.difference(span.startTime);
    final status = success ? 'OK' : 'ERROR';
    
    session.log(
      'TRACE [$traceId] END ${span.operationName} '
      '${duration.inMilliseconds}ms $status',
      level: success ? LogLevel.debug : LogLevel.warning,
    );
  }
  
  /// Get trace summary
  Map<String, dynamic> getSummary() {
    return {
      'traceId': traceId,
      'spanCount': _spans.length,
      'spans': _spans.map((s) => s.toJson()).toList(),
    };
  }
}

class TraceSpan {
  final String traceId;
  final String spanId;
  final String? parentSpanId;
  final String operationName;
  final DateTime startTime;
  DateTime? endTime;
  bool success = true;
  
  TraceSpan({
    required this.traceId,
    required this.spanId,
    this.parentSpanId,
    required this.operationName,
    required this.startTime,
  });
  
  int get durationMs => endTime != null
      ? endTime!.difference(startTime).inMilliseconds
      : 0;
  
  Map<String, dynamic> toJson() => {
    'spanId': spanId,
    'operation': operationName,
    'durationMs': durationMs,
    'success': success,
  };
}

// Usage in endpoints:
class PaymentEndpoint extends Endpoint {
  Future<PaymentResult> processPayment(
    Session session,
    PaymentRequest request,
  ) async {
    final tracer = RequestTracer(session);
    final inspector = SessionInspector(session);
    
    inspector.logSessionDetails();
    
    try {
      // Trace validation
      final validateSpan = tracer.startSpan('validate_payment');
      await _validatePayment(session, request);
      tracer.endSpan(validateSpan);
      
      // Trace external API call
      final apiSpan = tracer.startSpan('call_payment_gateway');
      final gatewayResponse = await _callPaymentGateway(request);
      tracer.endSpan(apiSpan);
      
      // Trace database update
      final dbSpan = tracer.startSpan('update_database');
      final result = await _recordPayment(session, request, gatewayResponse);
      tracer.endSpan(dbSpan);
      
      session.log(
        'Payment processed: ${tracer.getSummary()}',
        level: LogLevel.info,
      );
      
      return result;
    } catch (e, stackTrace) {
      ErrorReporter.report(
        session,
        e,
        stackTrace,
        context: 'Payment processing failed',
        metadata: {
          'traceId': tracer.traceId,
          'amount': request.amount,
        },
      );
      rethrow;
    }
  }
  
  Future<void> _validatePayment(Session s, PaymentRequest r) async {}
  Future<dynamic> _callPaymentGateway(PaymentRequest r) async => {};
  Future<PaymentResult> _recordPayment(
    Session s,
    PaymentRequest r,
    dynamic resp,
  ) async => PaymentResult();
}
```
