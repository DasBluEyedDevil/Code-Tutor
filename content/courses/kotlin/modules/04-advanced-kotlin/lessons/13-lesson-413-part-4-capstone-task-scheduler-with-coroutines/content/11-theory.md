---
type: "THEORY"
title: "Extension Challenges"
---


Ready for more? Try these advanced challenges:

### Challenge 1: Dependency Management

Add task dependencies so tasks only run after their dependencies complete:


### Challenge 2: Task Scheduler

Implement scheduled and recurring tasks:


### Challenge 3: Persistence

Save and restore task state:


### Challenge 4: Priority Queue

Implement priority-based task execution:


### Challenge 5: Error Recovery

Add sophisticated error recovery strategies:


---



```kotlin
sealed class RecoveryStrategy {
    data object Retry : RecoveryStrategy()
    data class Fallback(val alternativeTask: Task<*>) : RecoveryStrategy()
    data class Circuit(val threshold: Int, val resetTime: Duration) : RecoveryStrategy()
}
```
