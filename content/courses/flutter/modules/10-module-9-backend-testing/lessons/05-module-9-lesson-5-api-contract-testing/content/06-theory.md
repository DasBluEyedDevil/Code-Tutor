---
type: "THEORY"
title: "Version Compatibility Testing"
---


As your API evolves, you must ensure that different versions of clients and servers can communicate correctly. This is **version compatibility testing**.

**Why Version Compatibility Matters:**

In production, you often have:
- **Multiple client versions**: Users on old app versions
- **Rolling deployments**: Old and new servers running simultaneously
- **Gradual rollouts**: New features enabled for subset of users

**Testing Approach:**

1. **Define compatibility requirements**: Which client/server combinations must work?
2. **Create version-specific test fixtures**: Request/response samples for each version
3. **Run matrix tests in CI**: Test all supported combinations
4. **Document breaking changes**: Maintain changelog of contract changes

