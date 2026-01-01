---
type: "THEORY"
title: "Sealed Class Rules"
---

PERMITTED SUBCLASSES MUST:
1. Be in the same package (or module) as the sealed class
2. Directly extend the sealed class
3. Be one of: final, sealed, or non-sealed

THREE MODIFIERS FOR SUBCLASSES:

// FINAL - Cannot be extended further
public final class Circle extends Shape { }

// SEALED - Continues the sealing chain
public sealed class Polygon extends Shape permits Triangle, Rectangle { }

// NON-SEALED - Opens hierarchy back up
public non-sealed class CustomShape extends Shape { }
// Now anyone can extend CustomShape

SYNTAX VARIATIONS:

// Implicit permits (when all subclasses in same file)
sealed class Result {
    final class Success extends Result { }
    final class Failure extends Result { }
}
// No 'permits' clause needed - compiler infers from same file

// Sealed interfaces work the same way
sealed interface Expression permits Constant, Add, Multiply { }