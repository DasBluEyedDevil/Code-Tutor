---
type: "WARNING"
title: "DOM Myths"
---

### 1. DOM is not HTML
If you right-click a page and select "View Page Source," you see the static HTML that was sent from the server. If you select "Inspect," you see the **Live DOM**. These can be very different! JavaScript might have deleted elements, added classes, or changed text since the page loaded.

### 2. The Cost of Speed
Every time you change the DOM, the browser has to do a lot of math to figure out how the rest of the page should move or change color (this is called "Reflow" and "Repaint").
*   **Best Practice:** Don't change the DOM 1,000 times in a row inside a loop. Instead, prepare your changes in JavaScript and update the DOM once at the end.

### 3. Case Sensitivity
While HTML tags are not case-sensitive (`<DIV>` is the same as `<div>`), JavaScript is. If you use a DOM method, you must match the exact case.

### 4. Wait for the Load
If your script runs too early, the DOM Tree might not be fully built yet. Always ensure your scripts are placed correctly or wrapped in a "DOMContentLoaded" check.
