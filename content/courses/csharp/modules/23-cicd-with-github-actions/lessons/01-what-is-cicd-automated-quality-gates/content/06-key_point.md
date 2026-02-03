---
type: "KEY_POINT"
title: "CI/CD Pipeline Concepts"
---

## Key Takeaways

- **CI (Continuous Integration) catches bugs early** -- every push triggers automated builds and tests. Broken code is detected within minutes, not days. The main branch always stays deployable.

- **CD (Continuous Deployment) automates releases** -- after tests pass, code is automatically deployed to staging or production. Manual deployment steps are error-prone and slow.

- **Workflows live in `.github/workflows/`** -- YAML files define triggers, jobs, and steps. They are version-controlled alongside your code, so pipeline changes go through the same review process as application changes.
