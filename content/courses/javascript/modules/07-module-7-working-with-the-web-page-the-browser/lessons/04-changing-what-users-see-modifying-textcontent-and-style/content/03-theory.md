---
type: "THEORY"
title: "Manipulating Elements"
---

Once you have found an element using `querySelector`, you can use its properties to change how it looks and what it contains.

### 1. `textContent` vs. `innerHTML`
*   **`textContent`:** Changes the plain text inside an element. It treats everything you give it as literal text, making it safe from security risks.
*   **`innerHTML`:** Allows you to insert **actual HTML tags**. 
    *   `el.textContent = '<b>Hi</b>'` displays the literal words "<b>Hi</b>".
    *   `el.innerHTML = '<b>Hi</b>'` displays a **bold** Hi.
    *   **Security Warning:** Never use `innerHTML` with text provided by a user, as it can lead to XSS (Cross-Site Scripting) attacks!

### 2. The `style` Property
Every element has a `.style` object. You can set any CSS property here.
*   **Rule:** Properties that have a dash in CSS (`font-size`) must use `camelCase` in JavaScript (`fontSize`).
*   **Inline Styles:** Setting `.style` adds the style directly to the HTML tag (`style="..."`). This gives it very high priority, overriding most CSS files.

### 3. The `classList` API
This is the professional way to handle styles. Instead of micromanaging colors and pixels in your JavaScript, you define those styles in your CSS file and just "toggle" the class on or off.
*   `.classList.add('name')`
*   `.classList.remove('name')`
*   `.classList.toggle('name')`
*   `.classList.contains('name')` — Returns `true` or `false`.

### 4. Attributes
You can also change other parts of a tag, like the `src` of an image or the `href` of a link:
`myImage.src = 'new-photo.jpg';`
`myLink.href = 'https://google.com';`
