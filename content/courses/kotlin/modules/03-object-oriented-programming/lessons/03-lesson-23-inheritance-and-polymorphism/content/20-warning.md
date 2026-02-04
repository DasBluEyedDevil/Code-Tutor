---
type: "WARNING"
title: "Deep Inheritance Hierarchies"
---

**Avoid creating deep inheritance chains** beyond 3-4 levels. Deep hierarchies become rigid, hard to understand, and difficult to change.

**The diamond problem** occurs with multiple inheritance when two parent classes inherit from the same grandparent. Kotlin avoids this with single class inheritance, but interfaces can still create complexity when multiple interfaces define similar methods.

**Prefer composition over inheritance** when you're tempted to inherit just to reuse code. If the relationship isn't truly "is-a", use composition (has-a) instead. A `Car` shouldn't inherit from `Engine` just to reuse engine logic—it should contain an `Engine` instance.

**Example of bad inheritance:**
```kotlin
open class Animal
open class Mammal : Animal()
open class Carnivore : Mammal()
open class Feline : Carnivore()
class Cat : Feline()  // Too deep!
```

Better approach: limit depth and use interfaces for behavior.
