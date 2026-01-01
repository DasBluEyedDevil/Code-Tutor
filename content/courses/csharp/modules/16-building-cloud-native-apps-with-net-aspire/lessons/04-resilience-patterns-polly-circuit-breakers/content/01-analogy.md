---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're calling a friend who's not answering. What do you do?

BAD APPROACH: Keep calling every second for an hour. You waste time, annoy them, and their phone overheats!

GOOD APPROACH (Resilience Patterns):

1. RETRY: Try 3 times with pauses between
   Call... wait 1 sec... call... wait 2 sec... call

2. CIRCUIT BREAKER: Stop trying temporarily
   After 5 failures: 'Phone is probably dead, wait 30 seconds'
   Like an electrical circuit breaker - prevents damage!

3. TIMEOUT: Don't wait forever
   'If no answer in 10 seconds, hang up'

4. FALLBACK: Have a backup plan
   'If call fails, send a text instead'

5. BULKHEAD: Limit concurrent attempts
   'Only make 5 calls at once, queue the rest'

POLLY: .NET library for all these patterns!
- Fluent API to define policies
- Compose multiple strategies
- Built into Aspire via AddStandardResilienceHandler()

ASPIRE DEFAULT: Retry + Circuit Breaker + Timeout combined!

Think: 'Resilience patterns are like defensive driving - expect things to go wrong and be prepared!'