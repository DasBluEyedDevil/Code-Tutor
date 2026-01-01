---
type: "KEY_POINT"
title: "Lambda Syntax"
---

Lambda expressions have this basic syntax:

(parameters) -> expression
// or
(parameters) -> { statements; }

Examples:

// No parameters
() -> System.out.println("Hello")

// One parameter (parentheses optional)
x -> x * 2
(x) -> x * 2

// Two parameters
(x, y) -> x + y

// Multiple statements (need braces and return)
(x, y) -> {
    int sum = x + y;
    return sum * 2;
}

// With explicit types
(String s) -> s.length()

The arrow (->) separates parameters from the body. Think of it as 'goes to' or 'becomes'.