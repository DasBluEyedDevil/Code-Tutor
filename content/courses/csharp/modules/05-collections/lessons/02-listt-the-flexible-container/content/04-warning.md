---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These!

**Forgetting the using directive**: Lists require `using System.Collections.Generic;` at the top of your file. Without it, C# doesn't recognize the List type!

**Using .Length instead of .Count**: Arrays use .Length, Lists use .Count. They're similar but NOT interchangeable - list.Length doesn't exist!

**Modifying while iterating**: Removing items from a list WHILE looping through it with foreach causes a crash! Use a regular for loop (iterating backwards) or create a copy first.

**Index still matters**: Even though lists resize, you can still get IndexOutOfRangeException if you access list[10] when only 5 items exist. Use .Count to check bounds!