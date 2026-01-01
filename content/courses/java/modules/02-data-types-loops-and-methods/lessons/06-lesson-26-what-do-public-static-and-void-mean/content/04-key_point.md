---
type: "KEY_POINT"
title: "public vs private: Restaurant Kitchen Analogy"
---

Imagine a restaurant:

PUBLIC methods = Menu items (customer-facing):
- orderPizza() ✓ Customer can call this
- orderSalad() ✓ Customer can call this
These are the INTERFACE the public uses.

PRIVATE methods = Kitchen processes (internal):
- chopVegetables() ✗ Customer can't call this
- preheartOven() ✗ Customer can't call this
These are IMPLEMENTATION details customers shouldn't worry about.

In code:

public class Restaurant {
    // PUBLIC - customers can call
    public void orderPizza(String type) {
        prepareOven();        // Call private helper
        makeDough();          // Call private helper
        addToppings(type);    // Call private helper
    }
    
    // PRIVATE - internal methods
    private void prepareOven() { ... }
    private void makeDough() { ... }
    private void addToppings(String type) { ... }
}