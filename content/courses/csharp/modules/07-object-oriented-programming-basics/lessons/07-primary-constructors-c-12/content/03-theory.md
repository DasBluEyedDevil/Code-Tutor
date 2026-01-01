---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`public class Person(string name, int age)`**: Parameters go right after the class name in parentheses. No need to declare fields or write constructor body!

**`Parameters are captured`**: The parameters (name, age) are available in ALL instance members - methods, properties, initializers. They act like private fields.

**`: Person(name, age)`**: In derived classes, pass primary constructor parameters to base class constructor using this syntax after the class declaration.

**`No required fields`**: Parameters are captured but NOT automatically properties. If you need a public property, you still declare it: `public string Name { get; } = name;`

**`Validation`**: You can still validate by using the parameters in field initializers: `private readonly string _name = name ?? throw new ArgumentNullException(nameof(name));`