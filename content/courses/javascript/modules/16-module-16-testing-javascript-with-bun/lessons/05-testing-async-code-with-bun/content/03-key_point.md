---
type: "KEY_POINT"
title: "Testing Promise States"
---

Test all promise outcomes with Bun:

```javascript
import { describe, it, expect } from 'bun:test';

describe('fetchUser', () => {
  it('resolves with user data', async () => {
    const user = await fetchUser(1);
    expect(user.name).toBe('Alice');
  });

  it('rejects for invalid id', async () => {
    expect(fetchUser(-1)).rejects.toThrow('Invalid ID');
  });

  it('rejects with specific error type', async () => {
    expect(fetchUser(999))
      .rejects
      .toBeInstanceOf(NotFoundError);
  });
});
```

Useful matchers:
- `resolves.toBe()` - Unwraps resolved value
- `rejects.toThrow()` - Checks rejection
- `Bun.sleep(ms)` - Async sleep for timing tests