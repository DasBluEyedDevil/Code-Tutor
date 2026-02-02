---
type: "EXAMPLE"
title: "Implementing Performance Budgets"
---


Create a system to track and report budget violations:



```dart
// lib/performance/performance_budget.dart
import 'package:firebase_performance/firebase_performance.dart';

/// Performance budget configuration and monitoring
class PerformanceBudget {
  // Budget thresholds in milliseconds
  static const int coldStartBudgetMs = 3000;
  static const int warmStartBudgetMs = 1500;
  static const int screenTransitionBudgetMs = 300;
  static const int apiResponseBudgetMs = 500;
  static const int databaseQueryBudgetMs = 100;
  static const double maxSlowFramePercent = 5.0;
  
  final FirebasePerformance _performance;
  
  PerformanceBudget() : _performance = FirebasePerformance.instance;
  
  /// Check and log a budget violation
  Future<void> checkBudget({
    required String operationType,
    required int actualMs,
    required int budgetMs,
    Map<String, String>? context,
  }) async {
    if (actualMs > budgetMs) {
      await _logBudgetViolation(
        operationType: operationType,
        actualMs: actualMs,
        budgetMs: budgetMs,
        context: context,
      );
    }
  }
  
  Future<void> _logBudgetViolation({
    required String operationType,
    required int actualMs,
    required int budgetMs,
    Map<String, String>? context,
  }) async {
    final trace = _performance.newTrace('budget_violation');
    
    trace.putAttribute('operation_type', operationType);
    trace.putAttribute('budget_ms', budgetMs.toString());
    trace.putAttribute('over_budget_by', '${actualMs - budgetMs}ms');
    
    // Add custom context
    context?.forEach((key, value) {
      trace.putAttribute(key, value);
    });
    
    trace.incrementMetric('actual_ms', actualMs);
    trace.incrementMetric('budget_ms', budgetMs);
    trace.incrementMetric('exceeded_by_ms', actualMs - budgetMs);
    
    await trace.start();
    await trace.stop();
  }
  
  /// Measure an operation against its budget
  Future<T> measureWithBudget<T>({
    required String operationType,
    required int budgetMs,
    required Future<T> Function() operation,
    Map<String, String>? context,
  }) async {
    final stopwatch = Stopwatch()..start();
    
    try {
      return await operation();
    } finally {
      stopwatch.stop();
      await checkBudget(
        operationType: operationType,
        actualMs: stopwatch.elapsedMilliseconds,
        budgetMs: budgetMs,
        context: context,
      );
    }
  }
  
  /// Check cold start performance
  Future<void> checkColdStart(int durationMs) async {
    await checkBudget(
      operationType: 'cold_start',
      actualMs: durationMs,
      budgetMs: coldStartBudgetMs,
    );
  }
  
  /// Check API response time
  Future<void> checkApiResponse({
    required int durationMs,
    required String endpoint,
  }) async {
    await checkBudget(
      operationType: 'api_response',
      actualMs: durationMs,
      budgetMs: apiResponseBudgetMs,
      context: {'endpoint': endpoint},
    );
  }
  
  /// Check screen transition time
  Future<void> checkScreenTransition({
    required int durationMs,
    required String screenName,
  }) async {
    await checkBudget(
      operationType: 'screen_transition',
      actualMs: durationMs,
      budgetMs: screenTransitionBudgetMs,
      context: {'screen': screenName},
    );
  }
}

// Usage with app startup:
class AppStartupMonitor {
  static DateTime? _appStartTime;
  static final PerformanceBudget _budget = PerformanceBudget();
  
  static void markAppStart() {
    _appStartTime = DateTime.now();
  }
  
  static Future<void> markFirstFrame() async {
    if (_appStartTime != null) {
      final duration = DateTime.now().difference(_appStartTime!);
      await _budget.checkColdStart(duration.inMilliseconds);
    }
  }
}

// In main.dart:
void main() async {
  AppStartupMonitor.markAppStart();
  
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );
  
  runApp(const MyApp());
}

// In your first screen's initState:
@override
void initState() {
  super.initState();
  WidgetsBinding.instance.addPostFrameCallback((_) {
    AppStartupMonitor.markFirstFrame();
  });
}
```
