---
type: "KEY_POINT"
title: "Common Mistakes to Avoid"
---

❌ WRONG: int result = 5 x 3;  (x is not an operator)
✓ CORRECT: int result = 5 * 3;

❌ WRONG: int half = 10 / 2.0;  (can't assign double to int)
✓ CORRECT: double half = 10 / 2.0;

❌ WRONG: int result = 10 / 3;  // Thinking this gives 3.33
✓ CORRECT: double result = 10.0 / 3.0;  // This gives 3.33

❌ WRONG: x =+ 5;  (This assigns 5, doesn't add!)
✓ CORRECT: x += 5;  (This adds 5 to x)

Remember: In integer division, the decimal part is thrown away!
7 / 2 = 3 (not 3.5)