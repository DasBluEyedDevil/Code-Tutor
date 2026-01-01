---
type: "KEY_POINT"
title: "Dual Syntax: var vs Explicit Types"
---

You'll see TWO styles for declaring variables in Java:

MODERN SYNTAX (Java 10+):
var name = "Alice";
var age = 25;
var price = 19.99;
var items = new ArrayList<String>();

Use this when: The type is obvious from the right side.

TRADITIONAL SYNTAX (Java 8+):
String name = "Alice";
int age = 25;
double price = 19.99;
ArrayList<String> items = new ArrayList<String>();

Use this when: Working with Java 8, the type isn't obvious, or for class fields and parameters.

WHY BOTH EXIST:
- 'var' was added in Java 10 to reduce boilerplate
- Many codebases still use explicit types for clarity
- Enterprise environments often prefer explicit types

BOTH ARE VALID! The underlying type is the same. Choose based on readability and your team's style guide.