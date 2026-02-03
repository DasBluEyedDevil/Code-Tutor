---
type: "THEORY"
title: "Summary"
---


Congratulations! You've mastered Kotlin's `when` expression. Let's recap:

**Key Concepts:**
- **When expression** provides elegant multi-way decisions
- **Value matching** checks against specific values
- **Multiple values** can be matched with commas
- **Ranges** use `in` keyword for range checking
- **Conditions** can be complex with argument-less when
- **Type checking** with `is` and smart casts
- **Expression vs statement** - expressions need else

**When Syntax Patterns:**

**Looking Ahead:** Kotlin 2.2 introduced **guard conditions** in `when` branches, letting you add `if` conditions directly to branches (e.g., `is String if value.isNotEmpty() ->`). You'll encounter these in more advanced modules — for now, the argument-less `when` form covers the same use cases.

**Best Practices:**
- Use `when` for 3+ options
- Put specific cases before general ones
- Always include `else` for expressions
- Use braces for multi-statement branches
- Consider ranges for numeric values

---



```kotlin
// Basic value matching
when (x) {
    1 -> "One"
    2, 3 -> "Two or Three"
    else -> "Other"
}

// Range matching
when (score) {
    in 90..100 -> "A"
    in 80..89 -> "B"
    else -> "C or lower"
}

// Condition matching
when {
    x > 10 -> "Large"
    x > 5 -> "Medium"
    else -> "Small"
}
```
