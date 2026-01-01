---
type: "THEORY"
title: "Star Projections"
---


Star projection `*` is used when you don't know or care about the type argument:


**Rules for `List<*>`**:
- Equivalent to `List<out Any?>`
- You can read items (as `Any?`)
- For `MutableList<*>`: can't add items, can only read

---



```kotlin
fun printList(list: List<*>) {
    for (item in list) {
        println(item)  // item is Any?
    }
}

fun main() {
    printList(listOf(1, 2, 3))
    printList(listOf("a", "b", "c"))

    // Star projection on mutable types
    val anyList: MutableList<*> = mutableListOf(1, 2, 3)
    // anyList.add(4)  // ❌ Error: can't add to MutableList<*>
    val item = anyList[0]  // ✅ Can read (as Any?)
}
```
