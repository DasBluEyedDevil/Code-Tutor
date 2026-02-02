---
type: "WARNING"
title: "Modification Pitfalls"
---

### 1. The `innerHTML` Security Risk
NEVER use `innerHTML` to display data that came from a user (like a comment or a username). An attacker could provide a "username" that contains a `<script>` tag, allowing them to steal passwords from other users visiting your site. Always prefer `textContent`.

### 2. `textContent` vs. `value`
If you are working with an **Input** field (like a text box), `textContent` won't work!
*   **Wrong:** `myInput.textContent = "Hello";`
*   **Right:** `myInput.value = "Hello";`

### 3. CamelCase for Styles
Remember that you can't use dashes in JavaScript property names.
*   **Wrong:** `el.style.background-color = 'red';` (JavaScript thinks you are trying to subtract a variable called `color`).
*   **Right:** `el.style.backgroundColor = 'red';`

### 4. Overwriting `className`
There is an older property called `className` (`el.className = "active"`). Avoid using this because it **overwrites** every class on the element. If your element had 3 classes and you use `className`, the other 2 will be deleted. Always use `classList.add()` instead.
