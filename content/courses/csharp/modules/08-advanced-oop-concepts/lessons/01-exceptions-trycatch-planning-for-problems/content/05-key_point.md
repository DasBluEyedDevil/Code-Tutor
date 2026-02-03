---
type: "KEY_POINT"
title: "Exception Handling Essentials"
---

## Key Takeaways

- **Wrap risky operations in `try-catch`** -- file I/O, network calls, and user input parsing can fail. Catch specific exceptions (`FileNotFoundException`, `FormatException`) before generic `Exception`.

- **Exception filters with `when` preserve the stack trace** -- `catch (HttpRequestException ex) when (ex.StatusCode == 404)` only catches 404 errors. The filter runs before stack unwinding, keeping diagnostic information intact.

- **Use `ex.Message` and `ex.StackTrace` for debugging** -- always log the full exception details. In production, log to a structured logging system; in development, the stack trace tells you exactly where the error occurred.
