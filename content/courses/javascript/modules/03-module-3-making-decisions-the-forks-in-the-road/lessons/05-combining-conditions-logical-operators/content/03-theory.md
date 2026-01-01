---
type: "THEORY"
title: "Breaking Down the Syntax"
---

The three logical operators:

1. && (AND)
   - true && true → true
   - true && false → false
   - false && true → false
   - false && false → false
   - ALL conditions must be true for the result to be true

2. || (OR)
   - true || true → true
   - true || false → true
   - false || true → true
   - false || false → false
   - AT LEAST ONE condition must be true for the result to be true

3. ! (NOT)
   - !true → false
   - !false → true
   - Flips/inverts the boolean value

Order of operations:
1. ! (NOT) happens first
2. && (AND) happens second
3. || (OR) happens last

Example:
!false && true || false
= true && true || false  // ! first
= true || false          // && second
= true                   // || last

Use parentheses for clarity:
(age >= 18) && (hasLicense)

Short-circuit evaluation:
- With &&: If first is false, second is never checked
  - false && (anything) → immediately false
- With ||: If first is true, second is never checked
  - true || (anything) → immediately true

This is useful but can cause subtle bugs if you're not careful!