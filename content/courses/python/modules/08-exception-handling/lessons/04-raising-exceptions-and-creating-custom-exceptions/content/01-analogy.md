---
type: "ANALOGY"
title: "The Concept: Activating the Fire Alarm"
---

So far, we've been CATCHING exceptions (responding to the alarm when it goes off). But what about RAISING exceptions (pressing the fire alarm button yourself)?

**Why would you raise an exception?**

Imagine you're a security guard checking IDs at a club:
- Someone shows a fake ID → You raise an alarm (raise ValueError)
- Someone is under 21 → You raise an alarm (raise ValueError)
- Someone is on the banned list → You raise a CUSTOM alarm (raise BannedPersonError)

You're not waiting for something to break - you're actively detecting a problem and **signaling it** by raising an exception.

**Real-world code scenarios:**

1. **Validation:** Age is negative → raise ValueError("Age cannot be negative")
2. **Business rules:** Withdrawal exceeds balance → raise InsufficientFundsError("Not enough money")
3. **Preconditions:** Function requires positive number but got zero → raise ValueError("Expected positive number")

**Custom Exceptions** are like creating your own fire alarm sounds:
- Standard alarm: ValueError, TypeError (built-in)
- Custom alarm: InsufficientFundsError, InvalidPasswordError (your own)

Custom exceptions make your code self-documenting: except InsufficientFundsError: is clearer than except ValueError: in a banking app.

**Exception vs. Return Value:**
- **Return error value:** User enters wrong password → return False (expected, common)
- **Raise exception:** Credit card number has letters → raise ValueError (unexpected, exceptional)

Rule of thumb: If the error is EXCEPTIONAL (shouldn't normally happen), raise an exception. If it's EXPECTED (users often get it wrong), return a value.