---
type: "ANALOGY"
title: "The Smart Label Maker"
---

Imagine you're moving into a new house. You have a stack of boxes and a label maker.

1.  **Strict Labeling (Explicit Types):** You pick up a box and type out a label: "KITCHEN ITEMS ONLY." From that point on, you are mentally committed. If you find a stray screwdriver (Tools), you know it doesn't belong in that box.
2.  **Smart Guessing (Inference):** You start filling a box with books. Your label maker has a sensor and says, "I see you're putting books in here. I'll automatically label this as a BOOK box for you." You didn't have to type it, but the rule is now set.

In TypeScript, **Types** are those labels. 
*   **Explicit Types:** You tell TypeScript: `let age: number`.
*   **Inferred Types:** You just say `let age = 25`, and TypeScript is smart enough to know that `age` must be a `number`.

This labeling system ensures that you never accidentally try to "calculate the square root of a shoe" or "send an email to a number."
