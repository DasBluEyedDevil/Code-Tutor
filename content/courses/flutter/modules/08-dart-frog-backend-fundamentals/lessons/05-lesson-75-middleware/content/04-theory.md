---
type: "THEORY"
title: "Middleware Scope"
---


Middleware applies to all routes in its folder AND all subfolders. This gives you precise control over which routes use which middleware.

**Folder Structure Example**:
```
routes/
├── _middleware.dart       ← Applies to ALL routes
├── index.dart             ← Uses root middleware
├── public/
│   └── health.dart        ← Uses root middleware only
└── api/
    ├── _middleware.dart   ← Applies to /api/* routes
    ├── users.dart         ← Uses BOTH middlewares
    └── admin/
        ├── _middleware.dart  ← Applies to /api/admin/* routes
        └── settings.dart     ← Uses ALL THREE middlewares
```

**Middleware Execution Order**:
Middleware chains from outer to inner:
1. `routes/_middleware.dart` runs first
2. `routes/api/_middleware.dart` runs second
3. `routes/api/admin/_middleware.dart` runs third
4. Route handler executes
5. Then responses flow back through in reverse order

**Practical Example**:
```
Request to /api/admin/settings:

[Root Middleware: Logging]
    ↓
[API Middleware: Auth Check]
    ↓
[Admin Middleware: Admin Role Check]
    ↓
[settings.dart: Route Handler]
    ↓
[Responses flow back up the chain]
```

This layered approach lets you:
- Log ALL requests at the root level
- Require authentication for ALL /api/* routes
- Require admin role for ONLY /api/admin/* routes

