---
type: "THEORY"
title: "Unnamed Variables (_)"
---

Sometimes you MUST declare a variable but you don't actually use it. This often happens in loops or exception handling.

### The Problem
```java
try {
    int x = Integer.parseInt("abc");
} catch (NumberFormatException e) { // 'e' is never used!
    System.out.println("Not a number");
}
```

### The Solution: Unnamed Variables (`_`)
Java 21 allows you to use `_` (underscore) for variables you don't need.

```java
try {
    int x = Integer.parseInt("abc");
} catch (NumberFormatException _) { // Clearly signals: unused
    System.out.println("Not a number");
}
```

This also works in loops:
```java
for (String _ : names) {
    count++; // We just want to count, not use the name
}
```
