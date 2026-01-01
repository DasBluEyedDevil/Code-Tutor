---
type: "THEORY"
title: "Running Commands in Workspaces"
---

Key Bun workspace commands:

**Run in specific package:**
```bash
bun run --filter=api dev      # Run dev script in api package
bun run --filter=web build    # Run build in web package
```

**Run in all packages:**
```bash
bun run --filter=./packages/* test    # Run test in all packages
```

**Install in specific package:**
```bash
bun add express --filter=api          # Add to api only
bun add -D @types/react --filter=web  # Add to web only
bun add axios                         # Add to root
```

**Use workspace packages:**
Because `@app/shared` is in workspaces, both `api` and `web` can import from it:
```typescript
// In api/src/something.ts
import { UserType } from '@app/shared';

// In web/src/something.tsx
import { UserType } from '@app/shared';
```