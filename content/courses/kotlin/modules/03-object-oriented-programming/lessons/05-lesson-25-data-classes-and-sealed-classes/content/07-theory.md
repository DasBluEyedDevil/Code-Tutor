---
type: "THEORY"
title: "Sealed Classes"
---


**Sealed classes** represent restricted class hierarchies where all subclasses are known at compile-time.

### Why Sealed Classes?

**Problem**: Modeling states or results with regular classes


**Solution**: Sealed classes


### Defining Sealed Classes


**Key Points**:
- Subclasses must be defined in the same package (since Kotlin 1.5, they no longer need to be in the same file)
- Cannot be instantiated directly
- Perfect for `when` expressions (exhaustive checking)
- Use `data object` (not plain `object`) for stateless branches — it gives you a clean `toString()`, consistent `equals()`, and stable `hashCode()`

> **Sealed interfaces** also exist and are preferred when you need subclasses to implement multiple sealed hierarchies, since Kotlin only allows single class inheritance but supports multiple interfaces.

---



```kotlin
sealed class NetworkResult {
    data class Success(val data: String) : NetworkResult()
    data class Error(val code: Int, val message: String) : NetworkResult()
    data object Loading : NetworkResult()
    data object Idle : NetworkResult()
}
```
