---
type: "WARNING"
title: "Scheduled Job Pitfalls"
---

**1. Long-Running Jobs Block the Scheduler**

If a scheduled job takes longer than its interval, issues arise:

```dart
// Dangerous: Job takes 2 hours but runs every hour
class SlowJob extends ScheduledJob {
  @override
  Duration get interval => Duration(hours: 1);
  
  @override
  Future<void> run(Session session) async {
    // This takes 2 hours!
    await processAllRecords(session);
  }
}
```

**Solution**: Break work into smaller chunks or use a different pattern:

```dart
@override
Future<void> run(Session session) async {
  // Process in batches, not all at once
  const batchSize = 100;
  var offset = 0;
  
  while (true) {
    final records = await Record.db.find(
      session,
      where: (t) => t.needsProcessing.equals(true),
      limit: batchSize,
      offset: offset,
    );
    
    if (records.isEmpty) break;
    
    for (final record in records) {
      await processRecord(session, record);
    }
    
    offset += batchSize;
    
    // Yield to other tasks periodically
    await Future.delayed(Duration(milliseconds: 100));
  }
}
```

**2. Jobs Run on Every Server Instance**

In a multi-server setup, each server runs its own scheduler:

```
Server A: Runs daily report at 6 AM
Server B: Also runs daily report at 6 AM
Server C: Also runs daily report at 6 AM
// Result: 3 duplicate reports!
```

**Solution**: Use database locks or designate a primary server:

```dart
@override
Future<void> run(Session session) async {
  // Try to acquire a lock
  final lockAcquired = await session.db.query<bool>(
    'SELECT pg_try_advisory_lock(12345)' // Unique lock ID
  );
  
  if (lockAcquired != true) {
    session.log('Another instance is running this job, skipping');
    return;
  }
  
  try {
    await _doActualWork(session);
  } finally {
    await session.db.query('SELECT pg_advisory_unlock(12345)');
  }
}
```

**3. No Built-in Failure Alerting**

Scheduled jobs fail silently by default. Always add alerting:

```dart
@override
Future<void> run(Session session) async {
  try {
    await _doWork(session);
  } catch (e, stackTrace) {
    // Log the error
    session.log('Job failed: $e', level: LogLevel.error, stackTrace: stackTrace);
    
    // Alert the team
    await AlertService.sendSlackMessage(
      channel: '#alerts',
      message: 'Scheduled job "$name" failed: $e',
    );
    
    // Still rethrow so Serverpod logs it
    rethrow;
  }
}
```

