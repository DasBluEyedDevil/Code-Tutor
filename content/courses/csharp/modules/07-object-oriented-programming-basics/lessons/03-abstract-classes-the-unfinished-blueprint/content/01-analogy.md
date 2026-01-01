---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine an architect creates a 'Building' blueprint but leaves some parts intentionally blank: 'Each building MUST have an entrance, but I'm not specifying what kind - you figure that out!'

That's an ABSTRACT CLASS! It's an INCOMPLETE blueprint that:
• CAN'T be instantiated directly (can't create a 'Building' object)
• MUST be inherited
• Can have ABSTRACT methods (no implementation - derived classes MUST provide it)
• Can also have regular (concrete) methods with full implementation

When to use: When classes share common features, but some features don't make sense in the base class. 'Animal.MakeSound()' - what sound does a generic animal make? Doesn't make sense! Make it abstract - force Dog, Cat to implement it.

Abstract = 'Template with mandatory blanks to fill in'.