---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Using == when you mean ===:
   This is SO common. Most bugs from == are subtle and hard to spot.
   Rule of thumb: ALWAYS use ===

2. Not understanding type coercion:
   if (userInput == true)  // Almost never what you want
   Better: if (userInput === true) or just if (userInput)

3. Assuming null == 0:
   Actually, null == 0 → false
   But null == undefined → true
   Weird, right? That's why we use ===

4. Forgetting that form inputs are strings:
   <input type='number'> still gives you a string!
   Always convert: Number(input.value)

5. Triple equals in other languages:
   PHP has ===, but most languages (Java, Python, C#) only have ==
   JavaScript is unique in needing this distinction