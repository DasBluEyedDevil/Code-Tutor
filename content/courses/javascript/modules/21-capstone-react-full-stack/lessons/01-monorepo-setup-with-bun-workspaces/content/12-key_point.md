---
type: "KEY_POINT"
title: "Monorepo Structure"
---

The full-stack monorepo has three packages that work together:

- **packages/shared** -- TypeScript types and validation schemas used by both frontend and backend
- **packages/api** -- The Hono backend from Module 20 (or a new one matching its patterns)
- **packages/web** -- The React frontend built with Vite

Key commands from the root:
- `bun install` -- installs dependencies for ALL packages at once
- `bun run dev` -- starts both api and web in parallel
- `bun run test` -- runs tests across all packages
