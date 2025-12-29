# JavaScript Course Modernization Design

**Date:** 2025-12-29
**Status:** Approved
**Scope:** Modules 10, 13, 14, 15

## Summary

Modernize the JavaScript & TypeScript course by replacing the legacy Express/Node.js/Vitest stack with a modern Bun/Hono/bun:test stack, while preserving legacy knowledge through comparison sidebars.

## Decisions

| Area | Primary (Modern) | Legacy Sidebar |
|------|------------------|----------------|
| Runtime | Bun | Node.js |
| Framework | Hono | Express |
| Testing | bun:test | Vitest |

## Schema Changes

### New Content Section Type: `LEGACY_COMPARISON`

```json
{
  "type": "LEGACY_COMPARISON",
  "title": "Node.js/Express Equivalent",
  "legacy": "express",
  "content": "In Express, you would write this as...",
  "code": "const app = express();\napp.get('/', (req, res) => res.send('Hello'));",
  "language": "javascript"
}
```

**Fields:**
- `legacy`: Identifies the legacy tech (`"express"`, `"node"`, `"vitest"`)
- UI renders as collapsible panels
- Future: Students can toggle legacy comparisons on/off in settings

### Module Renaming

- Module 10: "Building for the Server - Node.js & Express" → "Building for the Server with Bun & Hono"
- Module 15: "Testing JavaScript with Vitest" → "Testing JavaScript with Bun"

## Module 10: Bun & Hono

| Lesson | Title | Key Changes |
|--------|-------|-------------|
| 10.1 | What is Bun? (The Supercharged Kitchen) | Bun intro, built-in TS, speed benefits. Legacy sidebar: Node.js |
| 10.2 | Your First Hono Server | `Bun.serve()` + Hono routing. Legacy sidebar: Express |
| 10.3 | Routes and HTTP Methods | Hono's `app.get()`, `app.post()`, path params |
| 10.4 | Middleware in Hono | Hono middleware, built-in helpers (`cors()`, `logger()`) |
| 10.5 | Error Handling | Hono's `app.onError()`, `HTTPException` |

### Example Code Transformation

**Modern (Hono + Bun):**
```typescript
import { Hono } from 'hono'

const app = new Hono()
app.get('/', (c) => c.text('Hello World'))

export default app
```

**Legacy Sidebar (Express + Node):**
```javascript
const express = require('express')
const app = express()
app.get('/', (req, res) => res.send('Hello World'))
app.listen(3000)
```

## Module 13: Full-Stack Integration

| Lesson | Title | Key Changes |
|--------|-------|-------------|
| 13.1 | Full-Stack Architecture Overview | Updated diagram showing Bun/Hono backend |
| 13.2 | API Design with Hono | RPC-style routes, `zod-validator` |
| 13.3 | Connecting React to Hono | Fetch patterns, `hono/cors`, Hono Client (`hc`) |
| 13.4 | Complete Full-Stack Todo App | React + Hono + Prisma |

**Key Feature:** Hono Client (`hc`) for type-safe API calls from React.

## Module 14: Deployment

| Lesson | Title | Key Changes |
|--------|-------|-------------|
| 14.1 | Git & Version Control | No change |
| 14.2 | Deploying Hono | Multi-target: Bun on Render, Cloudflare Workers, Deno Deploy |
| 14.3 | Deploying React to Vercel | No change |
| 14.4 | Environment Variables | Bun's `.env` handling |
| 14.5 | CI/CD with GitHub Actions | `oven-sh/setup-bun` action |

**Key Feature:** Hono's portability - one codebase deploys to Node, Bun, Deno, or Edge.

## Module 15: Testing with Bun

| Lesson | Title | Key Changes |
|--------|-------|-------------|
| 15.1 | Why Test? | No change |
| 15.2 | Your First Test with Bun | Zero config `bun test`. Legacy sidebar: Vitest |
| 15.3 | Writing Good Tests (AAA) | No change |
| 15.4 | Mocking with Bun | `mock.module()`, `spyOn()`. Legacy sidebar: `vi.mock()` |
| 15.5 | Testing Async Code | Bun's fake timers. Legacy sidebar: `vi.useFakeTimers()` |
| 15.6 | Test Organization | `--preload` for setup files |
| 15.7 | Coverage & CI | `bun test --coverage`, GitHub Actions |
| 15.8 | Integration Testing | Testing Hono with `app.request()` |

**Key Simplification:** No config files needed. Just `bun test`.

## Implementation Checklist

### Phase 1: Schema & Infrastructure
- [ ] Add `LEGACY_COMPARISON` content section type to course schema
- [ ] Update WPF UI to render legacy sidebars (collapsible panels)

### Phase 2: Module 10 Rewrite (Bun & Hono)
- [ ] Rewrite 10.1: What is Bun?
- [ ] Rewrite 10.2: Your First Hono Server
- [ ] Rewrite 10.3: Routes and HTTP Methods
- [ ] Rewrite 10.4: Middleware in Hono
- [ ] Rewrite 10.5: Error Handling
- [ ] Add Express legacy sidebars to each lesson

### Phase 3: Module 13 & 14 Updates
- [ ] Update 13.1-13.4: Replace Express with Hono
- [ ] Update 14.2: Multi-target deployment (Render, Cloudflare, Deno)
- [ ] Update 14.5: Bun-based GitHub Actions workflow

### Phase 4: Module 15 Rewrite (Bun Test)
- [ ] Rewrite 15.2-15.8: Replace Vitest with `bun:test`
- [ ] Add Vitest legacy sidebars where APIs differ

### Phase 5: Validation
- [ ] Test all code examples run with Bun
- [ ] Verify challenge test cases work
- [ ] Review legacy sidebars for accuracy

## Estimated Scope

- ~40 lessons affected
- ~15 with significant rewrites
- 4 modules updated
