import 'package:serverpod/serverpod.dart';
import 'package:uuid/uuid.dart';

enum Environment { development, staging, production }

class RequestLogger {
  final Session session;
  final String requestId;
  final DateTime startTime;
  final Environment environment;
  
  RequestLogger._(
    this.session,
    this.requestId,
    this.startTime,
    this.environment,
  );
  
  /// Create a new request logger
  factory RequestLogger(Session session, {Environment? env}) {
    return RequestLogger._(
      session,
      const Uuid().v4().substring(0, 8), // Short request ID
      DateTime.now(),
      env ?? Environment.production,
    );
  }
  
  /// Log the start of a request
  void logRequestStart() {
    final userId = session.auth?.userId ?? 'anonymous';
    final endpoint = session.endpoint ?? 'unknown';
    final method = session.method ?? 'unknown';
    
    _log(
      'REQUEST_START [$requestId] $endpoint.$method userId=$userId',
      LogLevel.info,
    );
  }
  
  /// Log the end of a request
  void logRequestEnd({bool success = true, Object? error}) {
    final duration = DateTime.now().difference(startTime);
    final status = success ? 'SUCCESS' : 'FAILURE';
    
    if (success) {
      _log(
        'REQUEST_END [$requestId] $status ${duration.inMilliseconds}ms',
        LogLevel.info,
      );
    } else {
      _log(
        'REQUEST_END [$requestId] $status ${duration.inMilliseconds}ms error=$error',
        LogLevel.error,
      );
    }
  }
  
  /// Wrap an async operation with logging
  Future<T> wrap<T>(Future<T> Function() operation) async {
    logRequestStart();
    
    try {
      final result = await operation();
      logRequestEnd(success: true);
      return result;
    } catch (e, stackTrace) {
      logRequestEnd(success: false, error: e);
      
      // Log full error details
      session.log(
        'REQUEST_ERROR [$requestId] ${e.toString()}',
        level: LogLevel.error,
        exception: e,
        stackTrace: stackTrace,
      );
      
      rethrow;
    }
  }
  
  /// Get the minimum log level for current environment
  LogLevel get _minLogLevel {
    switch (environment) {
      case Environment.development:
        return LogLevel.debug;
      case Environment.staging:
        return LogLevel.info;
      case Environment.production:
        return LogLevel.warning;
    }
  }
  
  /// Log only if level meets minimum
  void _log(String message, LogLevel level) {
    // Only log if level is at or above minimum
    // LogLevel enum: debug < info < warning < error < fatal
    if (_shouldLog(level)) {
      session.log(message, level: level);
    }
  }
  
  bool _shouldLog(LogLevel level) {
    const levelOrder = {
      LogLevel.debug: 0,
      LogLevel.info: 1,
      LogLevel.warning: 2,
      LogLevel.error: 3,
      LogLevel.fatal: 4,
    };
    
    return levelOrder[level]! >= levelOrder[_minLogLevel]!;
  }
}

// Example usage in an endpoint
class ExampleEndpoint extends Endpoint {
  Future<String> doSomething(Session session) async {
    final logger = RequestLogger(session, env: Environment.production);
    
    return logger.wrap(() async {
      // Simulated work
      await Future.delayed(Duration(milliseconds: 100));
      return 'Success!';
    });
  }
  
  Future<String> doSomethingRisky(Session session) async {
    final logger = RequestLogger(session, env: Environment.development);
    
    return logger.wrap(() async {
      // This will be logged with full error details
      throw Exception('Something went wrong!');
    });
  }
}