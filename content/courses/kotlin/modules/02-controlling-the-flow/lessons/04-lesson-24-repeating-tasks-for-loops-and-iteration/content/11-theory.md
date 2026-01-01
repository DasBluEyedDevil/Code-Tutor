---
type: "THEORY"
title: "Quick Quiz"
---


**Question 1:** What does this print?

<details>
<summary>Answer</summary>

**Output:** `1 3 5`

**Explanation:** Starts at 1, increments by 2 each time, up to 5.
- First iteration: i = 1
- Second iteration: i = 3
- Third iteration: i = 5
- Stop (next would be 7, which is > 5)
</details>

---

**Question 2:** How many times does this loop run?

<details>
<summary>Answer</summary>

**Answer:** 10 times (prints 0 through 9)

**Explanation:** `until` is exclusive of the end value. So `0 until 10` means 0, 1, 2, 3, 4, 5, 6, 7, 8, 9.
</details>

---

**Question 3:** What's the output?

<details>
<summary>Answer</summary>

**Output:** `H i`

**Explanation:** Strings are iterable. The loop goes through each character: 'H' then 'i'.
</details>

---

**Question 4:** How do you loop backwards from 10 to 1?

<details>
<summary>Answer</summary>


**Explanation:** Use `downTo` to create a reverse range.
</details>

---



```kotlin
for (i in 10 downTo 1) {
    println(i)
}
```
