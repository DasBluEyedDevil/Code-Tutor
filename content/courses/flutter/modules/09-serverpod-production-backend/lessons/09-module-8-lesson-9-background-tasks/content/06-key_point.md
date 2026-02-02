---
type: "KEY_POINT"
title: "Future Call Best Practices"
---

**1. Use Descriptive Call IDs**

Call IDs must be unique and should be meaningful for debugging:

```dart
// Bad: Non-descriptive
final callId = 'call-123';

// Good: Includes context
final callId = 'order-expiry-${orderId}-${DateTime.now().millisecondsSinceEpoch}';
```

**2. Handle Missing Data Gracefully**

By the time your future call runs, the data it references might be deleted:

```dart
Future<void> handleOrderExpiry(Session session, int orderId) async {
  final order = await Order.db.findById(session, orderId);
  
  // Order might have been deleted or already processed
  if (order == null) {
    session.log('Order $orderId not found, skipping expiry');
    return; // Exit gracefully, do not throw
  }
  
  if (order.status != OrderStatus.pending) {
    session.log('Order $orderId already processed, skipping expiry');
    return;
  }
  
  // Proceed with expiry logic
  await _expireOrder(session, order);
}
```

**3. Idempotency is Critical**

Future calls might run more than once (retries, server restarts). Design for idempotency:

```dart
// Bad: Not idempotent - sends duplicate emails
await EmailService.send(email);

// Good: Check if already sent
final existing = await SentEmail.db.findFirstRow(
  session,
  where: (t) => t.emailId.equals(emailId),
);

if (existing != null) {
  session.log('Email $emailId already sent, skipping');
  return;
}

await EmailService.send(email);
await SentEmail.db.insertRow(session, SentEmail(emailId: emailId, sentAt: DateTime.now()));
```

**4. Consider Time Zones**

When scheduling based on user preferences, account for time zones:

```dart
// Schedule reminder for 9 AM in user's local time
final userTimezone = user.timezone; // e.g., 'America/New_York'
final localTime = DateTime(2024, 1, 15, 9, 0); // 9 AM
final utcTime = convertToUtc(localTime, userTimezone);

await session.serverpod.futureCallWithDelay(
  callId,
  data,
  utcTime.difference(DateTime.now()),
);
```

