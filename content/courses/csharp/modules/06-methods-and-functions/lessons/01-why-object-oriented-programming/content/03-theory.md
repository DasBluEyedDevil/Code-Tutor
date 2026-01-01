---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`class ClassName`**: Defines a blueprint. By convention, class names use PascalCase (first letter capitalized). This is the template!

**`public string Name;`**: A FIELD - data stored in the class. 'public' means accessible from outside. Each object gets its OWN copy of this data!

**`new Player()`**: Creates an OBJECT (instance) from the class blueprint. 'new' allocates memory and builds the object. This is called INSTANTIATION.

**`object.Field`**: Dot notation accesses an object's data or methods. alice.Name gets Alice's name, bob.Name gets Bob's - they're separate!