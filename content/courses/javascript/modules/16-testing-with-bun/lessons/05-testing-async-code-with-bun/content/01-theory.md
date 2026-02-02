---
type: "THEORY"
title: "Async Testing Patterns"
---

Async code needs special handling in tests. Bun's test runner handles async naturally:

**Return a Promise:**
```javascript
it('fetches data', () => {
  return fetchData().then(data => {
    expect(data).toBeDefined();
  });
});
```

**Use async/await (preferred):**
```javascript
it('fetches data', async () => {
  const data = await fetchData();
  expect(data).toBeDefined();
});
```

**Key rule:** Always await or return async operations. Forgotten awaits cause tests to pass incorrectly because assertions run after the test completes.