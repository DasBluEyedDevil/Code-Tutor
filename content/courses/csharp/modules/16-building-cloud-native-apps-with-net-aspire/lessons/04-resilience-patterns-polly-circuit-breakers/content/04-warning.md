---
type: "WARNING"
title: "Common Pitfalls"
---

## Resilience Mistakes to Avoid

**Strategy Order Matters**: `AddCircuitBreaker().AddRetry()` means the circuit evaluates BEFORE retries - each attempt counts as a separate call! Usually use `AddRetry().AddCircuitBreaker().AddTimeout()` so retries happen within circuit evaluation.

**Too Aggressive Retries**: 10 retries with 100ms delay = hammering a dying service for 1 second. Use exponential backoff, jitter, and reasonable limits (3-5 max). Give services time to recover!

**Circuit Breaker Too Sensitive**: `MinimumThroughput=1` means ONE failure opens the circuit! Set MinimumThroughput to 5-10+ for statistical significance. Brief glitches shouldn't trigger circuit opens.

**Forgetting Status Code Handling**: Default retry only handles network exceptions. Server returning 503 (Service Unavailable) won't retry! Use `ShouldHandle` with `PredicateBuilder` to include specific HTTP status codes.

**Timeout vs Total Timeout**: Polly's timeout is PER ATTEMPT. With 3 retries and 10s timeout, total could be 30+ seconds! Consider using both per-attempt and total pipeline timeouts.

**Hiding Failures**: Resilience makes failures invisible if you don't log. Always use `OnRetry`, `OnOpened` callbacks to track when resilience activates - these are important signals!