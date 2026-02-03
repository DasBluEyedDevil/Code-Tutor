---
type: "KEY_POINT"
title: "Typed Client Generation with Kiota"
---

## Key Takeaways

- **Kiota generates strongly-typed API clients from OpenAPI specs** -- `kiota generate -l CSharp -d spec.json -c ApiClient` creates models and client code. No manual HTTP calls or JSON handling needed.

- **Regenerate when the API changes** -- run `kiota generate` again after endpoint additions or modifications. The generated client stays in sync with the API contract automatically.

- **Kiota supports many languages** -- CSharp, TypeScript, Python, Go, Java, and more. One OpenAPI spec can produce clients for all your consumer platforms.
