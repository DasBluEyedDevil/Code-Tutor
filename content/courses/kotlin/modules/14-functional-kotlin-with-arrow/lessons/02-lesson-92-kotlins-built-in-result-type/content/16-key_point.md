---
type: "KEY_POINT"
title: "Key Takeaways"
---

**`Result<T>` represents success or failure** without exceptions. It forces explicit error handling at call sites: `result.getOrNull()`, `result.onSuccess {}`, `result.fold({}, {})`. This makes error paths visible in types.

**Use Result for expected failures** (network errors, validation failures) where callers should handle errors explicitly. Reserve exceptions for unexpected, unrecoverable errors (out of memory, programmer mistakes).

**Result integrates with suspend functions via `runCatching`**: wrap throwing code to capture exceptions as Result<T>. This bridges exception-based APIs into functional error handling.
