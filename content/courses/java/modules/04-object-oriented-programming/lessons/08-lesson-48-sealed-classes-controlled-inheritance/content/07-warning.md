---
type: "WARNING"
title: "Common Sealed Classes Pitfalls (Java 17+)"
---

1. FORGETTING THE MODIFIER ON SUBCLASSES:
sealed class Shape permits Circle {}
class Circle extends Shape {}  // ERROR!
// Must be: final class Circle, sealed class Circle, or non-sealed class Circle

2. SUBCLASS NOT IN SAME PACKAGE/MODULE:
sealed class Shape permits Circle {}  // in package shapes
final class Circle extends Shape {}   // in package other - ERROR!

3. INDIRECT EXTENSION:
sealed class Shape permits Circle {}
final class Circle extends Shape {}
class SmallCircle extends Circle {}  // ERROR! Circle is final

4. FORGETTING permits CLAUSE:
sealed class Shape {}  // ERROR if subclasses in different files
// Use permits, or define all subclasses in same file

5. NON-SEALED BREAKS EXHAUSTIVENESS:
sealed interface Animal permits Dog, Cat, Wild {}
final class Dog implements Animal {}
final class Cat implements Animal {}
non-sealed class Wild implements Animal {}  // Opens it up!
// Now switch needs a default because Wild can have any subclass

6. SEALED CLASSES WORK WITH INTERFACES TOO:
sealed interface, not just sealed class
Interfaces are often preferred for sealed hierarchies