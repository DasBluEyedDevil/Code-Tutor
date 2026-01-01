---
type: "KEY_POINT"
title: "Encapsulation is Like a Bank Account"
---

WITHOUT ENCAPSULATION:
= Money sitting in a box on your desk
- Anyone can take money out
- Anyone can add fake money
- No security or validation

WITH ENCAPSULATION:
= Money in a bank account
- You can't directly touch the money
- Must use deposit() method (validates amount)
- Must use withdraw() method (checks balance)
- Bank controls access and enforces rules

In Java:
- Fields = private (like money in vault)
- Methods = public (like bank teller window)
- Methods validate before changing fields