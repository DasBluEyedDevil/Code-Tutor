---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Pitfall 1: Confusing && with ||

❌ **WRONG (wants AND but uses OR):**

✅ **CORRECT:**

### Pitfall 2: Redundant Comparisons

❌ **Redundant:**

✅ **Clean:**

❌ **Redundant:**

✅ **Clean:**

### Pitfall 3: Complex Nested Conditions

❌ **Hard to read:**

✅ **Use variables for clarity:**

### Best Practice 1: Use Parentheses for Complex Logic

Make your intent crystal clear:


### Best Practice 2: DeMorgan's Laws

Sometimes you can simplify logic using DeMorgan's Laws:

**DeMorgan's Law 1:**

**DeMorgan's Law 2:**

**Example:**

---



```kotlin
// These are equivalent:
if (!(isWeekend && isHoliday)) { /* ... */ }
if (!isWeekend || !isHoliday) { /* ... */ }
```
