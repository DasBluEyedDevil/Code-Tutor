---
type: "EXAMPLE"
title: "The Three Layers in Code"
---

While we usually write these in separate files, here is how they look when working together in a single conceptual "web document."

```html
<!-- 1. HTML: The Structure -->
<h1 id="main-title">Welcome to my page!</h1>
<p>This is a paragraph of text.</p>
<button id="color-button">Change Color</button>

<!-- 2. CSS: The Styling (Usually in a .css file) -->
<style>
  #main-title {
    color: darkblue;
    font-family: sans-serif;
  }
  
  .highlight {
    background-color: yellow;
    font-weight: bold;
  }
</style>

<!-- 3. JavaScript: The Behavior (Usually in a .js file) -->
<script>
  // This is the "Director" in action:
  const button = document.querySelector('#color-button');
  const title = document.querySelector('#main-title');

  button.addEventListener('click', () => {
    // Changing the CSS class using JavaScript!
    title.classList.toggle('highlight');
  });
</script>
```