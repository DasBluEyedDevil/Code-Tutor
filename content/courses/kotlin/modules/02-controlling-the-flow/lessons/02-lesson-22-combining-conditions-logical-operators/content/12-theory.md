---
type: "THEORY"
title: "Summary"
---


Congratulations! You've mastered logical operators. Let's recap:

**Key Concepts:**
- **AND (`&&`)**: Both conditions must be true
- **OR (`||`)**: At least one condition must be true
- **NOT (`!`)**: Inverts/flips a Boolean value
- **Short-circuit evaluation**: Optimization that skips unnecessary checks
- **Precedence**: `!` → `&&` → `||` (use parentheses for clarity)

**Truth Tables:**

**Common Patterns:**

**Best Practices:**
- Use parentheses to make complex conditions clear
- Extract complex logic into named Boolean variables
- Remember short-circuit evaluation for efficiency
- Avoid redundant comparisons with Boolean variables

---



```kotlin
// Range check
if (x >= min && x <= max) { }

// Multiple options
if (option1 || option2 || option3) { }

// Exclusion check
if (condition && !exception) { }

// Complex logic
if ((condition1 || condition2) && !condition3) { }
```
