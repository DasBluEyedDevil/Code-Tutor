---
type: "ANALOGY"
title: "The Family Tree"
---

When a browser reads your HTML, it doesn't just display it as a flat document. It builds a **map** called the **Document Object Model (DOM)**. 

Think of the DOM as a **Family Tree**:
*   **The Root:** The `<html>` tag is the great-grandparent of everything.
*   **The Parents:** Inside `<html>`, you have the `<head>` and the `<body>`.
*   **The Children:** Inside the `<body>`, you might have a `<div>` which contains an `<h1>` and a `<p>`.

In this tree, every single thing (a tag, a piece of text, even a comment) is called a **Node**. 

#### Why do we call it a "Model"?
Because it's a model that JavaScript can play with. If you tell JavaScript to "delete the second child of the body," it knows exactly which branch of the tree to cut off. If you tell it to "change the color of all siblings of the title," it follows the tree branches to find them.

The DOM is the **bridge** between your static code and the live, interactive experience.
