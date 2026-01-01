---
type: "WARNING"
title: "Common Beginner Mistakes"
---


### Mistake 1: Forgetting Quotes Around Text
Text must always be surrounded by double quotes `"`. Without them, Kotlin thinks the text is a variable name.
- `println(Hello)` ❌ (Kotlin looks for a variable named Hello)
- `println("Hello")` ✅ (Correct string)

### Mistake 2: Wrong Capitalization
Kotlin is **case-sensitive**. This means `Main` is different from `main`, and `println` is different from `PrintLn`. Most keywords in Kotlin are lowercase.

### Mistake 3: Missing Parentheses
Functions always need parentheses `()`, even if they don't have any inputs.
- `fun main { ... }` ❌
- `fun main() { ... }` ✅
- `println "Hello"` ❌
- `println("Hello")` ✅
