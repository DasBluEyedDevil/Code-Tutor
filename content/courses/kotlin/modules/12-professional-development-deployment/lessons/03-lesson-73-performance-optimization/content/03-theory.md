---
type: "THEORY"
title: "The Golden Rule of Optimization"
---


### Measure First, Optimize Second

**Wrong Approach**:

**Right Approach**:

**Why This Matters**:
- 90% of execution time is spent in 10% of code
- Optimizing the wrong code = wasted time
- Profilers show you the **actual** bottlenecks

---



```kotlin
// 1. Measure with profiler
// 2. Find actual bottleneck (it's not where you think!)
// 3. Optimize the bottleneck
// 4. Measure again to verify improvement
```
