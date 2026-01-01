---
type: "THEORY"
title: "Switch Expressions with yield"
---

When you need multiple statements in a case, use a block with 'yield':

int numLetters = switch (day) {
    case "MONDAY", "FRIDAY", "SUNDAY" -> 6;
    case "TUESDAY" -> 7;
    case "THURSDAY", "SATURDAY" -> 8;
    case "WEDNESDAY" -> {
        System.out.println("Middle of week!");
        yield 9;  // 'yield' returns the value from the block
    }
    default -> throw new IllegalArgumentException("Invalid day: " + day);
};

Key points:
- Use { } blocks when you need multiple statements
- Use 'yield' (not 'return') to provide the value
- 'yield' is only used in switch expressions, not statements