---
type: "WARNING"
title: "Common Testing Mistakes"
---

Avoid these testing pitfalls:

1. **Testing implementation, not behavior**:
   ```javascript
   // BAD - tests internal implementation
   expect(user._privateMethod).toHaveBeenCalled();
   
   // GOOD - tests observable behavior
   expect(user.getName()).toBe('Alice');
   ```

2. **Skipping edge cases**:
   - Empty inputs (null, undefined, '', [])
   - Boundary values (0, -1, MAX_INT)
   - Invalid types

3. **Tests that depend on each other**:
   Each test should set up its own state. Don't rely on order.

4. **Not testing the unhappy path**:
   Test what happens when things go WRONG, not just when they go right.

5. **Ignoring flaky tests**:
   A test that sometimes passes and sometimes fails is worse than no test.