---
type: "KEY_POINT"
title: "JEP 512: Compact Source Files - No More Ceremony"
---

Java 25 lets you write programs without class declarations (finalized as JEP 512, after previews as JEP 477/495):

// OLD WAY (still works):
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello!");
    }
}

// NEW WAY (Java 25+):
void main() {
    IO.println("Hello!");
}

What changed:
- No public class declaration needed
- No static keyword
- No String[] args (unless you need them)
- IO.println() for console output (IO class is in java.lang)

Perfect for:
- Learning Java
- Quick scripts
- Small utilities

Note: For larger applications, you'll still use proper class structure.