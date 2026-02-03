---
type: "WARNING"
title: "Common Pitfalls"
---

## Clean Architecture Pitfalls

**Over-Engineering Small Projects**: Clean Architecture adds significant upfront complexity. For simple CRUD APIs with 3-5 entities, the four-layer structure is overkill. You'll spend more time creating folders, interfaces, and wiring DI than writing actual business logic. Start simple, refactor toward Clean Architecture when complexity demands it.

**Architecture Astronaut Syndrome**: Don't architect for problems you don't have. Building abstractions for "what if we switch databases" when you've only ever used PostgreSQL wastes time. Add abstractions when you have a concrete reason, not hypothetical future flexibility.

**Premature Pattern Adoption**: Introducing CQRS, MediatR, Domain Events, and the full Clean Architecture stack on day one overwhelms teams and slows development. Start with the layers, add patterns like CQRS only when your read and write models genuinely diverge.

**Not All Apps Need Clean Architecture**: CLI tools, background workers, prototypes, and internal scripts don't benefit from four layers. Use the right level of architecture for your project's lifetime and complexity. A weekend project doesn't need the same structure as a five-year enterprise system.
