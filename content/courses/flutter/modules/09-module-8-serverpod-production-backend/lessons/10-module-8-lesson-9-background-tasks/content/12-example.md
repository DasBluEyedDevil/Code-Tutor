---
type: "EXAMPLE"
title: "Report Generation Patterns"
---

Generating large reports can take minutes. Background processing keeps users happy while reports generate asynchronously.



```dart
// File: lib/src/protocol/report_request.yaml
//
// class: ReportRequest
// table: report_requests
// fields:
//   userId: int
//   reportType: String        # 'sales', 'users', 'inventory'
//   parameters: String        # JSON encoded parameters
//   status: String            # 'pending', 'processing', 'completed', 'failed'
//   progress: int             # 0-100 percentage
//   resultUrl: String?        # URL to download when complete
//   errorMessage: String?
//   createdAt: DateTime
//   startedAt: DateTime?
//   completedAt: DateTime?

// File: lib/src/endpoints/report_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/report_generator.dart';

class ReportEndpoint extends Endpoint {
  /// Request a new report (returns immediately).
  Future<ReportRequest> requestReport(
    Session session,
    String reportType,
    Map<String, dynamic> parameters,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    // Create the request
    final request = ReportRequest(
      userId: userId,
      reportType: reportType,
      parameters: jsonEncode(parameters),
      status: 'pending',
      progress: 0,
      createdAt: DateTime.now(),
    );
    
    final saved = await ReportRequest.db.insertRow(session, request);
    
    // Queue for background processing
    await session.messages.postMessage(
      'report-generation',
      ReportGenerationMessage(requestId: saved.id!),
    );
    
    session.log('Report ${saved.id} queued for generation');
    return saved;
  }
  
  /// Check report status (poll this endpoint).
  Future<ReportRequest?> getReportStatus(
    Session session,
    int requestId,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    final request = await ReportRequest.db.findById(session, requestId);
    
    // Security: only return if user owns this report
    if (request?.userId != userId) return null;
    
    return request;
  }
  
  /// Get user's report history.
  Future<List<ReportRequest>> getMyReports(Session session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw UnauthorizedException();
    
    return await ReportRequest.db.find(
      session,
      where: (t) => t.userId.equals(userId),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: 20,
    );
  }
}

// File: lib/src/services/report_generator.dart

class ReportGenerator {
  /// Generate a report (called by background worker).
  static Future<void> generateReport(
    Session session,
    int requestId,
  ) async {
    final request = await ReportRequest.db.findById(session, requestId);
    if (request == null) {
      session.log('Report request $requestId not found');
      return;
    }
    
    try {
      // Mark as processing
      request.status = 'processing';
      request.startedAt = DateTime.now();
      await ReportRequest.db.updateRow(session, request);
      
      // Parse parameters
      final params = jsonDecode(request.parameters) as Map<String, dynamic>;
      
      // Generate based on type
      final reportData = await _generateReportData(
        session,
        request.reportType,
        params,
        (progress) async {
          // Update progress periodically
          request.progress = progress;
          await ReportRequest.db.updateRow(session, request);
        },
      );
      
      // Create the report file
      final fileName = 'report-${request.id}-${DateTime.now().millisecondsSinceEpoch}.csv';
      final fileUrl = await _uploadReportFile(session, fileName, reportData);
      
      // Mark as completed
      request.status = 'completed';
      request.progress = 100;
      request.resultUrl = fileUrl;
      request.completedAt = DateTime.now();
      await ReportRequest.db.updateRow(session, request);
      
      // Notify user (optional)
      await _notifyUser(session, request);
      
      session.log('Report ${request.id} completed: $fileUrl');
      
    } catch (e, stackTrace) {
      session.log(
        'Report ${request.id} failed: $e',
        level: LogLevel.error,
        stackTrace: stackTrace,
      );
      
      request.status = 'failed';
      request.errorMessage = e.toString();
      request.completedAt = DateTime.now();
      await ReportRequest.db.updateRow(session, request);
    }
  }
  
  static Future<String> _generateReportData(
    Session session,
    String reportType,
    Map<String, dynamic> params,
    Future<void> Function(int progress) onProgress,
  ) async {
    final buffer = StringBuffer();
    
    switch (reportType) {
      case 'sales':
        await _generateSalesReport(session, params, buffer, onProgress);
        break;
      case 'users':
        await _generateUsersReport(session, params, buffer, onProgress);
        break;
      default:
        throw ArgumentError('Unknown report type: $reportType');
    }
    
    return buffer.toString();
  }
  
  static Future<void> _generateSalesReport(
    Session session,
    Map<String, dynamic> params,
    StringBuffer buffer,
    Future<void> Function(int progress) onProgress,
  ) async {
    final startDate = DateTime.parse(params['startDate'] as String);
    final endDate = DateTime.parse(params['endDate'] as String);
    
    // Header
    buffer.writeln('Date,OrderId,Customer,Amount,Status');
    
    // Get total count for progress
    final totalCount = await Order.db.count(
      session,
      where: (t) => t.createdAt.between(startDate, endDate),
    );
    
    // Process in batches
    const batchSize = 100;
    var processed = 0;
    var offset = 0;
    
    while (true) {
      final orders = await Order.db.find(
        session,
        where: (t) => t.createdAt.between(startDate, endDate),
        limit: batchSize,
        offset: offset,
        orderBy: (t) => t.createdAt,
      );
      
      if (orders.isEmpty) break;
      
      for (final order in orders) {
        buffer.writeln(
          '${order.createdAt.toIso8601String()},'
          '${order.id},'
          '${order.customerName},'
          '${order.total},'
          '${order.status}'
        );
      }
      
      processed += orders.length;
      offset += batchSize;
      
      // Update progress (cap at 95% until file is uploaded)
      final progress = (processed / totalCount * 95).round();
      await onProgress(progress.clamp(0, 95));
      
      // Yield to other tasks
      await Future.delayed(Duration(milliseconds: 10));
    }
  }
  
  static Future<void> _generateUsersReport(
    Session session,
    Map<String, dynamic> params,
    StringBuffer buffer,
    Future<void> Function(int progress) onProgress,
  ) async {
    // Similar pattern to sales report
    buffer.writeln('UserId,Email,CreatedAt,LastLogin,OrderCount');
    
    final users = await User.db.find(session);
    final total = users.length;
    
    for (var i = 0; i < users.length; i++) {
      final user = users[i];
      final orderCount = await Order.db.count(
        session,
        where: (t) => t.userId.equals(user.id!),
      );
      
      buffer.writeln(
        '${user.id},${user.email},${user.createdAt},${user.lastLogin},$orderCount'
      );
      
      await onProgress(((i + 1) / total * 95).round());
    }
  }
  
  static Future<String> _uploadReportFile(
    Session session,
    String fileName,
    String content,
  ) async {
    // Upload to cloud storage (S3, GCS, etc.)
    // Return the download URL
    // For now, simulate:
    return 'https://storage.yourapp.com/reports/$fileName';
  }
  
  static Future<void> _notifyUser(
    Session session,
    ReportRequest request,
  ) async {
    final user = await User.db.findById(session, request.userId);
    if (user == null) return;
    
    await EmailService.queueEmail(
      session,
      to: user.email,
      subject: 'Your Report is Ready',
      bodyHtml: '''
        <h1>Your report is ready!</h1>
        <p>Your ${request.reportType} report has been generated.</p>
        <p><a href="${request.resultUrl}">Download Report</a></p>
      ''',
    );
  }
}
```
