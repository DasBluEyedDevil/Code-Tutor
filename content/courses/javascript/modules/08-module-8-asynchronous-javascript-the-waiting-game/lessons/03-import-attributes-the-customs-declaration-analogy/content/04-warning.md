---
type: "WARNING"
title: "Common Pitfalls"
---

1. **Forgetting the `with` keyword**: It's `with { type: 'json' }`, not `as json` or `type: 'json'`

2. **Wrong type value**: The type must match the file content. `{ type: 'json' }` for JSON, `{ type: 'css' }` for CSS.

3. **Dynamic imports use different syntax**:
```javascript
// Static import
import data from './data.json' with { type: 'json' };

// Dynamic import - note the nested object
const data = await import('./data.json', { with: { type: 'json' } });
```