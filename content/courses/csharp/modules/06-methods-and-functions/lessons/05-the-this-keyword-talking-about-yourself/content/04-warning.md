---
type: "WARNING"
title: "Common Pitfalls"
---

**Forgetting 'this' causes self-assignment!** In `Player(string name) { name = name; }`, you're assigning the parameter to itself! The field is never set. Use `this.name = name;`

**'this' is forbidden in static methods!** Static methods don't belong to any instance, so there's no 'this'. `public static void Method() { this.field = 5; }` is a compile error.

**Don't overuse 'this':** While `this.field` is always valid, writing it everywhere when there's no ambiguity can clutter your code. Use it when needed for clarity or disambiguation.

**'this' in constructor chaining:** Use `this()` to call another constructor in the same class: `public Player() : this("Unknown", 0) { }` - calls the two-parameter constructor.