---
type: "WARNING"
title: "Async Testing Gotchas"
---

Common async testing mistakes:

1. **Forgotten await**:
   ```javascript
   // WRONG - test passes before promise resolves!
   it('fetches user', () => {
     const user = fetchUser(1);  // Missing await!
     expect(user.name).toBe('Alice');  // user is a Promise, not data
   });
   
   // CORRECT
   it('fetches user', async () => {
     const user = await fetchUser(1);
     expect(user.name).toBe('Alice');
   });
   ```

2. **Not resetting fake timers**:
   ```javascript
   afterEach(() => {
     setSystemTime();  // Reset to real time!
   });
   ```

3. **Racing conditions**:
   Tests that sometimes pass and sometimes fail are worse than no tests. Use proper synchronization.

4. **Testing implementation timing**:
   Don't test that something takes exactly 100ms. Test that it completes and returns correct data.

5. **Unhandled rejections**:
   Always wrap async tests in try/catch or use `.rejects` matcher.