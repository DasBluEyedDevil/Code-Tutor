---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`interface IInterfaceName`**: Interfaces use 'interface' keyword. By convention, names start with 'I'. Define WHAT, not HOW.

**`void Method();`**: Interface methods have NO body, NO access modifiers (implicitly public). Just signatures ending with semicolon.

**`class C : IDrawable, IResizable`**: Class can implement MULTIPLE interfaces! Separate with commas. Must implement ALL methods from ALL interfaces.

**`Interface vs Abstract Class`**: Interface = pure contract (no implementation). Abstract = template with some implementation. Can implement many interfaces, inherit from one class.