---
type: "THEORY"
title: "Part 1: Records - Lightweight Data Grouping"
---


### What Are Records?

**Conceptual First:**
Imagine you want to return two values from a function - like a person's name AND their age. Before Dart 3, you had to either:
- Create a whole class just for two values (overkill!)
- Use a List or Map (loses type safety)
- Return multiple values awkwardly

**Records** solve this elegantly! They're like lightweight, immutable containers for multiple values.

**Jargon:**
- **Record**: A fixed-size, immutable collection of values
- **Positional fields**: Fields accessed by position ($1, $2, etc.)
- **Named fields**: Fields accessed by name

