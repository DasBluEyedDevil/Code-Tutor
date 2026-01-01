---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

Vitest uses vi.useFakeTimers() and vi.advanceTimersByTime() for timer control. Bun uses setSystemTime() instead.

```javascript
// Vitest version
import { vi, describe, it, expect, beforeEach, afterEach } from 'vitest';

beforeEach(() => {
  vi.useFakeTimers();  // Vitest fake timers
});

afterEach(() => {
  vi.restoreAllMocks();
});

it('delays execution', () => {
  const callback = vi.fn();
  delayedGreeting('Alice', callback);
  
  vi.advanceTimersByTime(1000);  // Vitest time advance
  
  expect(callback).toHaveBeenCalled();
});
```
