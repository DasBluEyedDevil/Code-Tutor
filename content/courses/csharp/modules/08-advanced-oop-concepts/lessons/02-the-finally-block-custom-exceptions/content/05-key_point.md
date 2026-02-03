---
type: "KEY_POINT"
title: "Finally Blocks and Custom Exceptions"
---

## Key Takeaways

- **`finally` always runs** -- whether the try block succeeds or throws, the finally block executes. Use it for cleanup: closing files, releasing database connections, disposing resources.

- **Prefer `using` over manual `finally`** -- `using var file = File.OpenRead(path);` automatically disposes the file when the scope ends. It is cleaner and less error-prone than try/finally.

- **Custom exceptions should extend `Exception`** -- name them ending with `Exception` (e.g., `InsufficientFundsException`). Include a descriptive message via `: base(message)` in the constructor.
