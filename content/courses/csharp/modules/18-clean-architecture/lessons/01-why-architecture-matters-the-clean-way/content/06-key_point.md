---
type: "KEY_POINT"
title: "Clean Architecture Principles"
---

## Key Takeaways

- **Dependencies point inward** -- outer layers (API, Infrastructure) depend on inner layers (Application, Domain). The Domain layer has zero external dependencies, making it the most stable and testable part.

- **Four layers: Domain, Application, Infrastructure, Presentation** -- Domain holds business rules. Application orchestrates use cases. Infrastructure implements data access. Presentation handles HTTP.

- **Architecture enables change** -- when you need to swap databases, add a new UI, or change a cloud provider, only the outer layers change. The core business logic remains untouched.
