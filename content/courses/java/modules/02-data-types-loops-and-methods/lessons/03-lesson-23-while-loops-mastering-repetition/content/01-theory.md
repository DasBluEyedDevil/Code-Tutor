---
type: "THEORY"
title: "The Problem: Repeating Tasks"
---

Imagine you need to print "Hello" 100 times. You COULD write:

System.out.println("Hello");
System.out.println("Hello");
System.out.println("Hello");
// ...97 more times!

This is:
❌ Tedious to write
❌ Error-prone (did you type exactly 100?)
❌ Hard to change (what if you need 1000?)

Real-world examples of repetition:
• A game runs the same game loop thousands of times
• A server checks for new requests continuously
• You scroll through a list of items one by one

You need a way to say: "KEEP DOING THIS until a condition is met."

This is what LOOPS do!