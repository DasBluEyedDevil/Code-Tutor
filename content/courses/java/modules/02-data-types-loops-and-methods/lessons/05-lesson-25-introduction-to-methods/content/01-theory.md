---
type: "THEORY"
title: "The Problem: Copy-Pasting Code is Dangerous"
---

Imagine you need to greet different people in your program:

System.out.println("Hello, Alice!");
System.out.println("Welcome, Alice!");
System.out.println("Goodbye, Alice!");

System.out.println("Hello, Bob!");
System.out.println("Welcome, Bob!");
System.out.println("Goodbye, Bob!");

This is:
❌ Repetitive (same pattern, different name)
❌ Hard to change (what if you want to add another line?)
❌ Error-prone (easy to make typos)

What if you could define the greeting pattern ONCE, and then just say:
greet("Alice");
greet("Bob");

This is what METHODS (also called functions) do! They let you package code into reusable chunks.