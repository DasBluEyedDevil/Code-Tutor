---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Using = instead of ===:
   if (age = 18)  // WRONG - this assigns 18 to age!
   if (age === 18)  // CORRECT - this compares

2. Using == instead of ===:
   JavaScript has == (loose equality) and === (strict equality)
   - 5 == '5' → true (converts types, then compares)
   - 5 === '5' → false (different types)
   ALWAYS use === and !== to avoid surprises

3. Comparing strings with > or <:
   'apple' < 'banana' → true (alphabetical order works!)
   But '10' < '2' → true (compares as strings, not numbers)
   To compare string numbers, convert first: Number('10') < Number('2') → false

4. Case sensitivity:
   'Hello' === 'hello' → false
   To ignore case: 'Hello'.toLowerCase() === 'hello'.toLowerCase()

5. Confusing >= and =>:
   >= is greater-than-or-equal (comparison)
   => is for arrow functions (we'll learn later)