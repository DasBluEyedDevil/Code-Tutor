---
type: "WARNING"
title: "Integration Testing Pitfalls"
---

Common integration testing mistakes to avoid:

**1. Shared State Between Tests:**
```javascript
// WRONG - state leaks between tests
const app = createApp();

// CORRECT - fresh instance each test
beforeEach(() => {
  app = createApp();
});
```

**2. Forgetting to Seed Test Data:**
```javascript
// WRONG - test depends on previous test
it('finds user', async () => {
  const res = await app.request('/api/users/1');
  // Fails if 'creates user' test didn't run first
});

// CORRECT - each test is self-contained
beforeEach(() => {
  seedTestData(db);
});
```

**3. Not Cleaning Up Resources:**
```javascript
afterAll(() => {
  db.close();  // Always close connections!
});
```

**4. Testing Implementation Instead of Behavior:**
Test what the API returns, not how it computes the result internally.

**5. Forgetting Content-Type Header:**
```javascript
// WRONG - body won't be parsed
await app.request('/api/users', {
  method: 'POST',
  body: JSON.stringify({ name: 'Test' })
});

// CORRECT
await app.request('/api/users', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ name: 'Test' })
});
```