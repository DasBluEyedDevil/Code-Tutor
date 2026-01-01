---
type: "WARNING"
title: "Common Pitfalls"
---

**Forgetting 'new':** `Player p = Player();` is WRONG! You must use `new Player()` to create an instance. Without 'new', C# thinks you're calling a method.

**Class vs Object confusion:** The class is the BLUEPRINT (definition). Objects are INSTANCES built from the blueprint. You can have many Player objects, but only one Player class.

**Public fields are risky!** While we use `public string Name;` here for simplicity, real code uses properties with get/set for data protection. You'll learn this soon!

**Null reference errors:** If you declare `Player p;` without `= new Player()`, then `p.Name` will crash! Always initialize your objects.