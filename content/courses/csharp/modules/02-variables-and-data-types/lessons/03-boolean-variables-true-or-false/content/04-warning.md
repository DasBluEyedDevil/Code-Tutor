---
type: "WARNING"
title: "Common Mistakes"
---

**Putting quotes around true/false**: `bool isReady = "true";` is WRONG! This creates a string, not a boolean. Use `bool isReady = true;` without quotes.

**Capitalizing True/False**: C# is case-sensitive! Use lowercase `true` and `false`. `True` and `FALSE` are not valid.

**Trying to assign other values**: `bool isActive = 1;` doesn't work! In C#, booleans can ONLY be `true` or `false` - not 1/0, not "yes"/"no".

**Poor naming**: `bool flag = true;` - what does 'flag' mean? Use descriptive names like `isGameOver`, `hasPermission`, or `canEdit` that explain what question the boolean answers.