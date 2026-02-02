---
type: "THEORY"
title: "Nested Routes"
---


Your folder structure creates nested URL paths. Here's how complex APIs map to folders:



```text
routes/
├── index.dart                    -> GET /
├── users/
│   ├── index.dart                -> GET /users
│   └── [id].dart                 -> GET /users/:id
├── products/
│   ├── index.dart                -> GET /products
│   └── [id]/
│       ├── index.dart            -> GET /products/:id
│       └── reviews.dart          -> GET /products/:id/reviews
└── api/
    ├── v1/
    │   └── status.dart           -> GET /api/v1/status
    └── health.dart               -> GET /api/health
```
