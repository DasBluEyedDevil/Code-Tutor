---
type: "EXAMPLE"
title: "Scheduled Jobs (Cron-Style Tasks)"
---

Scheduled jobs run on a recurring basis, similar to cron jobs in Unix. They are perfect for maintenance tasks, report generation, and periodic cleanup.



```dart
// File: lib/src/scheduled_jobs/daily_report_job.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/report_service.dart';
import '../services/email_service.dart';

/// Generates and emails daily reports.
class DailyReportJob extends ScheduledJob {
  @override
  String get name => 'daily-report';
  
  @override
  Duration get interval => Duration(hours: 24);
  
  @override
  DateTime get startTime {
    // Run at 6 AM UTC every day
    final now = DateTime.now().toUtc();
    var runTime = DateTime.utc(now.year, now.month, now.day, 6, 0, 0);
    
    // If we have passed 6 AM today, schedule for tomorrow
    if (now.isAfter(runTime)) {
      runTime = runTime.add(Duration(days: 1));
    }
    
    return runTime;
  }
  
  @override
  Future<void> run(Session session) async {
    session.log('Starting daily report generation');
    final stopwatch = Stopwatch()..start();
    
    try {
      // Generate the report for yesterday
      final yesterday = DateTime.now().subtract(Duration(days: 1));
      final report = await _generateDailyReport(session, yesterday);
      
      // Save to database
      await DailyReport.db.insertRow(session, report);
      
      // Email to admins
      await _emailReportToAdmins(session, report);
      
      stopwatch.stop();
      session.log(
        'Daily report completed in ${stopwatch.elapsedMilliseconds}ms',
      );
    } catch (e, stackTrace) {
      session.log(
        'Daily report failed: $e',
        level: LogLevel.error,
        stackTrace: stackTrace,
      );
      
      // Notify team of failure
      await _notifyReportFailure(session, e.toString());
      
      rethrow;
    }
  }
  
  Future<DailyReport> _generateDailyReport(
    Session session,
    DateTime date,
  ) async {
    final startOfDay = DateTime(date.year, date.month, date.day);
    final endOfDay = startOfDay.add(Duration(days: 1));
    
    // Aggregate metrics
    final newUsers = await User.db.count(
      session,
      where: (t) => t.createdAt.between(startOfDay, endOfDay),
    );
    
    final activeUsers = await Session.db.count(
      session,
      where: (t) => t.lastActivity.between(startOfDay, endOfDay),
    );
    
    final totalOrders = await Order.db.count(
      session,
      where: (t) => t.createdAt.between(startOfDay, endOfDay),
    );
    
    final revenue = await session.db.query<double>(
      'SELECT COALESCE(SUM(total), 0) FROM orders '
      'WHERE created_at >= @start AND created_at < @end',
      parameters: {'start': startOfDay, 'end': endOfDay},
    );
    
    return DailyReport(
      date: date,
      newUserCount: newUsers,
      activeUserCount: activeUsers,
      orderCount: totalOrders,
      totalRevenue: revenue ?? 0.0,
      generatedAt: DateTime.now(),
    );
  }
  
  Future<void> _emailReportToAdmins(
    Session session,
    DailyReport report,
  ) async {
    final admins = await User.db.find(
      session,
      where: (t) => t.role.equals('admin'),
    );
    
    final subject = 'Daily Report - ${_formatDate(report.date)}';
    final body = '''
Daily Report for ${_formatDate(report.date)}

New Users: ${report.newUserCount}
Active Users: ${report.activeUserCount}
Orders: ${report.orderCount}
Revenue: \$${report.totalRevenue.toStringAsFixed(2)}

Generated at ${report.generatedAt}
''';
    
    for (final admin in admins) {
      await EmailService.sendEmail(
        to: admin.email,
        subject: subject,
        body: body,
      );
    }
  }
  
  Future<void> _notifyReportFailure(Session session, String error) async {
    // Could send to Slack, PagerDuty, etc.
    await EmailService.sendEmail(
      to: 'alerts@yourcompany.com',
      subject: 'ALERT: Daily Report Generation Failed',
      body: 'The daily report job failed with error:\n\n$error',
    );
  }
  
  String _formatDate(DateTime date) {
    return '${date.year}-${date.month.toString().padLeft(2, '0')}-'
           '${date.day.toString().padLeft(2, '0')}';
  }
}

// File: lib/server.dart - Register the scheduled job

void run(List<String> args) async {
  final pod = Serverpod(
    args,
    Protocol(),
    Endpoints(),
  );
  
  // Register scheduled jobs
  pod.registerScheduledJob(DailyReportJob());
  pod.registerScheduledJob(HourlyCleanupJob());
  pod.registerScheduledJob(WeeklyMaintenanceJob());
  
  await pod.start();
}
```
