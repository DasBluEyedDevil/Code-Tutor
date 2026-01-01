---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting 'let' or 'const': Writing just age = 25; instead of let age = 25; In strict mode, this causes an error.

2. Using 'let' or 'const' when USING a variable: You only use 'let' or 'const' when CREATING the variable. After that, just use the name:
   Correct: let age = 25; age = 26;
   Wrong: let age = 25; let age = 26; (Can't create the same box twice!)

3. Trying to change a 'const': Remember, const means 'constant'. Once set, it cannot be changed.

4. Misspelling variable names: If you create 'userName' but later try to use 'username', JavaScript will say 'username is not defined' because it's looking for a box with that exact label.

5. Not using quotes for text: let name = Alice; won't work. Text must be in quotes: let name = 'Alice';