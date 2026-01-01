---
type: "WARNING"
title: "Common Pitfalls"
---

**The 'required' keyword only works at compile time!** If you create objects using reflection or JSON deserialization, 'required' won't throw errors - it's purely a compile-time check.

**Don't confuse 'init' with 'private set':** Both restrict modification, but 'init' allows setting during object initialization (even from outside), while 'private set' only allows the class itself to modify the value.

**Auto-properties can't have validation!** If you write `{ get; set; }`, there's no place to add validation logic. You need the full property syntax with a backing field to add checks.

**Infinite recursion trap:** Never do `public int Age { get { return Age; } }` - this calls itself forever! Use a backing field: `get { return _age; }`