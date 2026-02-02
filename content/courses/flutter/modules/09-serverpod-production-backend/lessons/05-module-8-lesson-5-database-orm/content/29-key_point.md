---
type: "KEY_POINT"
title: "Transaction Rules"
---

**Rule 1: Keep Transactions Short**

Transactions lock database rows. Long transactions block other operations. Do only database work inside transactions, not HTTP calls or file operations.

```dart
// BAD - HTTP call inside transaction
await session.db.transaction((t) async {
  await User.db.insertRow(session, user);
  await sendWelcomeEmail(user.email); // HTTP call - slow!
  await UserProfile.db.insertRow(session, profile);
});

// GOOD - HTTP call outside transaction
await session.db.transaction((t) async {
  await User.db.insertRow(session, user);
  await UserProfile.db.insertRow(session, profile);
});
await sendWelcomeEmail(user.email); // After transaction commits
```

**Rule 2: Handle Errors Properly**

Transactions automatically roll back on exceptions. Catch and handle errors appropriately.

```dart
try {
  await session.db.transaction((t) async {
    // ... operations ...
  });
  // Success - transaction committed
} catch (e) {
  // Failure - transaction rolled back
  session.log('Transaction failed: $e', level: LogLevel.error);
  rethrow;
}
```

**Rule 3: Avoid Nested Transactions**

Serverpod uses savepoints for nested transactions, but they add complexity. Flatten your transaction logic when possible.

**Rule 4: Return Values**

You can return values from transactions:

```dart
final createdOrder = await session.db.transaction((t) async {
  final order = await Order.db.insertRow(session, newOrder);
  // ... more operations ...
  return order; // This is returned from the transaction
});
```

