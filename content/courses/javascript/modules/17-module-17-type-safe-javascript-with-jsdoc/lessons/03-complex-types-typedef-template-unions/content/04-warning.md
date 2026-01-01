---
type: "WARNING"
title: "When JSDoc Gets Too Complex"
---

If you find yourself writing types like this:

```javascript
/**
 * @typedef {Object} DeepNestedType
 * @property {{ items: Array<{ id: number, data: { nested: { value: string }[] } }> }} response
 */
```

**It's time to consider TypeScript.** JSDoc is great for adding types to JavaScript, but deeply nested generic types become unreadable. That's your signal to migrate.