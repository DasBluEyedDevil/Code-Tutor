---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Returning in finally**: A `return` statement in finally overrides returns in try/catch! This is confusing - avoid returning from finally blocks.

**Finally for business logic**: Finally is for CLEANUP only (close files, release locks). Don't put important app logic there - use regular code flow.

**Forgetting : base(message)**: Custom exceptions without `: base(message)` lose the error message! Always pass it to the base constructor.

**Prefer 'using' over finally for resources**: For IDisposable objects (files, connections), the `using` statement is cleaner than try/finally and guarantees cleanup.

**Throwing in finally**: If you throw an exception in finally while another exception is propagating, the original exception is LOST! Never throw in finally blocks.