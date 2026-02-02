---
type: "WARNING"
title: "Integration Testing Pitfalls"
---

Common Hono integration testing mistakes:

1. **Sharing app state between tests**:
   ```javascript
   // WRONG - state leaks between tests
   const app = createApp();
   
   // CORRECT - fresh app each test
   beforeEach(() => {
     app = createApp();
   });
   ```

2. **Forgetting Content-Type header**:
   ```javascript
   // WRONG - Hono won't parse body
   await app.request('/todos', {
     method: 'POST',
     body: JSON.stringify({ text: 'Test' })
   });
   
   // CORRECT - include Content-Type
   await app.request('/todos', {
     method: 'POST',
     headers: { 'Content-Type': 'application/json' },
     body: JSON.stringify({ text: 'Test' })
   });
   ```

3. **Not testing error responses**:
   Always test 400, 404, 401 cases, not just happy paths.

4. **Calling res.json() twice**:
   ```javascript
   const data = await res.json();  // First call
   console.log(await res.json());  // ERROR! Body already consumed
   ```

5. **Ignoring async/await**:
   Both app.request() and res.json() return Promises. Always await them.