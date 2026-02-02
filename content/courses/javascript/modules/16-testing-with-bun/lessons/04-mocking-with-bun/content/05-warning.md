---
type: "WARNING"
title: "Mocking Pitfalls"
---

Common mocking mistakes:

1. **Over-mocking**:
   ```javascript
   // BAD - mocking everything defeats the purpose
   mock(add);  // Why mock a simple function?
   mock(multiply);
   mock(subtract);
   
   // GOOD - mock only external dependencies
   mock(fetchFromAPI);
   ```

2. **Forgetting to restore mocks**:
   ```javascript
   // Mocks persist between tests!
   afterEach(() => {
     mockFn.mockRestore();
   });
   ```

3. **Mocking implementation instead of interface**:
   If you change the mock every time you refactor, your tests are too coupled.

4. **Not resetting call history**:
   ```javascript
   // WRONG - call count from previous test
   expect(mock).toHaveBeenCalledTimes(1);
   
   // CORRECT - reset in beforeEach
   beforeEach(() => mock.mockClear());
   ```

5. **Mocking what you're testing**:
   Never mock the module you're actually trying to test!