---
type: "KEY_POINT"
title: "Health Checks and Monitoring"
---


**Built-in Health Endpoints**

Serverpod provides automatic health check endpoints:

| Endpoint | Purpose |
|----------|--------|
| `/health` | Basic server health |
| `/ready` | Readiness probe for load balancers |
| `/live` | Liveness probe for orchestrators |

**Implementing Custom Health Checks**

```dart
// lib/src/endpoints/health_endpoint.dart
import 'package:serverpod/serverpod.dart';

class HealthEndpoint extends Endpoint {
  /// Comprehensive health check
  Future<HealthStatus> checkHealth(Session session) async {
    final checks = <String, HealthCheck>{};
    
    // Database check
    checks['database'] = await _checkDatabase(session);
    
    // Redis check (if using cache)
    checks['cache'] = await _checkCache(session);
    
    // External API check
    checks['payment_gateway'] = await _checkPaymentGateway();
    
    // Determine overall status
    final allHealthy = checks.values.every((c) => c.healthy);
    
    return HealthStatus(
      healthy: allHealthy,
      timestamp: DateTime.now(),
      checks: checks,
    );
  }
  
  Future<HealthCheck> _checkDatabase(Session session) async {
    try {
      final start = DateTime.now();
      // Simple query to verify connection
      await session.db.unsafeQuery('SELECT 1');
      final latency = DateTime.now().difference(start);
      
      return HealthCheck(
        name: 'database',
        healthy: true,
        latencyMs: latency.inMilliseconds,
      );
    } catch (e) {
      return HealthCheck(
        name: 'database',
        healthy: false,
        error: e.toString(),
      );
    }
  }
  
  Future<HealthCheck> _checkCache(Session session) async {
    try {
      final start = DateTime.now();
      // Ping Redis
      await session.caches.local.put(
        'health_check',
        'ok',
        lifetime: Duration(seconds: 10),
      );
      final value = await session.caches.local.get<String>('health_check');
      final latency = DateTime.now().difference(start);
      
      return HealthCheck(
        name: 'cache',
        healthy: value == 'ok',
        latencyMs: latency.inMilliseconds,
      );
    } catch (e) {
      return HealthCheck(
        name: 'cache',
        healthy: false,
        error: e.toString(),
      );
    }
  }
  
  Future<HealthCheck> _checkPaymentGateway() async {
    // Check external payment service
    try {
      // Simulated check
      return HealthCheck(
        name: 'payment_gateway',
        healthy: true,
        latencyMs: 45,
      );
    } catch (e) {
      return HealthCheck(
        name: 'payment_gateway',
        healthy: false,
        error: e.toString(),
      );
    }
  }
}
```

**Database Health Metrics**

Serverpod tracks database connection pool statistics:

- Active connections
- Idle connections
- Connection wait time
- Query execution time

**Memory and CPU Monitoring**

The Insights dashboard displays:
- Heap memory usage
- Resident set size (RSS)
- CPU utilization per isolate
- Request throughput

**Setting Up Alerts**

While Serverpod Insights provides visualization, for alerting you should:

1. Export metrics to Prometheus/Grafana
2. Use cloud provider monitoring (AWS CloudWatch, GCP Monitoring)
3. Implement webhook notifications for critical health failures

