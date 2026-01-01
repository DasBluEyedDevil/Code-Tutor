---
type: "THEORY"
title: "The Problem: How Do You Know It Works?"
---

You've written a method:

public static int add(int a, int b) {
    return a + b;
}

How do you KNOW it works correctly for all inputs?
- What if a is negative?
- What if both are zero?
- What if they're very large numbers?

Manual testing (running main and checking output) is:
❌ Tedious
❌ Error-prone
❌ Doesn't scale

Professional developers write AUTOMATED TESTS that run instantly and check everything.