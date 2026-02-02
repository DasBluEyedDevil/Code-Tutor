---
type: "ANALOGY"
title: "The Recipe and the Contract"
---

Imagine you're hiring a catering company for a wedding.

1.  **The Contract (Interface):** You provide a list of requirements. "The caterer MUST provide a main dish, a dessert, and three servers." This is a **Contract**. You don't care how they make the food or where they find the servers, but if any of those things are missing on the big day, the contract is broken.
2.  **The Recipe (Type Alias):** You give them a specific recipe for your grandmother's secret sauce. The recipe is a **Definition** of what that sauce *is*. It can't be changed or "extended" by another recipe; it is simply a label for that specific list of ingredients.

In TypeScript:
*   **Interfaces** are best for describing "Shapes"—the properties and methods an object should have. They are very flexible and can be expanded later.
*   **Type Aliases** are best for creating unique labels for any type of data (strings, numbers, unions, or even objects).

Both act as **Blueprints** that ensure your data remains consistent across your entire application.
