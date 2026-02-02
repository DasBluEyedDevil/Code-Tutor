---
type: "KEY_POINT"
title: "Which Syntax Should You Use?"
---

In Java 25, printing is simple:

void main() {
    IO.println("Hello, World!");
}

That's it! IO.println() prints text to the console, followed by a new line.

- IO is a built-in class (in java.lang, auto-imported)
- println() means "print line" -- it prints your text and moves to the next line
- Everything you want to print goes inside the parentheses, in quotes

NOTE: In older tutorials and Stack Overflow answers, you'll see System.out.println() instead of IO.println(). They do the same thing -- IO.println() is the modern way. We cover the old syntax in Lesson 6.