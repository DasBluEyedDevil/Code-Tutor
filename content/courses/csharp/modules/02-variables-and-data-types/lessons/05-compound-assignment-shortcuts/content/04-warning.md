---
type: "WARNING"
title: "Common Mistakes"
---

**Confusing += with =**: `score = 5` REPLACES the value with 5. `score += 5` ADDS 5 to the current value. Huge difference!

**Forgetting ++ changes the variable**: `count++` permanently changes `count`. It's not temporary! If count was 5, it's now 6 forever (until you change it again).

**Using ++ on non-numbers**: `name++` doesn't work on strings! Increment and decrement only work on numeric types.

**Prefix vs postfix confusion in expressions**: `int x = 5; int y = x++;` sets y to 5 (old value), then x becomes 6. But `int y = ++x;` sets x to 6 first, then y gets 6. When in doubt, do the increment on a separate line!