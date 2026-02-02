---
type: "THEORY"
title: "When to Migrate"
---

**Stay with JSDoc when:**
- Your project is small (<10 files)
- You want zero build step
- Team is new to types
- You're adding types to legacy code gradually

**Migrate to TypeScript when:**
- JSDoc comments become too verbose
- You need advanced features (decorators, enums, namespaces)
- Team is comfortable with type concepts
- You're starting a new large project

**The Migration:**
1. Rename `.js` to `.ts` (Bun handles both!)
2. Convert JSDoc comments to type annotations
3. Fix any new errors TypeScript catches
4. Repeat file by file