---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're playing a game and you gain 10 points. You'd write:

score = score + 10;

This takes your current score, adds 10, and stores the new value back in score. But C# has a SHORTCUT:

score += 10;

The += operator means 'add this to the variable'. It's shorter and clearer!

C# has shortcuts for all math operations:
• += (add and assign): score += 5 → score = score + 5
• -= (subtract and assign): lives -= 1 → lives = lives - 1
• *= (multiply and assign): damage *= 2 → damage = damage * 2
• /= (divide and assign): speed /= 2 → speed = speed / 2

There are also special shortcuts for adding/subtracting 1:
• ++ (increment by 1): score++ → score = score + 1
• -- (decrement by 1): lives-- → lives = lives - 1

Real programmers use these shortcuts ALL THE TIME!