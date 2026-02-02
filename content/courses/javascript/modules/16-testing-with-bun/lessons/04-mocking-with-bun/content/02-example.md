---
type: "EXAMPLE"
title: "mock() and mock.module()"
---

See the code example above demonstrating mock() and mock.module().

```javascript
import { describe, it, expect, mock, spyOn } from 'bun:test';

// Create a mock function
const mockFetch = mock(() => Promise.resolve({ name: 'Alice' }));

// Mock an entire module
mock.module('./api', () => ({
  fetchUser: mockFetch
}));

import { getUserGreeting } from './greeting';

describe('getUserGreeting', () => {
  it('greets the user by name', async () => {
    // Setup mock return value
    mockFetch.mockResolvedValue({ name: 'Alice' });

    const greeting = await getUserGreeting(1);

    expect(greeting).toBe('Hello, Alice!');
    expect(mockFetch).toHaveBeenCalledWith(1);
  });

  it('handles API errors gracefully', async () => {
    mockFetch.mockRejectedValue(new Error('Network error'));

    const greeting = await getUserGreeting(1);

    expect(greeting).toBe('Hello, Guest!');
  });
});
```
