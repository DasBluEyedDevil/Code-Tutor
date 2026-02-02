import 'dart:async';
import 'dart:math';

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
    final email = QueuedEmail(
      id: _nextId++,
      to: to,
      subject: subject,
      body: body,
      status: 'pending',
      attempts: 0,
      maxAttempts: maxAttempts,
      createdAt: DateTime.now(),
    );
    _emails.add(email);
    return email.id;
  }
  
  QueueStats getStats() {
    int pending = 0, sending = 0, sent = 0, failed = 0;
    for (final email in _emails) {
      switch (email.status) {
        case 'pending': pending++; break;
        case 'sending': sending++; break;
        case 'sent': sent++; break;
        case 'failed': failed++; break;
      }
    }
    return QueueStats(pending: pending, sending: sending, sent: sent, failed: failed);
  }
  
  Future<int> processBatch({int batchSize = 10}) async {
    final pendingEmails = _emails.where((e) => e.status == 'pending').take(batchSize).toList();
    int successCount = 0;
    
    for (final email in pendingEmails) {
      email.status = 'sending';
      email.attempts++;
      
      try {
        await _sender.send(email.to, email.subject, email.body);
        email.status = 'sent';
        email.sentAt = DateTime.now();
        email.lastError = null;
        successCount++;
      } catch (e) {
        email.lastError = e.toString();
        if (email.attempts < email.maxAttempts) {
          email.status = 'pending';
        } else {
          email.status = 'failed';
        }
      }
    }
    return successCount;
  }
  
  List<QueuedEmail> getAllEmails() => List.unmodifiable(_emails);
  
  List<QueuedEmail> getFailedEmails() {
    return _emails.where((e) => e.status == 'failed').toList();
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