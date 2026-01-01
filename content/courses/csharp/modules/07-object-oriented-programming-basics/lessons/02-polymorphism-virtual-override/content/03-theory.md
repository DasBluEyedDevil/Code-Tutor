---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`public virtual void Method()`**: 'virtual' in base class means 'derived classes CAN override this'. Without virtual, derived classes can't override (they can hide with 'new', but that's different).

**`public override void Method()`**: 'override' in derived class replaces the base class implementation. Signature (name, parameters, return type) MUST match exactly!

**`Animal dog = new Dog();`**: You can store a derived type in a base type variable! This is polymorphism - the variable is Animal, but the object is Dog. Calls Dog's methods!

**`base.Method()`**: In override method, use 'base.Method()' to call the BASE class version. Useful when you want to EXTEND, not REPLACE.