---
type: "WARNING"
title: "Common Workspace Mistakes"
---

**Don't:**
1. Use relative imports across packages
   - Bad: `import from '../../shared/src'`
   - Good: `import from '@app/shared'`

2. Forget to export from shared/src/index.ts
   - Always create an index.ts that exports public API

3. Publish shared package to npm
   - Keep it private for internal use
   - Set `"private": true` in shared/package.json

4. Mix CommonJS and ESM
   - Keep all packages as `"type": "module"`

5. Forget to update paths in tsconfig
   - Keep path aliases in sync with actual structure