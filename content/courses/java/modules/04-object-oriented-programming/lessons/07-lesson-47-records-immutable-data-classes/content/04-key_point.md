---
type: "KEY_POINT"
title: "Records are Immutable"
---

Records are IMMUTABLE by design:

Person alice = new Person("Alice", 30);
alice.age = 31;  // COMPILE ERROR! No setter exists

To "change" a record, create a new one:

Person olderAlice = new Person(alice.name(), alice.age() + 1);

WHY IMMUTABILITY?
1. Thread-safe by default (no synchronization needed)
2. Can be used as map keys safely
3. Easy to reason about (no unexpected changes)
4. Functional programming friendly

RECORD RESTRICTIONS:
- Cannot extend other classes (implicitly extend Record)
- Cannot be extended (implicitly final)
- All fields are final (cannot be changed)
- Cannot declare instance fields (only components)

RECORDS CAN:
- Implement interfaces
- Have static fields and methods
- Have instance methods
- Override accessor methods (rarely needed)