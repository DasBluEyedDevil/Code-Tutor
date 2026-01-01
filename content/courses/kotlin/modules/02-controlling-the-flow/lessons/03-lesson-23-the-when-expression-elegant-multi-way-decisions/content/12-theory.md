---
type: "THEORY"
title: "Quick Quiz"
---


**Question 1:** What will this print?

<details>
<summary>Answer</summary>

**Output:** `Medium`

**Explanation:** `5` is in the range `4..6`, so "Medium" is returned.
</details>

---

**Question 2:** Is this valid code?

<details>
<summary>Answer</summary>

**Yes!** This is valid. When used as a **statement** (not returning a value), `else` is optional. If `day = 3`, nothing will print.
</details>

---

**Question 3:** What's wrong with this?

<details>
<summary>Answer</summary>

**Problem:** The second branch (`in 90..100`) will never execute because it's completely covered by the first branch (`in 0..100`). Always put more specific conditions first!

**Fixed:**
</details>

---

**Question 4:** Can you use `when` with strings?

<details>
<summary>Answer</summary>

**Yes!** `when` works with any type:

</details>

---



```kotlin
val fruit = "apple"
when (fruit) {
    "apple" -> println("Red or green")
    "banana" -> println("Yellow")
    else -> println("Unknown fruit")
}
```
