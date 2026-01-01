---
type: "THEORY"
title: "The Problem: Uncontrolled Inheritance"
---

Sealed classes are a standard feature since Java 17 and are now widely adopted in modern Java development.

In traditional Java, any class can be extended unless marked final:

public class Shape { }

// Anyone can extend Shape!
class Circle extends Shape { }  // OK
class Triangle extends Shape { }  // OK
class WeirdShape extends Shape { }  // Also OK... but should it be?

THE PROBLEM:
- You can't control WHO extends your class
- Pattern matching can't be exhaustive (new subtypes could appear)
- API design is unclear - which subclasses are "official"?

Before sealed classes, you had only two options:
1. Leave class open (anyone extends)
2. Make it final (no one extends)

Sealed classes give you a THIRD option: allow SPECIFIC classes to extend.