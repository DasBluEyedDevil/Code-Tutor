---
type: "EXAMPLE"
title: "Database Monitoring and Query Analysis"
---


Monitor database performance and optimize slow queries:



```dart
// lib/src/util/query_monitor.dart
import 'package:serverpod/serverpod.dart';

/// Wrapper for monitoring database query performance
class QueryMonitor {
  final Session session;
  static const int slowQueryThresholdMs = 100;
  
  QueryMonitor(this.session);
  
  /// Execute and monitor a query
  Future<T> monitor<T>(
    String queryName,
    Future<T> Function() queryFn,
  ) async {
    final stopwatch = Stopwatch()..start();
    
    try {
      final result = await queryFn();
      stopwatch.stop();
      
      final durationMs = stopwatch.elapsedMilliseconds;
      
      // Log slow queries
      if (durationMs > slowQueryThresholdMs) {
        session.log(
          'SLOW_QUERY [$queryName] took ${durationMs}ms',
          level: LogLevel.warning,
        );
      } else {
        session.log(
          'QUERY [$queryName] completed in ${durationMs}ms',
          level: LogLevel.debug,
        );
      }
      
      return result;
    } catch (e, stackTrace) {
      stopwatch.stop();
      session.log(
        'QUERY_ERROR [$queryName] failed after ${stopwatch.elapsedMilliseconds}ms',
        level: LogLevel.error,
        exception: e,
        stackTrace: stackTrace,
      );
      rethrow;
    }
  }
}

// lib/src/endpoints/analytics_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../util/query_monitor.dart';

class AnalyticsEndpoint extends Endpoint {
  /// Get dashboard data with query monitoring
  Future<DashboardData> getDashboard(Session session) async {
    final monitor = QueryMonitor(session);
    
    // Monitor individual queries
    final orderCount = await monitor.monitor(
      'count_orders',
      () => Order.db.count(session),
    );
    
    final recentOrders = await monitor.monitor(
      'fetch_recent_orders',
      () => Order.db.find(
        session,
        orderBy: (t) => t.createdAt,
        orderDescending: true,
        limit: 10,
      ),
    );
    
    final topProducts = await monitor.monitor(
      'fetch_top_products',
      () => _getTopProducts(session),
    );
    
    return DashboardData(
      totalOrders: orderCount,
      recentOrders: recentOrders,
      topProducts: topProducts,
    );
  }
  
  Future<List<ProductStats>> _getTopProducts(Session session) async {
    // Complex aggregation query
    final results = await session.db.unsafeQuery('''
      SELECT 
        p.id,
        p.name,
        COUNT(oi.id) as order_count,
        SUM(oi.quantity) as total_quantity,
        SUM(oi.quantity * oi.price) as total_revenue
      FROM products p
      LEFT JOIN order_items oi ON oi.product_id = p.id
      LEFT JOIN orders o ON o.id = oi.order_id
      WHERE o.created_at > NOW() - INTERVAL '30 days'
      GROUP BY p.id, p.name
      ORDER BY total_revenue DESC
      LIMIT 10
    ''');
    
    return results.map((row) => ProductStats(
      productId: row[0] as int,
      name: row[1] as String,
      orderCount: row[2] as int,
      totalQuantity: row[3] as int,
      totalRevenue: (row[4] as num).toDouble(),
    )).toList();
  }
}

// Connection pool monitoring
class ConnectionPoolMonitor {
  /// Log connection pool statistics
  static Future<void> logPoolStats(Session session) async {
    // Access pool statistics from Serverpod's database manager
    session.log(
      'CONNECTION_POOL: Active connections logged',
      level: LogLevel.debug,
    );
  }
}

// Database maintenance utilities
class DatabaseMaintenance {
  /// Run ANALYZE on tables for query optimization
  static Future<void> analyzeTable(
    Session session,
    String tableName,
  ) async {
    session.log(
      'Running ANALYZE on table: $tableName',
      level: LogLevel.info,
    );
    
    await session.db.unsafeQuery('ANALYZE $tableName');
    
    session.log(
      'ANALYZE completed for: $tableName',
      level: LogLevel.info,
    );
  }
  
  /// Check for missing indexes
  static Future<List<String>> findMissingIndexes(Session session) async {
    final results = await session.db.unsafeQuery('''
      SELECT
        schemaname || '.' || relname AS table,
        seq_scan,
        seq_tup_read,
        idx_scan,
        idx_tup_fetch
      FROM pg_stat_user_tables
      WHERE seq_scan > idx_scan
        AND seq_tup_read > 10000
      ORDER BY seq_tup_read DESC
      LIMIT 10
    ''');
    
    return results.map((row) => row[0] as String).toList();
  }
}
```
