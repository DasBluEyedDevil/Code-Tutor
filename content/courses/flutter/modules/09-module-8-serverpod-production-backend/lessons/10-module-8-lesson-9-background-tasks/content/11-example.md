---
type: "EXAMPLE"
title: "Production Email Queue Implementation"
---

Here is a production-ready email queue with database persistence, retries, and tracking.



```dart
// Step 1: Define the email queue model
// File: lib/src/protocol/email_queue.yaml
//
// class: EmailQueueItem
// table: email_queue
// fields:
//   to: String
//   subject: String
//   bodyHtml: String
//   bodyText: String?
//   status: String          # pending, sending, sent, failed
//   attempts: int
//   maxAttempts: int
//   lastError: String?
//   scheduledAt: DateTime
//   sentAt: DateTime?
//   createdAt: DateTime

// Step 2: Create the email service
// File: lib/src/services/email_service.dart

import 'package:serverpod/serverpod.dart';
import 'package:mailer/mailer.dart';
import 'package:mailer/smtp_server.dart';
import '../generated/protocol.dart';

class EmailService {
  static late SmtpServer _smtpServer;
  
  /// Initialize the SMTP configuration.
  static void initialize({
    required String host,
    required int port,
    required String username,
    required String password,
  }) {
    _smtpServer = SmtpServer(
      host,
      port: port,
      username: username,
      password: password,
      ssl: true,
    );
  }
  
  /// Queue an email for sending.
  static Future<int> queueEmail(
    Session session, {
    required String to,
    required String subject,
    required String bodyHtml,
    String? bodyText,
    DateTime? scheduledAt,
    int maxAttempts = 5,
  }) async {
    final item = EmailQueueItem(
      to: to,
      subject: subject,
      bodyHtml: bodyHtml,
      bodyText: bodyText,
      status: 'pending',
      attempts: 0,
      maxAttempts: maxAttempts,
      scheduledAt: scheduledAt ?? DateTime.now(),
      createdAt: DateTime.now(),
    );
    
    final saved = await EmailQueueItem.db.insertRow(session, item);
    session.log('Queued email to $to: $subject');
    return saved.id!;
  }
  
  /// Process pending emails (called by scheduled job).
  static Future<void> processPendingEmails(
    Session session, {
    int batchSize = 50,
  }) async {
    final now = DateTime.now();
    
    // Find emails ready to send
    final pending = await EmailQueueItem.db.find(
      session,
      where: (t) => 
        t.status.equals('pending') &
        t.scheduledAt.isSmallerOrEqualTo(now) &
        t.attempts.isSmallerThan(Column('max_attempts')),
      limit: batchSize,
      orderBy: (t) => t.scheduledAt,
    );
    
    session.log('Processing ${pending.length} pending emails');
    
    for (final item in pending) {
      await _sendEmail(session, item);
    }
  }
  
  static Future<void> _sendEmail(
    Session session,
    EmailQueueItem item,
  ) async {
    // Mark as sending
    item.status = 'sending';
    item.attempts++;
    await EmailQueueItem.db.updateRow(session, item);
    
    try {
      // Build the email message
      final message = Message()
        ..from = Address('noreply@yourapp.com', 'Your App')
        ..recipients.add(item.to)
        ..subject = item.subject
        ..html = item.bodyHtml
        ..text = item.bodyText;
      
      // Send via SMTP
      final sendReport = await send(message, _smtpServer);
      
      // Mark as sent
      item.status = 'sent';
      item.sentAt = DateTime.now();
      item.lastError = null;
      await EmailQueueItem.db.updateRow(session, item);
      
      session.log('Sent email ${item.id} to ${item.to}');
      
    } catch (e, stackTrace) {
      session.log(
        'Failed to send email ${item.id}: $e',
        level: LogLevel.warning,
      );
      
      // Record the error
      item.lastError = e.toString();
      
      if (item.attempts >= item.maxAttempts) {
        // Permanent failure
        item.status = 'failed';
        session.log(
          'Email ${item.id} failed permanently after ${item.attempts} attempts',
          level: LogLevel.error,
        );
      } else {
        // Will retry later
        item.status = 'pending';
        // Exponential backoff: 1min, 4min, 16min, 64min, etc.
        final delay = Duration(minutes: pow(4, item.attempts - 1).toInt());
        item.scheduledAt = DateTime.now().add(delay);
        session.log('Email ${item.id} will retry in ${delay.inMinutes} minutes');
      }
      
      await EmailQueueItem.db.updateRow(session, item);
    }
  }
  
  /// Helper: Send a welcome email.
  static Future<int> sendWelcomeEmail(
    Session session,
    String to,
    String userName,
  ) async {
    return queueEmail(
      session,
      to: to,
      subject: 'Welcome to Our App!',
      bodyHtml: '''
        <h1>Welcome, $userName!</h1>
        <p>Thank you for signing up. We are excited to have you on board.</p>
        <p>Get started by exploring our features.</p>
      ''',
      bodyText: 'Welcome, $userName! Thank you for signing up.',
    );
  }
  
  /// Helper: Send a password reset email.
  static Future<int> sendPasswordResetEmail(
    Session session,
    String to,
    String resetToken,
  ) async {
    final resetUrl = 'https://yourapp.com/reset?token=$resetToken';
    
    return queueEmail(
      session,
      to: to,
      subject: 'Reset Your Password',
      bodyHtml: '''
        <h1>Password Reset Request</h1>
        <p>Click the link below to reset your password:</p>
        <p><a href="$resetUrl">Reset Password</a></p>
        <p>This link expires in 1 hour.</p>
        <p>If you did not request this, please ignore this email.</p>
      ''',
      bodyText: 'Reset your password: $resetUrl',
    );
  }
}

int pow(int base, int exponent) {
  int result = 1;
  for (int i = 0; i < exponent; i++) {
    result *= base;
  }
  return result;
}

// Step 3: Create scheduled job to process queue
// File: lib/src/scheduled_jobs/email_processor_job.dart

class EmailProcessorJob extends ScheduledJob {
  @override
  String get name => 'email-processor';
  
  @override
  Duration get interval => Duration(minutes: 1);
  
  @override
  Future<void> run(Session session) async {
    await EmailService.processPendingEmails(session);
  }
}
```
