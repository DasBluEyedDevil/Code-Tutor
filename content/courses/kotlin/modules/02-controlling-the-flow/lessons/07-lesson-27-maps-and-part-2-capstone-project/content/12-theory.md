---
type: "THEORY"
title: "Quick Quiz"
---


**Question 1:** What's the output?

<details>
<summary>Answer</summary>

**Output:** `null`

**Explanation:** The key "c" doesn't exist, so accessing it returns null.
</details>

---

**Question 2:** How do you add to a mutable map?

<details>
<summary>Answer</summary>

</details>

---

**Question 3:** What's wrong here?

<details>
<summary>Answer</summary>

**Error:** `mapOf()` creates an **immutable** map. Can't add to it.

**Fix:**
</details>

---

**Question 4:** How do you iterate through keys and values?

<details>
<summary>Answer</summary>

</details>

---



```kotlin
val map = mapOf("a" to 1, "b" to 2)

// With destructuring (recommended)
for ((key, value) in map) {
    println("$key -> $value")
}

// Or with entry
for (entry in map) {
    println("${entry.key} -> ${entry.value}")
}
```
