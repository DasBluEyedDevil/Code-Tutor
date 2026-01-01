---
type: "KEY_POINT"
title: "spyOn() for Watching Calls"
---

**spyOn** watches function calls and optionally changes behavior:
```javascript
import { spyOn } from 'bun:test';

const spy = spyOn(console, 'log');
myFunction();
expect(spy).toHaveBeenCalledWith('expected message');
spy.mockRestore(); // Restore original
```

**mock()** creates a mock function:
```javascript
import { mock } from 'bun:test';

const mockFn = mock(() => 42);
expect(mockFn()).toBe(42);
```

Useful mock methods:
- `mockReturnValue(val)` - Always return val
- `mockResolvedValue(val)` - Return Promise.resolve(val)
- `mockRejectedValue(err)` - Return Promise.reject(err)
- `mockImplementation(fn)` - Custom logic

Verify calls:
- `toHaveBeenCalled()` - Was it called?
- `toHaveBeenCalledWith(args)` - With what?
- `toHaveBeenCalledTimes(n)` - How many times?