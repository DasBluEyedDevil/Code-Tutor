---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`get { return _age; }`**: The 'get' accessor returns the value. Called when you READ the property (Console.WriteLine(person.Age)).

**`set { _age = value; }`**: The 'set' accessor assigns the value. 'value' is a special keyword containing what the user is trying to assign. Add validation here!

**`public string Name { get; set; }`**: AUTO-IMPLEMENTED property. C# creates a hidden backing field for you. Use when you don't need validation.

**`{ get; init; }`** (C# 9+): INIT-ONLY property. Can be set during object initialization but becomes read-only afterwards. Perfect for immutable data that should never change after creation!

**`required`** (C# 11+): Forces callers to initialize the property when creating an object. The compiler won't let you forget! Combine with init for required immutable properties.

**`=> expression`**: Expression-bodied property (C# 6+). Shorthand for get-only properties. 'public string FullName => First + " " + Last;' returns calculated value.