---
type: "WARNING"
title: "Future TypeScript Changes May Affect JSDoc"
---

TypeScript evolves rapidly, and future versions may change how JSDoc types are handled. The TypeScript team is actively rewriting the compiler in Go (targeting a major performance release in 2025-2026), which could introduce changes to JSDoc support.

**Stay prepared:**

1. **Pin your TypeScript version** - Don't auto-upgrade in CI; test new versions first
2. **Monitor release notes** - Each TypeScript release documents breaking changes
3. **Run `tsc --checkJs` after upgrades** - Catch any new errors early
4. **Keep JSDoc annotations standard** - Stick to well-supported tags (`@param`, `@returns`, `@type`, `@typedef`, `@template`) for maximum forward compatibility

**If a future version changes JSDoc behavior:**
- Check the [TypeScript release notes](https://devblogs.microsoft.com/typescript/) for migration guidance
- Update or remove any deprecated JSDoc tags
- Consider migrating complex types to TypeScript if JSDoc support narrows