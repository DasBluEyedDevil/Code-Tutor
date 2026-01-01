---
type: "KEY_POINT"
title: "When to Use Each Loop Style"
---

TRADITIONAL FOR: Use when you need the index
• Modifying elements: array[i] = newValue
• Comparing adjacent items: array[i] vs array[i+1]
• Iterating in reverse or by steps
• Needing to know "which" iteration you're on

for (int i = 0; i < scores.length; i++) {
    scores[i] = scores[i] * 2;  // Need index to modify
}

ENHANCED FOR: Use when you just process items
• Reading values without changing them
• Processing each item the same way
• When you don't need the position

for (var score : scores) {
    total += score;  // Just reading, no index needed
}

QUICK REFERENCE:
• Need index? → Traditional for
• Just processing each item? → Enhanced for (for-each)
• Unknown iterations? → while
• Modern collections? → Consider streams (covered later)