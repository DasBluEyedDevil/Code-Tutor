---
type: "THEORY"
title: "Why Modern Java Hides public static void main"
---

You've been writing simple programs with just:

void main() {
    IO.println("Hello!");
}

But the traditional Java entry point is:

public static void main(String[] args) {
    System.out.println("Hello!");
}

Why does modern Java (25+, JEP 512) hide these keywords?

For beginners, the traditional syntax creates UNNECESSARY complexity:
• public - Who else would call main? The JVM needs it, period.
• static - Required because there's no object yet. Confusing for beginners!
• void - main doesn't return anything useful to the JVM.
• String[] args - Command-line args you rarely use while learning.
• System.out.println -- IO.println() is the modern equivalent, shorter and cleaner.

Modern Java's "compact source files" let you focus on WHAT the code does, not ceremony.

TRADITIONAL (all the ceremony):
public class HelloWorld {
    public static void main(String[] args) {
        System.out.println("Hello!");
    }
}

MODERN (just the logic):
void main() {
    IO.println("Hello!");
}

Behind the scenes, Java still creates the class and static main - it just does it FOR you. When you're ready for larger programs with multiple classes, you'll use the full syntax.