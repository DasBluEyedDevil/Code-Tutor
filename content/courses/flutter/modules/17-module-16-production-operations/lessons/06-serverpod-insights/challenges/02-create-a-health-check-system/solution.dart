import 'package:serverpod/serverpod.dart';

enum HealthStatus { healthy, degraded, unhealthy }

class HealthCheckResult {
  final String name;
  final bool healthy;
  final int latencyMs;
  final String? error;
  
  HealthCheckResult({
    required this.name,
    required this.healthy,
    required this.latencyMs,
    this.error,
  });
  
  Map<String, dynamic> toJson() => {
    'name': name,
    'healthy': healthy,
    'latencyMs': latencyMs,
    if (error != null) 'error': error,
  };
}

class SystemHealth {
  final HealthStatus status;
  final DateTime timestamp;
  final List<HealthCheckResult> checks;
  final int totalLatencyMs;
  
  SystemHealth({
    required this.status,
    required this.timestamp,
    required this.checks,
    required this.totalLatencyMs,
  });
  
  /// Create SystemHealth from a list of check results
  factory SystemHealth.fromChecks(List<HealthCheckResult> checks) {
    // Calculate total latency (max of parallel checks)
    final totalLatency = checks.isNotEmpty
        ? checks.map((c) => c.latencyMs).reduce((a, b) => a > b ? a : b)
        : 0;
    
    // Determine overall status
    final dbCheck = checks.where((c) => c.name == 'database').firstOrNull;
    final allHealthy = checks.every((c) => c.healthy);
    final anyHealthy = checks.any((c) => c.healthy);
    
    HealthStatus status;
    if (allHealthy) {
      status = HealthStatus.healthy;
    } else if (dbCheck != null && !dbCheck.healthy) {
      // Database is critical - if it's down, system is unhealthy
      status = HealthStatus.unhealthy;
    } else if (anyHealthy) {
      status = HealthStatus.degraded;
    } else {
      status = HealthStatus.unhealthy;
    }
    
    return SystemHealth(
      status: status,
      timestamp: DateTime.now(),
      checks: checks,
      totalLatencyMs: totalLatency,
    );
  }
  
  Map<String, dynamic> toJson() => {
    'status': status.name,
    'timestamp': timestamp.toIso8601String(),
    'totalLatencyMs': totalLatencyMs,
    'checks': checks.map((c) => c.toJson()).toList(),
  };
}

class HealthChecker {
  final Session session;
  static const Duration timeout = Duration(seconds: 5);
  
  HealthChecker(this.session);
  
  /// Check database connectivity
  Future<HealthCheckResult> checkDatabase() async {
    final stopwatch = Stopwatch()..start();
    
    try {
      // Execute simple query to verify connection
      await session.db.unsafeQuery('SELECT 1');
      stopwatch.stop();
      
      return HealthCheckResult(
        name: 'database',
        healthy: true,
        latencyMs: stopwatch.elapsedMilliseconds,
      );
    } catch (e) {
      stopwatch.stop();
      
      return HealthCheckResult(
        name: 'database',
        healthy: false,
        latencyMs: stopwatch.elapsedMilliseconds,
        error: e.toString(),
      );
    }
  }
  
  /// Check cache connectivity
  Future<HealthCheckResult> checkCache() async {
    final stopwatch = Stopwatch()..start();
    final testKey = 'health_check_${DateTime.now().millisecondsSinceEpoch}';
    final testValue = 'ok';
    
    try {
      // Write test value
      await session.caches.local.put(
        testKey,
        testValue,
        lifetime: Duration(seconds: 10),
      );
      
      // Read it back
      final retrieved = await session.caches.local.get<String>(testKey);
      stopwatch.stop();
      
      final isHealthy = retrieved == testValue;
      
      return HealthCheckResult(
        name: 'cache',
        healthy: isHealthy,
        latencyMs: stopwatch.elapsedMilliseconds,
        error: isHealthy ? null : 'Cache read/write mismatch',
      );
    } catch (e) {
      stopwatch.stop();
      
      return HealthCheckResult(
        name: 'cache',
        healthy: false,
        latencyMs: stopwatch.elapsedMilliseconds,
        error: e.toString(),
      );
    }
  }
  
  /// Run all health checks
  Future<SystemHealth> checkAll() async {
    // Run all checks in parallel with timeout
    final results = await Future.wait([
      _runWithTimeout('database', checkDatabase),
      _runWithTimeout('cache', checkCache),
    ]);
    
    return SystemHealth.fromChecks(results);
  }
  
  /// Helper to run a check with timeout
  Future<HealthCheckResult> _runWithTimeout(
    String name,
    Future<HealthCheckResult> Function() check,
  ) async {
    try {
      return await check().timeout(timeout);
    } on TimeoutException {
      return HealthCheckResult(
        name: name,
        healthy: false,
        latencyMs: timeout.inMilliseconds,
        error: 'Health check timed out after ${timeout.inSeconds}s',
      );
    } catch (e) {
      return HealthCheckResult(
        name: name,
        healthy: false,
        latencyMs: 0,
        error: 'Unexpected error: $e',
      );
    }
  }
}

// Usage in a health endpoint:
class HealthEndpoint extends Endpoint {
  Future<Map<String, dynamic>> check(Session session) async {
    final checker = HealthChecker(session);
    final health = await checker.checkAll();
    return health.toJson();
  }
}