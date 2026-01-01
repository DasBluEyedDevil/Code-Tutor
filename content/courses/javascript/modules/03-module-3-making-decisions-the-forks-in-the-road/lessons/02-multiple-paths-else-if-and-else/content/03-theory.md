---
type: "THEORY"
title: "Breaking Down the Syntax"
---

The structure:

if (first condition) {
  // Runs if first condition is true
} else if (second condition) {
  // Runs if first is false BUT second is true  
} else if (third condition) {
  // Runs if first and second are false BUT third is true
} else {
  // Runs if ALL above conditions are false (the 'default')
}

Important rules:

1. You MUST start with 'if' - you can't have 'else if' or 'else' without an 'if' first

2. You can have as many 'else if' blocks as you want (0, 1, 5, 100...)

3. The 'else' block is optional - it's the "catch-all" for when nothing else is true

4. ONLY ONE block of code will run - the first one with a true condition
   - If the first 'if' is true, the rest are skipped completely
   - If the first 'if' is false, check the first 'else if'
   - And so on...

5. Order matters! In the grade example:
   - We check >= 90 first
   - Then >= 80 (which also includes 90-100, but we already handled those)
   - If we checked >= 60 first, everyone would get a 'D' because 90 >= 60 is true!