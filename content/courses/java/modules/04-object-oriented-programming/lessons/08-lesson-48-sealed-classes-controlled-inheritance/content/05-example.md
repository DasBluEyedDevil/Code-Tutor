---
type: "EXAMPLE"
title: "Sealed Classes in Action: Expression Evaluator"
---

Sealed classes with records create clean, type-safe domain models. The compiler enforces exhaustive handling of all cases.

```java
// Define a sealed expression hierarchy using records
sealed interface Expr permits Constant, Add, Multiply, Negate {}

record Constant(int value) implements Expr {}
record Add(Expr left, Expr right) implements Expr {}
record Multiply(Expr left, Expr right) implements Expr {}
record Negate(Expr expr) implements Expr {}

// Evaluate expressions - compiler ensures all cases handled!
int evaluate(Expr expr) {
    return switch (expr) {
        case Constant(int v) -> v;
        case Add(Expr l, Expr r) -> evaluate(l) + evaluate(r);
        case Multiply(Expr l, Expr r) -> evaluate(l) * evaluate(r);
        case Negate(Expr e) -> -evaluate(e);
    };
}

// Pretty print expressions
String format(Expr expr) {
    return switch (expr) {
        case Constant(int v) -> String.valueOf(v);
        case Add(Expr l, Expr r) -> "(" + format(l) + " + " + format(r) + ")";
        case Multiply(Expr l, Expr r) -> format(l) + " * " + format(r);
        case Negate(Expr e) -> "-" + format(e);
    };
}

void main() {
    // Build expression: (3 + 4) * 2
    Expr expr = new Multiply(
        new Add(new Constant(3), new Constant(4)),
        new Constant(2)
    );
    
    println(format(expr));    // (3 + 4) * 2
    println(evaluate(expr));  // 14
    
    // Expression: -(5 + 3)
    Expr neg = new Negate(new Add(new Constant(5), new Constant(3)));
    println(format(neg));     // -(5 + 3)
    println(evaluate(neg));   // -8
}
```
