---
type: "LEGACY_COMPARISON"
title: "The Old Way: System.out.println and Class Boilerplate"
---

In older Java tutorials, Stack Overflow answers, and enterprise codebases, you'll see this pattern:

```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }
}
```

This is the TRADITIONAL way to write Java (Java 1.0 through Java 21). It still works perfectly, but it requires concepts you haven't learned yet:
- `public class Main` -- a class declaration (covered in Module 04)
- `public static void main(String[] args)` -- the full method signature (covered in Module 02 Lesson 06)
- `System.out.println()` -- the old printing method

WHAT CHANGED:
- `System.out.println("Hello")` became `IO.println("Hello")` -- shorter and cleaner
- `public class Main { public static void main(String[] args) { ... } }` became `void main() { ... }` -- no ceremony
- Multiple import statements became `import module java.base;` -- one line for everything

WHY IT CHANGED:
Java recognized that beginners shouldn't need to understand classes, static methods, and the System class just to print "Hello." Compact source files (JEP 512) let you focus on programming concepts first and add structure later.

WHEN YOU'LL SEE THE OLD SYNTAX:
- Reading older tutorials or textbooks
- Working in enterprise codebases (most still use Java 17 or 21)
- Job interview questions (interviewers may use traditional syntax)
- Stack Overflow answers written before 2025

Both styles produce identical results. This course uses the modern syntax, but you should recognize the traditional form when you encounter it.
