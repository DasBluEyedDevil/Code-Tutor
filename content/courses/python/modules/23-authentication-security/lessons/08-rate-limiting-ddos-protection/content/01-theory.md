---
type: "THEORY"
title: "Rate Limiting Strategies"
---

**Rate limiting** protects your API from abuse by restricting how many requests a client can make in a given time window. Different algorithms offer different trade-offs:

**1. Fixed Window**
The simplest approach: count requests per fixed time window (e.g., 100 requests per minute).

```
Window: 12:00:00 - 12:00:59
Request at 12:00:30: Count = 1 (allowed)
Request at 12:00:45: Count = 2 (allowed)
...
Request at 12:00:59: Count = 100 (allowed)
Request at 12:00:59: Count = 101 (BLOCKED)
Window resets at 12:01:00: Count = 0
```

**Pros:** Simple to implement, low memory usage
**Cons:** Burst problem - user can send 100 requests at 12:00:59 and 100 more at 12:01:00 (200 in 2 seconds)

**2. Sliding Window Log**
Track timestamps of all requests, count those within the window.

```
Window size: 60 seconds
Current time: 12:01:30
Check requests between 12:00:30 and 12:01:30
```

**Pros:** Accurate, no burst problem
**Cons:** High memory usage (stores every timestamp)

**3. Sliding Window Counter**
Hybrid approach: weighted average of current and previous window.

```
Previous window (12:00-12:01): 80 requests
Current window (12:01-12:02): 20 requests so far
Current time: 12:01:15 (25% into window)

Weighted count = 80 * 0.75 + 20 = 80 requests
```

**Pros:** Good accuracy, low memory
**Cons:** Slightly more complex

**4. Token Bucket**
Tokens are added at a fixed rate; each request consumes a token.

```
Bucket capacity: 100 tokens
Refill rate: 10 tokens/second

User sends 50 requests: 50 tokens remaining
Wait 3 seconds: 80 tokens (50 + 30 refilled)
User sends 100 requests: 80 allowed, 20 rejected
```

**Pros:** Allows controlled bursts, smooth rate
**Cons:** More complex state management

**5. Leaky Bucket**
Requests queue up and are processed at a constant rate.

```
Bucket capacity: 100 requests
Leak rate: 10 requests/second

Requests enter bucket, processed at constant rate
If bucket overflows, requests are dropped
```

**Pros:** Consistent output rate
**Cons:** Can add latency, complex for distributed systems