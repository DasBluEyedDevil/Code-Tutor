---
type: "THEORY"
title: "Combining Logical Operators"
---


You can combine AND, OR, and NOT in the same expression!

### Example: Comprehensive Access Control


**How it evaluates:**
1. `age >= 18` → `17 >= 18` → false
2. `hasParentConsent` → true
3. `false || true` → **true** (at least one is true)
4. `!isVIP` → `!false` → **true**
5. `true && true` → **true** (both parts are true)
6. Execute the if block

### Order of Operations (Precedence)

Just like math has PEMDAS, logical operators have precedence:

1. **`!` (NOT)** - Highest priority
2. **`&&` (AND)** - Medium priority
3. **`||` (OR)** - Lowest priority

**Example:**

**Evaluation order:**
1. `!false` → true (NOT first)
2. `true && true` → true (AND second)
3. `true || false` → true (OR last)

**Use parentheses for clarity:**

### Complex Real-World Example: Movie Ticket Eligibility


**Breaking it down:**
- Age check: `age >= 17` is false, but `age >= 13 && hasParentConsent` is true → **passes**
- Showing access: `isMember` is true → **passes**
- Both conditions pass → **can watch!**

---



```kotlin
fun main() {
    val age = 16
    val hasParentConsent = true
    val isMatinee = false
    val isMember = true

    // Movie is R-rated, requires 17+ OR 13-16 with parent consent
    // Additionally, members get access to any showing, non-members only matinee
    val canWatch = (age >= 17 || (age >= 13 && hasParentConsent)) &&
                   (isMember || isMatinee)

    if (canWatch) {
        println("Enjoy the movie!")
    } else {
        println("Cannot watch this movie")
    }
}
```
