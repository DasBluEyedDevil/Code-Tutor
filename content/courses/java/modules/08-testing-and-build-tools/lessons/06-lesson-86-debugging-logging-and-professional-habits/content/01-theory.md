---
type: "THEORY"
title: "The Problem: System.out.println() Everywhere"
---

Beginner debugging:

public void processOrder(Order order) {
    System.out.println("Got order: " + order);  // Debug line
    calculateTotal(order);
    System.out.println("Total: " + order.getTotal());  // Debug line
    saveToDatabase(order);
    System.out.println("Saved!");  // Debug line
}

PROBLEMS:
❌ Output mixed with actual program output
❌ Can't turn debug messages off easily
❌ No way to filter by severity (error vs info)
❌ Forgot to remove debug prints before release
❌ No timestamps or context

Professional solution: LOGGING FRAMEWORKS