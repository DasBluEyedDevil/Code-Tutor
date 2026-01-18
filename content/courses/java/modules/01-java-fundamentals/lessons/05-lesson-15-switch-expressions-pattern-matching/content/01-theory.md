---
type: "THEORY"
title: "The Problem: Clunky Switch Statements"
---

Traditional `switch` statements in Java are verbose and error-prone.

```java
String dayType;
switch (day) {
    case "MONDAY":
    case "TUESDAY":
    case "WEDNESDAY":
    case "THURSDAY":
    case "FRIDAY":
        dayType = "Weekday";
        break;  // Easy to forget!
    case "SATURDAY":
    case "SUNDAY":
        dayType = "Weekend";
        break;
    default:
        dayType = "Unknown";
        break;
}
```

### Problems
1.  **Fall-through bugs**: If you forget `break`, the code "falls through" to the next case.
2.  **Verbosity**: Requires a lot of code for simple mappings.
3.  **Statement, not Expression**: Can't return a value directly.

Java 14+ introduced **Switch Expressions** to fix these problems.
