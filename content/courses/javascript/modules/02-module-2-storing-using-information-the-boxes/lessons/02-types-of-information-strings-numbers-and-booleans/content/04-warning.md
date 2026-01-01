---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Quotes around numbers: let age = '25'; makes age a STRING, not a NUMBER. You can't do math with it: '25' + 5 = '255' (string joining, not 30).

2. Quotes around booleans: let isStudent = 'true'; makes it a STRING containing the word 'true', not the boolean value true.

3. Forgetting quotes around text: let name = Alice; won't work. JavaScript thinks Alice is a variable, not text. Use let name = 'Alice';

4. Mixing up + for numbers vs strings:
   - 5 + 5 = 10 (math)
   - '5' + '5' = '55' (joining strings)
   - '5' + 5 = '55' (JavaScript converts the number to a string)

5. Case matters: true and false must be lowercase. True or FALSE won't work.