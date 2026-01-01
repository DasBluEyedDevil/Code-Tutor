---
type: "THEORY"
title: "Understanding Gradle Caching"
---


### Types of Caching

**1. Incremental Builds**
- Skips tasks when inputs/outputs unchanged
- Enabled by default
- Shows `UP-TO-DATE` in output

**2. Build Cache**
- Stores task outputs by input hash
- Reuses outputs even after `clean`
- Can be shared across machines (remote cache)

**3. Configuration Cache**
- Caches the build configuration itself
- Dramatically speeds up repeated builds
- Requires compatible plugins

---

