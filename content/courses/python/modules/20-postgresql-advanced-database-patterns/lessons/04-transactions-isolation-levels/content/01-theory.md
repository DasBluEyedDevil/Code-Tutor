---
type: "THEORY"
title: "ACID Properties Explained"
---

Database transactions are the foundation of data integrity. Every reliable database system guarantees ACID properties:

**Atomicity** - All or Nothing
A transaction is a single unit of work. Either all operations succeed, or none do. If transferring $100 between accounts, both the debit and credit must complete - you can't have money disappear or appear from nowhere.

**Consistency** - Valid State to Valid State
Transactions move the database from one valid state to another. All constraints, foreign keys, and business rules are enforced. You can't end up with an account balance violating a CHECK constraint.

**Isolation** - Concurrent Transactions Don't Interfere
Multiple transactions running simultaneously shouldn't affect each other's results. Your balance inquiry shouldn't see a half-completed transfer from another user.

**Durability** - Committed Data Survives Crashes
Once a transaction commits, it's permanent. Even if the server crashes immediately after, the data is safe on disk.

**Why This Matters for Finance Tracker:**
Transferring funds between accounts MUST be atomic. If the debit succeeds but the credit fails, money vanishes. Transactions ensure both happen or neither happens.