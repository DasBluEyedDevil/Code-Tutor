---
type: "EXAMPLE"
title: "Implementing Future Calls (Delayed Execution)"
---

Future Calls let you schedule a method to run at a specific time in the future. The call is persisted to the database, so it survives server restarts.



```dart
// Step 1: Define a Future Call in your endpoint
// File: lib/src/endpoints/reminder_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ReminderEndpoint extends Endpoint {
  /// Schedule a reminder email for the future.
  /// Returns the ID of the scheduled future call.
  Future<String> scheduleReminder(
    Session session, {
    required int userId,
    required String message,
    required DateTime sendAt,
  }) async {
    // Create a unique identifier for this future call
    final callId = 'reminder-$userId-${DateTime.now().millisecondsSinceEpoch}';
    
    // Schedule the future call
    await session.serverpod.futureCallWithDelay(
      callId,
      ReminderData(userId: userId, message: message),
      sendAt.difference(DateTime.now()),
    );
    
    // Log for debugging
    session.log('Scheduled reminder for user $userId at $sendAt');
    
    return callId;
  }
  
  /// Cancel a previously scheduled reminder.
  Future<void> cancelReminder(Session session, String callId) async {
    await session.serverpod.cancelFutureCall(callId);
    session.log('Cancelled reminder: $callId');
  }
}

// Step 2: Define the data model for the future call
// File: lib/src/protocol/reminder_data.yaml
//
// class: ReminderData
// fields:
//   userId: int
//   message: String

// Step 3: Handle the future call when it triggers
// File: lib/server.dart (add to the run function)

import 'package:serverpod/serverpod.dart';
import 'src/generated/protocol.dart';
import 'src/services/email_service.dart';

void run(List<String> args) async {
  final pod = Serverpod(
    args,
    Protocol(),
    Endpoints(),
  );
  
  // Register the future call handler
  pod.registerFutureCall(
    // This name matches the object type
    'ReminderData',
    (session, data) async {
      if (data is ReminderData) {
        await _handleReminderCall(session, data);
      }
    },
  );
  
  await pod.start();
}

Future<void> _handleReminderCall(Session session, ReminderData data) async {
  try {
    // Fetch user details
    final user = await User.db.findById(session, data.userId);
    if (user == null) {
      session.log('User ${data.userId} not found, skipping reminder');
      return;
    }
    
    // Send the reminder email
    await EmailService.sendEmail(
      to: user.email,
      subject: 'Reminder',
      body: data.message,
    );
    
    session.log('Sent reminder to ${user.email}');
    
    // Optionally record that reminder was sent
    await ReminderLog.db.insertRow(
      session,
      ReminderLog(
        userId: data.userId,
        message: data.message,
        sentAt: DateTime.now(),
      ),
    );
  } catch (e, stackTrace) {
    session.log(
      'Failed to send reminder: $e',
      level: LogLevel.error,
      stackTrace: stackTrace,
    );
    // Rethrow to trigger retry mechanism
    rethrow;
  }
}
```
