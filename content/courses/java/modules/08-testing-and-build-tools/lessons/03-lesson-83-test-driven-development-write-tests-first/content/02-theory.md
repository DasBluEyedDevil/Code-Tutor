---
type: "THEORY"
title: "Test-Driven Development (TDD) - A Revolutionary Approach"
---

TDD FLIPS the process:

1. Write a FAILING test first (describes what you want)
2. Write MINIMAL code to make test pass
3. Refactor (improve code quality)
4. Repeat

The cycle: RED → GREEN → REFACTOR

🔴 RED: Write a failing test
@Test
void testCalculateDiscount() {
    double result = PriceCalculator.calculateDiscount(100, 10);
    assertEquals(90, result);
}
// Fails: PriceCalculator doesn't exist yet!

🟢 GREEN: Write just enough code to pass
public class PriceCalculator {
    public static double calculateDiscount(double price, double percent) {
        return price - (price * percent / 100);
    }
}
// Test passes!

🔵 REFACTOR: Clean up code
// Code is already clean, move to next test