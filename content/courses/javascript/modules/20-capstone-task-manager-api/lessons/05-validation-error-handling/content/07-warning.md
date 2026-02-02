---
type: "WARNING"
title: "Common Pitfalls with Error Handling"
---

**1. Async Errors Not Being Caught**
```typescript
// WRONG - Errors in async handlers won't be caught
tasks.post('/', async (c) => {
  const data = c.req.valid('json'); // If validation fails, error isn't caught!
  // ...
});

// RIGHT - Let the error handler middleware catch it
tasks.post(
  '/',
  zValidator('json', schema),
  async (c) => {
    // zValidator handles validation, error handler catches anything else
    const data = c.req.valid('json');
  }
);
```

**2. Sending Response After Error**
```typescript
// WRONG - Calling c.json() twice
tasks.post('/', async (c) => {
  try {
    // ...
  } catch (err) {
    c.json({ error: 'Something failed' }, 500);
  }
  // This will try to send response again!
  return c.json({ success: true });
});

// RIGHT - Return or throw
tasks.post('/', async (c) => {
  try {
    return c.json({ success: true });
  } catch (err) {
    throw err; // Let error handler deal with it
  }
});
```

**3. Not Logging Enough Context**
```typescript
// WRONG - Just logging the message
console.error('Task update failed');

// RIGHT - Log context for debugging
console.error('Task update failed', {
  userId,
  taskId,
  timestamp: new Date().toISOString(),
  error: err.message,
});
```

**4. Exposing Internal Details**
```typescript
// WRONG - Leaking implementation details
throw new Error(`Failed to update task in Prisma: ${err.message}`);

// RIGHT - User-friendly messages
throw new ServerError('Could not save your changes. Please try again.');
```