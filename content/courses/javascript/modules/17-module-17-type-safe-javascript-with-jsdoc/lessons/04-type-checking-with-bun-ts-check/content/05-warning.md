---
type: "WARNING"
title: "TypeScript 7.0 Changes"
---

**TypeScript 7.0 (December 2025) changes JSDoc handling:**

1. **Stricter checking** - Some relaxed rules in JavaScript have been tightened
2. **Removed tags** - `@enum` and `@constructor` are no longer recognized
3. **More errors** - Codebases may see new errors that need fixing

**If upgrading:**
- Run `tsc --checkJs` to find new errors
- Update or remove unsupported JSDoc tags
- Consider migrating complex types to TypeScript