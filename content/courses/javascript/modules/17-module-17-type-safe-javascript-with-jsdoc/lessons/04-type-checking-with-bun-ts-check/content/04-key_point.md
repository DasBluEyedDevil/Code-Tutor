---
type: "KEY_POINT"
title: "Ignoring Errors"
---

Sometimes you know better than the type checker:

```javascript
// @ts-ignore - Ignore the next line only
// @ts-expect-error - Same, but fails if no error exists (preferred)

const data = JSON.parse(input);
// @ts-expect-error - API returns wrong type, we handle it
data.items = data.items || [];
```

**Use sparingly!** Every ignore is technical debt.