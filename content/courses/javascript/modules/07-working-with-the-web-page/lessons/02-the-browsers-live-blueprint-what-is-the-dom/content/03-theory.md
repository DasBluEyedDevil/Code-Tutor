---
type: "THEORY"
title: "Inside the DOM"
---

The **Document Object Model (DOM)** is a programming interface for web documents. It represents the page so that programs can change the document structure, style, and content.

### 1. Everything is an Object
Each "tag" in your HTML becomes an object in the DOM. These objects have **Properties** (like `id`, `className`, or `innerHTML`) and **Methods** (like `click()`, `focus()`, or `remove()`).

### 2. Nodes vs. Elements
This is a frequent source of confusion:
*   **Node:** The most generic term. A node can be an element tag, the text inside a tag, or even a comment.
*   **Element:** A specific type of node that represents an HTML tag (like `<div>` or `<h1>`).
In 99% of your JavaScript work, you will be interacting with **Elements**.

### 3. The `document` Object
The `document` object is the "entry point" to the DOM. It represents the entire webpage. If you want to find anything on the page, you start with the word `document`.

### 4. Dynamic Nature
The most important thing to understand is that the DOM is **live**. When you change it using JavaScript, the browser re-paints the screen immediately. This allows for features like search results appearing as you type, or "Like" buttons changing color without a page refresh.
