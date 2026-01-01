---
type: "WARNING"
title: "Common Mistakes"
---

**Using a variable before declaring it**: C# must know about your 'box' before you can put things in it. `playerName = "Alex"; string playerName;` causes an error!

**Misspelling variable names**: C# is case-sensitive! `playerName` and `playername` are completely different variables.

**Using quotes when you mean the variable**: `Console.WriteLine("playerName")` prints the word 'playerName'. `Console.WriteLine(playerName)` prints the VALUE inside the variable.

**Overusing `var`**: Only use `var` when the type is obvious. `var x = GetData();` is unclear - what type is x? Use explicit types when it helps readability.