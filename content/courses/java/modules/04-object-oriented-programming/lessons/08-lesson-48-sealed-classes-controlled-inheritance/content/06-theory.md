---
type: "THEORY"
title: "When to Use Sealed Classes"
---

USE SEALED CLASSES WHEN:
- You have a FIXED set of subtypes (known at design time)
- You want EXHAUSTIVE pattern matching
- You're modeling domain concepts with clear variants
- You want to prevent unexpected extensions

GREAT USE CASES:
- Result types (Success/Failure/Pending)
- AST nodes (expressions, statements)
- State machines (states are fixed)
- Payment methods (Card/Bank/Crypto)
- HTTP responses (Ok/Error/Redirect)

sealed interface PaymentResult permits Approved, Declined, Pending {}
record Approved(String transactionId) implements PaymentResult {}
record Declined(String reason) implements PaymentResult {}
record Pending(String checkUrl) implements PaymentResult {}

DON'T USE SEALED CLASSES WHEN:
- Subtypes should be extensible by users
- The set of subtypes is unknown or growing
- You're building a library meant for extension

SEALED vs FINAL vs ABSTRACT:
- abstract: Must be extended, anyone can
- final: Cannot be extended at all
- sealed: Can only be extended by specific classes