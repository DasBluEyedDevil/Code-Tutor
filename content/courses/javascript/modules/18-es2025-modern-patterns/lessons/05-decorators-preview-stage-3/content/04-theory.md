---
type: "THEORY"
title: "Decorator Kinds"
---

The Stage 3 decorators proposal supports:

| Kind | Target | Example Use |
|------|--------|-------------|
| Class | `class Foo {}` | Singleton, frozen, service registration |
| Method | `method() {}` | Logging, timing, validation |
| Getter/Setter | `get foo() {}` | Lazy loading, caching |
| Field | `x = 1;` | Observable, computed |
| Accessor | `accessor x = 1;` | Reactive fields |

**The `context` object includes:**
- `kind`: 'class', 'method', 'getter', 'setter', 'field', 'accessor'
- `name`: The member name
- `access`: { get, set } for fields/accessors
- `isStatic`: boolean
- `isPrivate`: boolean
- `addInitializer`: Add initialization code