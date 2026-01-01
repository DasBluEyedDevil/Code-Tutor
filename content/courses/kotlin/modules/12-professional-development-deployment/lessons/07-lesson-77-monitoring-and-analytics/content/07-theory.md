---
type: "THEORY"
title: "Performance Monitoring (APM)"
---


### New Relic Integration


**newrelic.yml**:

**Custom Metrics**:

### Custom Performance Tracking


---



```kotlin
class PerformanceMonitor {
    // ConcurrentHashMap is correct here - metrics may be recorded from multiple threads
    private val metrics = ConcurrentHashMap<String, MutableList<Long>>()

    fun track(operation: String, block: () -> Unit) {
        val start = System.nanoTime()
        try {
            block()
        } finally {
            val duration = (System.nanoTime() - start) / 1_000_000 // ms
            metrics.getOrPut(operation) { mutableListOf() }.add(duration)
        }
    }

    suspend fun <T> trackSuspend(operation: String, block: suspend () -> T): T {
        val start = System.nanoTime()
        try {
            return block()
        } finally {
            val duration = (System.nanoTime() - start) / 1_000_000
            metrics.getOrPut(operation) { mutableListOf() }.add(duration)
        }
    }

    fun getStats(operation: String): PerformanceStats? {
        val durations = metrics[operation] ?: return null

        return PerformanceStats(
            operation = operation,
            count = durations.size,
            avgMs = durations.average(),
            minMs = durations.minOrNull() ?: 0,
            maxMs = durations.maxOrNull() ?: 0,
            p95Ms = durations.sorted()[durations.size * 95 / 100],
            p99Ms = durations.sorted()[durations.size * 99 / 100]
        )
    }

    fun getAllStats(): Map<String, PerformanceStats> {
        return metrics.keys.associateWith { getStats(it)!! }
    }
}

// Usage
val monitor = PerformanceMonitor()

monitor.track("database_query") {
    userRepository.findAll()
}

val user = monitor.trackSuspend("api_call") {
    apiClient.getUser(userId)
}
```
