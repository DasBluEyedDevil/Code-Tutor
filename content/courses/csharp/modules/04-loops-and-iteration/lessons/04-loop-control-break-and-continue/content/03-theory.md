---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`break;`**: Immediately exits the loop. Code jumps to the first line AFTER the loop. The loop is completely done, even if iterations remain!

**`continue;`**: Skips the rest of the current iteration and jumps back to the loop's condition check. The loop keeps going, just skips THIS round!

**`break in nested loops`**: break only exits the INNERMOST loop it's in! If you have loops inside loops, break only exits the current one, not all of them.

**`When to use`**: break: When you find what you're looking for and don't need to continue. continue: When you want to skip specific items but keep processing others.

**`Works in all loops`**: Both break and continue work in for, foreach, while, and do-while loops. They're essential for efficient loop control.