---
type: "THEORY"
title: "Comments System Architecture"
---


**Designing a Comments System**

Comments can be simple or complex. We'll build a threaded comment system with these features:

**Comment Features**

| Feature | Implementation |
|---------|---------------|
| Threading | Parent-child relationships |
| Nesting depth | Limit to 3 levels |
| Sorting | Newest, oldest, top (by likes) |
| Editing | Author can edit within time limit |
| Deletion | Soft delete, show "deleted" placeholder |
| Reactions | Like/unlike comments |

**Threading Approaches**

| Approach | Pros | Cons |
|----------|------|------|
| **Adjacency List** | Simple, flexible | Recursive queries |
| **Nested Sets** | Fast reads | Complex updates |
| **Materialized Path** | Good balance | Path string size |
| **Closure Table** | Flexible queries | Extra table |

We'll use **Adjacency List** (parent_id) with **depth limiting** for simplicity.

**Comment Tree Structure**

```
Post
├── Comment A (depth: 1)
│   ├── Reply A1 (depth: 2)
│   │   └── Reply A1a (depth: 3)  ← Max depth
│   └── Reply A2 (depth: 2)
├── Comment B (depth: 1)
└── Comment C (depth: 1)
    └── Reply C1 (depth: 2)
```

**Loading Strategy**

1. **Eager loading**: Load all comments (small posts)
2. **Lazy loading**: Load top-level, expand replies on demand
3. **Hybrid**: Load top N comments + counts for "load more"

**Performance Optimizations**

- Denormalize reply counts
- Index on (post_id, parent_id, created_at)
- Paginate top-level comments
- Cache hot comment trees

