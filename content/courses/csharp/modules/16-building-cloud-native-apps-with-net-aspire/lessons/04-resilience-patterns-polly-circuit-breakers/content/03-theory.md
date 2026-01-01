---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`AddStandardResilienceHandler()`**: Aspire's one-liner for production-ready resilience. Adds retry, circuit breaker, timeout, and rate limiting with sensible defaults.

**`AddResilienceHandler(name, builder => {...})`**: Custom resilience for specific HttpClient. Chain .AddRetry(), .AddCircuitBreaker(), .AddTimeout() in the builder.

**`BackoffType.Exponential`**: Each retry waits longer: 500ms, 1s, 2s, 4s... Prevents hammering a struggling service. Add UseJitter=true to randomize.

**`FailureRatio + MinimumThroughput`**: Circuit breaker opens when FailureRatio (e.g., 50%) of requests fail, but only after MinimumThroughput requests. Prevents opening on 1 failure.

**`ResiliencePipelineBuilder`**: Polly's fluent builder for composing strategies. Build() creates an immutable pipeline. Execute() runs code through the pipeline.

**`OnRetry, OnOpened, OnClosed callbacks`**: Hooks for logging, metrics, alerts. Know when resilience kicks in! Returns ValueTask for async support.