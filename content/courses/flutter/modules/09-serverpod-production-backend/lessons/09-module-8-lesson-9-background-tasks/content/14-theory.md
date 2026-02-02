---
type: "THEORY"
title: "Monitoring Background Tasks"
---

Background tasks are invisible to users, making monitoring crucial. You need to know when tasks fail, when queues back up, and when jobs take too long.

**Key Metrics to Track:**

1. **Queue Depth**: How many tasks are waiting?
   - Alert if queue exceeds threshold
   - May indicate workers are down or overwhelmed

2. **Processing Time**: How long do tasks take?
   - Track P50, P95, P99 latencies
   - Identify slow tasks for optimization

3. **Success/Failure Rate**: What percentage succeed?
   - Alert on sudden increase in failures
   - Track by task type

4. **Retry Rate**: How often do tasks need retrying?
   - High retry rate indicates systemic issues

5. **Dead Letter Queue Size**: How many tasks failed permanently?
   - These need manual investigation

**Logging Best Practices:**

```dart
// Bad: No context
session.log('Task failed');

// Good: Rich context for debugging
session.log(
  'EmailTask failed: SMTP connection timeout',
  level: LogLevel.error,
  metadata: {
    'taskId': task.id,
    'taskType': 'email',
    'recipient': task.to,
    'attempt': task.attempts,
    'maxAttempts': task.maxAttempts,
    'queuedAt': task.createdAt.toIso8601String(),
    'processingTimeMs': stopwatch.elapsedMilliseconds,
  },
);
```

**Health Check Endpoints:**

```dart
class HealthEndpoint extends Endpoint {
  Future<Map<String, dynamic>> getBackgroundTaskHealth(Session session) async {
    final emailQueueDepth = await EmailQueueItem.db.count(
      session,
      where: (t) => t.status.equals('pending'),
    );
    
    final failedTasks = await EmailQueueItem.db.count(
      session,
      where: (t) => t.status.equals('failed'),
    );
    
    final oldestPending = await EmailQueueItem.db.findFirstRow(
      session,
      where: (t) => t.status.equals('pending'),
      orderBy: (t) => t.createdAt,
    );
    
    final queueAge = oldestPending != null
        ? DateTime.now().difference(oldestPending.createdAt)
        : Duration.zero;
    
    return {
      'emailQueue': {
        'depth': emailQueueDepth,
        'failedCount': failedTasks,
        'oldestPendingMinutes': queueAge.inMinutes,
        'healthy': emailQueueDepth < 1000 && queueAge.inMinutes < 30,
      },
      // Add other queues...
    };
  }
}
```

