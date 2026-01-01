---
type: "KEY_POINT"
title: "Integration Test Best Practices"
---

**Test HTTP semantics:**
```javascript
// Status codes
expect(res.status).toBe(201);  // Created
expect(res.status).toBe(404);  // Not found

// Headers
expect(res.headers.get('Content-Type')).toContain('application/json');

// Response body
const data = await res.json();
expect(data.id).toBeDefined();
```

**Test full flows:**
```javascript
it('full todo lifecycle', async () => {
  // Create
  const createRes = await app.request('/todos', {
    method: 'POST',
    body: JSON.stringify({ text: 'Test' })
  });
  const { id } = await createRes.json();
  
  // Update
  await app.request(`/todos/${id}`, { method: 'PATCH' });
  
  // Verify
  const getRes = await app.request('/todos');
  const todos = await getRes.json();
  expect(todos[0].done).toBe(true);
});
```

**Isolate tests:** Create fresh app instance in beforeEach.