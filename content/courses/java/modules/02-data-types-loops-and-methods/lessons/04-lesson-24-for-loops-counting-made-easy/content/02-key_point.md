---
type: "KEY_POINT"
title: "For Loops are Like a Recipe's Prep List"
---

Imagine a recipe that says:

"For each of the 12 muffin cups: fill with batter, bake for 20 minutes"

In Java:

for (cup 1 to 12) {
    fill cup with batter;
    bake cup;
}

The recipe header tells you:
- START: cup 1
- STOP WHEN: you've done 12
- EACH TIME: move to the next cup

A for loop packages all this information upfront, making it crystal clear:
"I'm going to do this task a specific number of times."