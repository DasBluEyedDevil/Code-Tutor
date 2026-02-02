---
type: "THEORY"
title: "Your First Java Program"
---

In Java 25+, writing your first program is simple using compact source files (JEP 512):

void main() {
    IO.println("Hello, World!");
}

That's it! Just 3 lines. Let's break it down:
- void main() - the entry point of your program (no class declaration needed!)
- IO.println() - prints text to the console using Java's built-in IO class
- The semicolon ';' ends the instruction (like a period ends a sentence)

The IO class lives in java.lang and is automatically available in every Java file—no import needed.

KEY RULE: Java is case-sensitive. 'Main' and 'main' are different. Be precise!