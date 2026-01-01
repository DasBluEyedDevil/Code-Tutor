---
type: "KEY_POINT"
title: "When Async Helps vs When It Doesn't"
---

**Async is GREAT for I/O-bound tasks:**
- API calls and web requests
- Database queries
- File operations
- WebSocket connections
- Any operation where you're WAITING for external systems

**Async does NOT help CPU-bound tasks:**
- Mathematical computations
- Image processing
- Data crunching
- Machine learning training

**Why?** Async is about using wait time efficiently. If there's no waiting (pure computation), there's no time to reclaim.

**Rule of thumb:**
- **I/O-bound** (waiting) -> Use `async/await`
- **CPU-bound** (computing) -> Use `multiprocessing`
- **Mixed** -> Combine both approaches