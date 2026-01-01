---
type: "KEY_POINT"
title: "Dual Syntax: Switch Expressions vs Traditional Switch"
---

You'll see TWO styles for switch in Java:

MODERN SYNTAX (Java 14+):
String result = switch (day) {
    case "MON", "TUE", "WED" -> "Weekday";
    case "SAT", "SUN" -> "Weekend";
    default -> "Unknown";
};

Use this when: Assigning values, Java 14+, cleaner code.

TRADITIONAL SYNTAX (Java 8+):
String result;
switch (day) {
    case "MON":
    case "TUE":
    case "WED":
        result = "Weekday";
        break;
    case "SAT":
    case "SUN":
        result = "Weekend";
        break;
    default:
        result = "Unknown";
        break;
}

Use this when: Working with Java 8-13, or when you need fall-through behavior.

KEY DIFFERENCES:
- Arrow syntax (->) = no fall-through, can return values directly
- Colon syntax (:) = requires 'break', can fall-through intentionally
- Modern: multiple cases on one line (case "A", "B" ->)
- Traditional: cases must be stacked

BOTH ARE VALID! Arrow syntax is preferred in modern Java for clarity and safety.