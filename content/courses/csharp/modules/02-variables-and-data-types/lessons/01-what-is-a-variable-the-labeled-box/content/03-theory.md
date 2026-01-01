---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`string playerName;`**: 'string' means this box holds TEXT. 'playerName' is the label on the box. The semicolon ends the statement.

**`playerName = "Alex";`**: The = sign means 'put this value INTO the box'. We're storing the text 'Alex' in our playerName box.

**`Console.WriteLine(playerName);`**: No quotes around playerName! That tells C# to look INSIDE the box and get the value, not just display the word 'playerName'.

**`var score = 100;`**: The `var` keyword lets C# infer the type from the value you assign. Use it when the type is obvious from the right side - it makes code cleaner!