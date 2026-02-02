---
type: "KEY_POINT"
title: "Compact Source Files: How Your Code Works"
---

When you write a compact source file like this:

void main() {
    IO.println("Hello!");
}

Java automatically provides the surrounding structure. You don't need to declare a class or worry about the `public static` keywords.

What compact source files give you:
- No class declaration needed for simple programs
- No `static` keyword on main()
- No `String[] args` (unless you need command-line arguments)
- IO.println() for console output (the IO class is in java.lang, auto-imported)

This is the standard way to write simple Java programs. As your programs grow and you start creating your own classes (Module 04), you'll learn the full class syntax naturally.