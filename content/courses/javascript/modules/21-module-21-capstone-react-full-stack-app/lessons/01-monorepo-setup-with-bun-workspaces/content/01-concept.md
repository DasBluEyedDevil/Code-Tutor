---
type: "CONCEPT"
title: "Monorepo Architecture"
---

A monorepo is a single git repository that contains multiple related projects. This is ideal for full-stack applications where you want:

**Benefits:**
- Shared code between frontend and backend
- Single dependency management
- Easier version coordination
- Simpler refactoring across packages
- Unified tooling and scripts

**Our Structure:**
```
full-stack-app/
├── packages/
│   ├── api/        # Backend: Hono REST API
│   ├── web/        # Frontend: React application
│   └── shared/     # Shared: TypeScript types & utilities
├── package.json    # Root workspace configuration
└── tsconfig.json   # Shared TypeScript config
```

**Package Relationships:**
- `shared` exports types used by both `api` and `web`
- `web` calls APIs provided by `api`
- Both use shared validation schemas and interfaces

**Why Bun workspaces?**
- Built-in monorepo support with `bun install`
- Shares dependencies across packages
- Single lockfile for consistency