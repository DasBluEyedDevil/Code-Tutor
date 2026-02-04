---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Real KMP applications balance purity and pragmatism**. Clean Architecture principles guide structure, but pragmatic compromises (combining layers for simple features) are acceptable when benefits outweigh costs.

**Start with simpler architecture and refactor as complexity grows**—don't implement every pattern on day one. Begin with ViewModels + repositories, add use cases when business logic becomes complex.

**Architecture is validated through tests and refactoring ease**. If adding a feature requires touching many unrelated files, or if unit testing requires complex setup, your architecture needs adjustment.
