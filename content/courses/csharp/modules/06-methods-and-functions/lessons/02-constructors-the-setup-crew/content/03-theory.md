---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`public Player(...)`**: Constructor has the SAME NAME as the class and NO RETURN TYPE (not even void!). This is how C# knows it's a constructor.

**`Parameters`**: Constructors can take parameters to initialize the object. When you write 'new Player("Alice", 100)', you're calling the constructor with those values!

**`Name = name;`**: Inside the constructor, set the class fields using the parameters. 'Name' (field) = 'name' (parameter). Common pattern!

**`Auto-runs on 'new'`**: You don't 'call' the constructor explicitly. It runs automatically when you use 'new Player(...)'. It's the first code that executes!

**`Primary Constructors (C# 12+)`**: Write parameters directly after the class name: `class Enemy(string name, int damage)`. These parameters are available throughout the class body. Much more concise for simple classes!