---
type: "WARNING"
title: "Common Bun Test Pitfalls"
---

Watch out for these common issues:

1. **Wrong file naming**:
   ```bash
   # WRONG - won't be found
   mytest.js
   tests.js
   
   # CORRECT - detected automatically
   my.test.js
   my.spec.js
   my_test.js
   ```

2. **Forgetting async/await**:
   ```javascript
   // WRONG - test passes before promise resolves!
   it('fetches data', () => {
     fetchData().then(data => expect(data).toBe('ok'));
   });
   
   // CORRECT - wait for promise
   it('fetches data', async () => {
     const data = await fetchData();
     expect(data).toBe('ok');
   });
   ```

3. **Confusing toBe vs toEqual**:
   ```javascript
   expect([1, 2]).toBe([1, 2]);    // FAILS! Different objects
   expect([1, 2]).toEqual([1, 2]); // PASSES! Same values
   ```

4. **Not running in watch mode during development**:
   ```bash
   bun test          # Watch mode (default)
   bun test --run    # Run once and exit (for CI)
   ```