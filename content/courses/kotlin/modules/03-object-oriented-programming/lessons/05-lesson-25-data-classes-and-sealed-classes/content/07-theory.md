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
- Subclasses must be defined in the same file (or as nested classes)
- Cannot be instantiated directly
- Perfect for `when` expressions (exhaustive checking)

---



```kotlin
sealed class NetworkResult {
    data class Success(val data: String) : NetworkResult()
    data class Error(val code: Int, val message: String) : NetworkResult()
    object Loading : NetworkResult()
    object Idle : NetworkResult()
}
```
