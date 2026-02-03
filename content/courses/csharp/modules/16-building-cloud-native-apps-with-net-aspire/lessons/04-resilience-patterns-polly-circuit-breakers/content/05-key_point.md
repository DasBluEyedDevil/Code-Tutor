---
type: "KEY_POINT"
title: "Resilience with Polly"
---

## Key Takeaways

- **`AddStandardResilienceHandler()` adds production-ready defaults** -- retry with exponential backoff, circuit breaker, timeout, and rate limiting. One line of code for robust HTTP communication.

- **Circuit breakers prevent cascading failures** -- when a downstream service fails repeatedly, the circuit opens and rejects requests immediately instead of waiting for timeouts. It automatically tries again after a cooldown period.

- **Always use jitter with retries** -- `UseJitter = true` randomizes retry delays so that many clients do not all retry at the exact same moment, which would overwhelm the recovering service.
