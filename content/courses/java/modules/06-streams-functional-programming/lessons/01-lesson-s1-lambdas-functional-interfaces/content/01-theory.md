---
type: "THEORY"
title: "The Problem: Verbose Anonymous Classes"
---

Before lambdas, passing behavior as a parameter required anonymous inner classes:

// Old way: 5 lines just to compare two strings!
Collections.sort(names, new Comparator<String>() {
    @Override
    public int compare(String a, String b) {
        return a.compareTo(b);
    }
});

This is verbose, hard to read, and obscures the actual logic (just one line: a.compareTo(b)).

Lambdas solve this by allowing you to express behavior concisely:

// New way: 1 line!
Collections.sort(names, (a, b) -> a.compareTo(b));

Lambdas are anonymous functions - code blocks you can pass around like data.