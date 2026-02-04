---
type: "KEY_POINT"
title: "Key Takeaways"
---

**`when` expressions replace switch statements and long if-else chains** with more readable and safer code. Unlike Java's switch, `when` is exhaustive for sealed classes and enums, forcing you to handle all cases.

**Use `when` without an argument for arbitrary conditions**, making it a powerful replacement for complex if-else ladders. This is Kotlin's answer to pattern matching found in functional languages.

**Comma-separated values in `when` branches check multiple cases efficiently**. Write `when (day) { "Mon", "Tue", "Wed", "Thu", "Fri" -> "Weekday" }` instead of five separate branches.
