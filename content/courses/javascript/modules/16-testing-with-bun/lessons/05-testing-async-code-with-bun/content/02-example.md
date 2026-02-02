---
type: "EXAMPLE"
title: "Controlling Time with setSystemTime()"
---

Bun provides built-in fake timers through setSystemTime().

```javascript
import { describe, it, expect, beforeEach, afterEach, mock, setSystemTime } from 'bun:test';

function delayedGreeting(name, callback) {
  setTimeout(() => callback(`Hello, ${name}!`), 1000);
}

describe('delayedGreeting', () => {
  beforeEach(() => {
    setSystemTime(new Date('2024-01-01'));  // Take control of time
  });

  afterEach(() => {
    setSystemTime();  // Restore real time
  });

  it('calls callback after 1 second', async () => {
    const callback = mock(() => {});

    delayedGreeting('Alice', callback);
    
    expect(callback).not.toHaveBeenCalled();  // Not yet!
    
    // Use Bun.sleep or advance time manually
    await Bun.sleep(1000);
    
    expect(callback).toHaveBeenCalledWith('Hello, Alice!');
  });
});
```
