---
type: "THEORY"
title: "How Top-Level Await Works"
---

**The module loading story:**

1. Your module starts executing
2. Hits a top-level `await`
3. Module loading PAUSES (but doesn't block the event loop)
4. Other modules can still load in parallel
5. Once await resolves, your module continues
6. When all awaits complete, module is 'ready'
7. Modules that import yours wait until you're ready

**This means:**
- Circular dependencies with top-level await can deadlock
- Import order matters more
- Errors in top-level await crash module loading

**Best practice:** Keep top-level await for true initialization, not ongoing work.