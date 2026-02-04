---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Railway-oriented programming models success/failure as parallel tracks**—operations stay on success track when they succeed, switch to failure track on errors. Once on failure track, subsequent operations are skipped.

**Use `map` for success-only transformations, `flatMap` for operations that can fail**. Compose these operators to build pipelines where errors short-circuit automatically without explicit null/error checks.

**Arrow's Raise DSL brings railway orientation to Kotlin**: `raise(error)` jumps to failure track, eliminating explicit Either wrapping. Write sequential-looking code that compiles to functional error handling.
