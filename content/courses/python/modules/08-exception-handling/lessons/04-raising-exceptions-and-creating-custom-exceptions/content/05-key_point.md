---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Raise exceptions** with raise ExceptionType('message') when validation fails or preconditions aren't met. Always include a descriptive message.
- **Custom exceptions** are classes that inherit from Exception. Use them for domain-specific errors: class MyError(Exception): pass
- **Exception hierarchy** lets you group related exceptions. Create a base exception and inherit from it: class BankError(Exception), then class InsufficientFundsError(BankError).
- **Re-raise exceptions** with bare raise (not raise e) to preserve the original stack trace. Useful for logging then re-raising.
- **Use exceptions for exceptional cases** (validation failures, violated preconditions). Use return values for expected alternate flows (search returns no results).
- **Exception messages should be actionable:** Include what went wrong, expected vs. actual values, and how to fix it.
- **Custom exceptions make code self-documenting:** except InsufficientFundsError is clearer than except ValueError in a banking app.
- **Don't use exceptions for control flow:** They're slower than if/else and make code harder to understand. Save them for truly exceptional situations.