---
type: "KEY_POINT"
title: "Key Takeaways"
---

**CI/CD automates building, testing, and deployment**, reducing human error and enabling rapid iteration. Every commit should trigger automated tests; successful builds on main can auto-deploy to staging.

**Use matrix builds to test all platforms**—Android, iOS simulator, JVM, and JS should all pass before merging. GitHub Actions matrix strategy runs jobs in parallel for fast feedback.

**Separate build and deployment pipelines**: CI builds and tests on every commit, CD deploys only from tagged releases or main branch. This separation prevents accidental production deployments.
