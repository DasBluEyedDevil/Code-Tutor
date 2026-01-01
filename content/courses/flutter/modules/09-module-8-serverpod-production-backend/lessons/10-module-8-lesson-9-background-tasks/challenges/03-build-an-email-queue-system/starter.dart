import 'dart:async';
import 'dart:math';

/// Represents an email in the queue.
class QueuedEmail {
  final int id;
  final String to;
  final String subject;
  final String body;
  String status;
  int attempts;
  final int maxAttempts;
  String? lastError;
  DateTime? sentAt;
  final DateTime createdAt;
  
  QueuedEmail({
    required this.id,
    required this.to,
    required this.subject,
    required this.body,
    this.status = 'pending',
    this.attempts = 0,
    this.maxAttempts = 3,
    this.lastError,
    this.sentAt,
    DateTime? createdAt,
  }) : createdAt = createdAt ?? DateTime.now();
  
  @override
  String toString() {
    return 'Email #$id to $to: $status (attempts: $attempts/$maxAttempts)';
  }
}

/// Statistics about the email queue.
class QueueStats {
  final int pending;
  final int sending;
  final int sent;
  final int failed;
  final int total;
  
  QueueStats({
    required this.pending,
    required this.sending,
    required this.sent,
    required this.failed,
  }) : total = pending + sending + sent + failed;
  
  @override
  String toString() {
    return 'Queue Stats: $pending pending, $sending sending, $sent sent, $failed failed (total: $total)';
  }
}

/// Simulates email sending (randomly fails 30% of the time).
class MockEmailSender {
  final Random _random = Random();
  
  Future<bool> send(String to, String subject, String body) async {
    await Future.delayed(Duration(milliseconds: 50));
    if (_random.nextDouble() < 0.3) {
      throw Exception('SMTP connection failed');
    }
    return true;
  }
}

/// Email queue with batch processing and retry logic.
class EmailQueue {
  final List<QueuedEmail> _emails = [];
  final MockEmailSender _sender = MockEmailSender();
  int _nextId = 1;
  
  int enqueue({
    required String to,
    required String subject,
    required String body,
    int maxAttempts = 3,
  }) {
    // TODO: Create a new QueuedEmail and add it to the list
    return 0;
  }
  
  QueueStats getStats() {
    // TODO: Count emails by status and return QueueStats
    return QueueStats(pending: 0, sending: 0, sent: 0, failed: 0);
  }
  
  Future<int> processBatch({int batchSize = 10}) async {
    // TODO: Process pending emails with retry logic
    return 0;
  }
  
  List<QueuedEmail> getAllEmails() => List.unmodifiable(_emails);
  
  List<QueuedEmail> getFailedEmails() {
    // TODO: Return emails with status 'failed'
    return [];
  }
}

void main() async {
  print('=== Testing EmailQueue ===\n');
  
  final queue = EmailQueue();
  
  print('Test 1: Enqueuing emails');
  for (int i = 1; i <= 5; i++) {
    final id = queue.enqueue(
      to: 'user$i@example.com',
      subject: 'Welcome!',
      body: 'Hello user $i',
    );
    print('Queued email #$id');
  }
  print('');
  
  print('Test 2: Initial queue stats');
  print(queue.getStats());
  print('Expected: 5 pending, 0 sending, 0 sent, 0 failed\n');
  
  print('Test 3: Processing batch');
  final sentCount = await queue.processBatch();
  print('Sent $sentCount emails in first batch');
  print(queue.getStats());
  print('');
  
  print('Test 4: Processing again');
  await queue.processBatch();
  print(queue.getStats());
  print('');
  
  print('Test 5: Process until complete');
  for (int i = 0; i < 10; i++) {
    final stats = queue.getStats();
    if (stats.pending == 0) break;
    await queue.processBatch();
  }
  print('Final stats:');
  print(queue.getStats());
  print('');
  
  print('Test 6: Failed emails');
  final failed = queue.getFailedEmails();
  for (final email in failed) {
    print('  $email - Error: ${email.lastError}');
  }
  if (failed.isEmpty) {
    print('  No failed emails!');
  }
}