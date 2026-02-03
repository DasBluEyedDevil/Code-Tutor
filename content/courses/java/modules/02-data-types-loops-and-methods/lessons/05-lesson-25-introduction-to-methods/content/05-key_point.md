---
type: "KEY_POINT"
title: "void vs Returning a Value"
---

When to use 'void' (no return value):
✓ Method performs an action (print, modify something)
✓ You don't need a result back

public static void printBanner() {
    IO.println("============");
    IO.println("WELCOME");
    IO.println("============");
}

When to return a value:
✓ Method calculates something
✓ You need the result for later

public static double calculateTip(double bill, double percent) {
    return bill * (percent / 100);
}

double tip = calculateTip(50.0, 15);  // tip is 7.5

Key difference:
- void methods DO something
- Returning methods CALCULATE and give you something back