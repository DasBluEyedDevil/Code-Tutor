---
type: "THEORY"
title: "Transactions for Atomic Operations"
---

A transaction groups multiple database operations into a single unit that either completely succeeds or completely fails. There is no middle ground - if any operation fails, all changes are rolled back.

**When to Use Transactions:**

1. **Transferring money**: Debit one account AND credit another (both or neither)
2. **Creating related records**: User AND their profile (both must exist)
3. **Inventory updates**: Decrement stock AND create order (must match)
4. **Cascading deletes**: Remove user AND all their posts (consistency)

**Without a Transaction (Dangerous):**

```dart
// If this succeeds...
await Account.db.updateRow(session, sender.copyWith(balance: sender.balance - 100));

// ...but this fails (network error, validation, etc.)
await Account.db.updateRow(session, receiver.copyWith(balance: receiver.balance + 100));

// Money disappeared! Sender lost 100, receiver got nothing.
```

**With a Transaction (Safe):**

```dart
await session.db.transaction((transaction) async {
  await Account.db.updateRow(session, sender.copyWith(balance: sender.balance - 100));
  await Account.db.updateRow(session, receiver.copyWith(balance: receiver.balance + 100));
});
// Either BOTH succeed, or NEITHER happens. Money is never lost.
```

