---
type: "THEORY"
title: "Profiling Tools"
---


### Android Studio Profiler

**CPU Profiler**:

Shows:
- Which functions take the most time
- Call stack and flame graphs
- Thread activity

**Example Output**:

**Memory Profiler**:

Shows:
- Memory allocation over time
- Heap dumps
- Memory leaks

**Network Profiler**:

Shows:
- Request/response times
- Payload sizes
- Connection duration

### Ktor Server Profiling

**Add Timing Plugin**:

**Output**:

---



```kotlin
GET /api/users - 45ms
GET /api/products - 850ms ⚠️ SLOW!
POST /api/orders - 120ms
```
