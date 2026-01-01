---
type: "THEORY"
title: "Part 2: Understanding 'public' vs 'private'"
---

'public' and 'private' control WHO can access your method.

PUBLIC = Everyone can use it:

public static int add(int a, int b) {
    return a + b;
}

// Any code, anywhere can call this:
int x = MathUtils.add(5, 3);  // Works!

PRIVATE = Only THIS class can use it:

private static int secretCalculation(int x) {
    return x * 2 + 10;  // Internal helper method
}

// Other classes CAN'T call this:
int y = MathUtils.secretCalculation(5);  // COMPILE ERROR!

WHY USE PRIVATE?
- Hide internal implementation details
- Prevent others from depending on internal code
- Make it easier to change later

RULE OF THUMB:
- Methods you want others to use: public
- Helper methods just for your class: private