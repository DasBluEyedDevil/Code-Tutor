---
type: "WARNING"
title: "Stage 3 = Not Yet Standard"
---

Decorators are Stage 3 in TC39, meaning:

- Syntax is stable but not yet ES standard
- Bun and TypeScript support them with configuration
- May change before final standardization

**To use in Bun today:**
```bash
# No config needed! Bun supports decorators out of the box
bun run decorated.ts
```

**To use in TypeScript:**
```json
// tsconfig.json
{
  "compilerOptions": {
    "experimentalDecorators": true
  }
}
```

Note: TypeScript decorators use an older spec. Stage 3 decorators have slightly different semantics.