---
type: "THEORY"
title: "The Query API"
---

The `querySelector` methods are the modern standard for finding elements in the DOM. They allow you to use the full power of CSS selectors directly in your JavaScript.

### 1. `document.querySelector(selector)`
*   **Returns:** The **first** element that matches the selector.
*   **If no match is found:** It returns `null`.
*   **Common Selectors:**
    *   `'#id'` (ID)
    *   `'.class'` (Class)
    *   `'tag'` (Tag name like `div` or `h1`)
    *   `'[attribute]'` (Attribute like `[type="text"]`)

### 2. `document.querySelectorAll(selector)`
*   **Returns:** A **NodeList** containing all elements that match.
*   **If no match is found:** It returns an empty list `[]`.
*   **NodeList vs. Array:** A NodeList is "array-like." In modern browsers, you can use `.forEach()` on it, but you might need to convert it to a real array (`[...list]`) if you want to use methods like `map` or `filter`.

### 3. The Power of CSS
Because these methods use CSS logic, you can be very specific:
*   `'#nav .active'` — Find an element with class "active" inside the element with ID "nav."
*   `'button:disabled'` — Find all buttons that are currently disabled.

### 4. Performance
`querySelector` is slightly slower than older methods like `getElementById`, but the difference is so tiny (microseconds) that it doesn't matter for 99% of applications. The benefit of using a single, powerful API far outweighs the performance cost.
