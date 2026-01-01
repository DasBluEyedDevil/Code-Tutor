---
type: "WARNING"
title: "Common Pitfalls"
---

**Don't add a return type!** Writing `public void Player()` makes it a regular method named 'Player', NOT a constructor! Constructors have NO return type.

**Primary constructor parameters aren't properties!** In `class Person(string name)`, 'name' is just a parameter, not a public property. You must explicitly create properties: `public string Name { get; } = name;`

**Forgetting 'new':** Writing `Player p = Player("Alice")` is wrong! Must use `new Player("Alice")` to instantiate.

**Constructor chaining:** If you have multiple constructors, use `this()` to call another constructor from the same class, avoiding code duplication.