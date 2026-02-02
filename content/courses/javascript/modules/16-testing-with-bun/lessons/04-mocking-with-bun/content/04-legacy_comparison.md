---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

Vitest uses vi.fn() and vi.mock() instead of Bun's mock() and mock.module(). The API is similar but uses a different namespace.

```javascript
// Vitest version
import { vi, describe, it, expect } from 'vitest';

// vi.fn() instead of mock()
const mockFetch = vi.fn();

// vi.mock() instead of mock.module()
vi.mock('./api', () => ({
  fetchUser: mockFetch
}));

// vi.spyOn() instead of spyOn()
const spy = vi.spyOn(console, 'log');

// Everything else works the same!
```
