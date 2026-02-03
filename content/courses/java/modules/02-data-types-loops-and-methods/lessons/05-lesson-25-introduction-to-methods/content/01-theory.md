---
type: "THEORY"
title: "The Problem: Copy-Pasting Code is Dangerous"
---

Imagine you need to greet different people in your program:

IO.println("Hello, Alice!");
IO.println("Welcome, Alice!");
IO.println("Goodbye, Alice!");

IO.println("Hello, Bob!");
IO.println("Welcome, Bob!");
IO.println("Goodbye, Bob!");

This is:
❌ Repetitive (same pattern, different name)
❌ Hard to change (what if you want to add another line?)
❌ Error-prone (easy to make typos)

What if you could define the greeting pattern ONCE, and then just say:
greet("Alice");
greet("Bob");

This is what METHODS (also called functions) do! They let you package code into reusable chunks.