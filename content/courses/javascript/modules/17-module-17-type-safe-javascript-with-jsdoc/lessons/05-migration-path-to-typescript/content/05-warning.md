---
type: "WARNING"
title: "Migration Pitfalls"
---

**Common mistakes when migrating:**

1. **Converting everything at once** - Migrate file by file, test as you go

2. **Losing type safety during migration**
```typescript
// WRONG - 'any' defeats the purpose
const user: any = getUser();

// RIGHT - Use unknown or proper types
const user: unknown = getUser();
```

3. **Forgetting to update imports** - TypeScript may need explicit `.js` extensions in ESM

4. **Not testing after each file** - Run `bun test` after converting each file