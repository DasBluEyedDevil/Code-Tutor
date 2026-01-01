---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: A) A String that might be null**

The `?` makes a type nullable:


---

**Question 2: B) null**

Safe call returns null if the receiver is null:


---

**Question 3: B) Provides a default value when something is null**


---

**Question 4: B) Rarely, only when you're certain a value isn't null**

`!!` can cause crashes—use it sparingly:


---

**Question 5: B) null**

Safe cast returns null on failure:


---



```kotlin
val num: Any = 42
val str = num as? String  // null (safe, no crash)
```
