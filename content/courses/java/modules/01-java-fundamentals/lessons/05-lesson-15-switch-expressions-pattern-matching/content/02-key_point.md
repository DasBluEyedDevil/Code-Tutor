---
type: "KEY_POINT"
title: "Switch Expressions with Arrow Syntax"
---

Modern Java uses the arrow (->) syntax for cleaner switches:

// Old way (statement):
switch (day) {
    case "MONDAY":
        IO.println("Start of week");
        break;
    // ... more cases
}

// New way (expression with arrows):
String dayType = switch (day) {
    case "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY" -> "Weekday";
    case "SATURDAY", "SUNDAY" -> "Weekend";
    default -> "Unknown";
};

Benefits:
- No break needed (no fall-through)
- Multiple cases can share one arrow
- Returns a value directly
- Much more concise!

Arrow rules:
- Use -> for single expression or block
- No fall-through between cases
- Must cover all possibilities (exhaustive)