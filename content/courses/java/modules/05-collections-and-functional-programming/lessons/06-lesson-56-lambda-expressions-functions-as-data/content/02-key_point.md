---
type: "KEY_POINT"
title: "Lambda Syntax Explained"
---

A lambda is an anonymous function you can pass around:

(parameters) -> expression
  or
(parameters) -> { statements; }

EXAMPLES:

// No parameters
() -> IO.println("Hello")

// One parameter (parentheses optional)
x -> x * 2
(x) -> x * 2

// Two parameters
(a, b) -> a + b

// With types (optional)
(String s) -> s.length()
(int a, int b) -> a + b

// Multi-line with braces (must use return)
(a, b) -> {
    int sum = a + b;
    return sum * 2;
}

KEY RULES:
1. Types are usually inferred (don't need to specify)
2. Single expression: no braces, implicit return
3. Multiple statements: need braces and explicit return