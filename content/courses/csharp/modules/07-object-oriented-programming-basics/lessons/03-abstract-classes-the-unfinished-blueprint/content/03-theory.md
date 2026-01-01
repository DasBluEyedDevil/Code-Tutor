---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`abstract class ClassName`**: 'abstract' keyword makes class abstract. Can't do 'new ClassName()' - you MUST inherit and use derived classes.

**`public abstract void Method();`**: Abstract method has NO BODY (no { }), just semicolon! Derived classes MUST override and provide implementation. Like a contract: 'you must implement this!'

**`Concrete methods in abstract class`**: Abstract classes CAN have regular methods with implementation! Mix of abstract (must override) and concrete (inherited as-is) methods is common.

**`Forcing implementation`**: If you inherit from abstract class, compiler FORCES you to override all abstract methods! Forget one = compile error. This ensures consistency.