---
type: "KEY_POINT"
title: "JEP 477: Implicit Main - No More Ceremony"
---

Java 23 lets you write programs without class declarations:

// OLD WAY (still works):
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello!");
    }
}

// NEW WAY (Java 23+):
void main() {
    println("Hello!");
}

What changed:
- No public class declaration needed
- No static keyword
- No String[] args (unless you need them)
- println() works directly (no System.out)

Perfect for:
- Learning Java
- Quick scripts
- Small utilities

Note: For larger applications, you'll still use proper class structure.