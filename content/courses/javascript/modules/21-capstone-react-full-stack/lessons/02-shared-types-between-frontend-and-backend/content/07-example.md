---
type: "EXAMPLE"
title: "Create Directory Structure"
---

Set up the complete shared package structure:

```bash
# From packages/shared directory
mkdir -p src/{types,schemas}

# Create the files
touch src/index.ts
touch src/types/user.ts
touch src/types/task.ts
touch src/types/api.ts
touch src/schemas/validation.ts

# Structure:
# packages/shared/
# ├── src/
# │   ├── index.ts
# │   ├── types/
# │   │   ├── user.ts
# │   │   ├── task.ts
# │   │   └── api.ts
# │   └── schemas/
# │       └── validation.ts
# ├── package.json
# └── tsconfig.json
```
