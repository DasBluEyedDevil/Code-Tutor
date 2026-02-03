---
type: "KEY_POINT"
title: "Project Setup Checklist"
---

Before writing any route handlers, ensure these foundations are in place:

1. **Bun + TypeScript** initialized with strict mode enabled
2. **Hono** installed as the web framework (lightweight, fast, type-safe)
3. **Prisma** initialized with your database provider (SQLite for dev, PostgreSQL for production)
4. **Zod** ready for input validation on every endpoint
5. **Project structure** organized: `src/routes/`, `src/middleware/`, `src/schemas/`, `src/lib/`

A solid project setup prevents rework later. Every capstone project starts here.
