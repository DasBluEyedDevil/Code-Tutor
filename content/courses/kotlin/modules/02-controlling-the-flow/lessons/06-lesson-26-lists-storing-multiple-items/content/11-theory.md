---
type: "THEORY"
title: "Quick Quiz"
---


**Question 1:** What's the output?

<details>
<summary>Answer</summary>

**Output:**

**Explanation:** `list[0]` gets the first element, `last()` gets the last element.
</details>

---

**Question 2:** What's wrong with this code?

<details>
<summary>Answer</summary>

**Error:** `listOf()` creates an **immutable** list. You can't add to it.

**Fix:** Use `mutableListOf()` instead:
</details>

---

**Question 3:** What does this produce?

<details>
<summary>Answer</summary>

**Output:** `[6, 8, 10]`

**Explanation:**
1. Filter keeps: `[3, 4, 5]` (values > 2)
2. Map doubles: `[6, 8, 10]`
</details>

---

**Question 4:** What's the size?

<details>
<summary>Answer</summary>

**Output:** `3`

**Explanation:**
1. Start: `[1, 2, 3]` (size 3)
2. Add 4: `[1, 2, 3, 4]` (size 4)
3. Remove 2: `[1, 3, 4]` (size 3)
</details>

---



```kotlin
val list = mutableListOf(1, 2, 3)
list.add(4)
list.remove(2)
println(list.size)
```
