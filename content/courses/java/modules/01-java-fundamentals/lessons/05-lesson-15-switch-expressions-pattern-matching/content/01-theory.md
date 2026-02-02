---
type: "THEORY"
title: "The Problem: Clunky Switch Statements"
---

Traditional switch statements in Java are verbose and error-prone:

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

Problems:
1. Must remember 'break' or fall-through bugs occur
2. Can't return values directly from switch
3. Very verbose for simple mappings
4. No compile-time exhaustiveness checking

Switch expressions were finalized in Java 14 (JEP 361), and pattern matching for switch was finalized in Java 21 (JEP 441). These features fix all of the problems above!