---
type: "KEY_POINT"
title: "Contract Testing Best Practices"
---


**1. Contracts Are Shared Artifacts**

Store contracts in a shared location accessible to both frontend and backend.

**2. Consumer-Driven Development**

Let consumers define what they need. Frontend team writes contract expectations, backend team implements to satisfy contracts.

**3. Version Your Contracts**

Treat contracts like any other code with semantic versioning and changelogs.

**4. Test Both Happy and Error Paths**

Contracts should cover success responses, client errors, server errors, and edge cases.

**5. Run Contract Tests in CI**

Every PR must pass contract tests.

