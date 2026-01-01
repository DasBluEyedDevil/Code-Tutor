---
type: "EXAMPLE"
title: "Visualizing the DOM"
---

This is what a simple HTML structure looks like after the browser turns it into a DOM Tree.

```html
<!-- THE HTML -->
<body>
  <div id="wrapper">
    <h1>Hello World</h1>
    <p>This is a <span>special</span> paragraph.</p>
  </div>
</body>

<!-- THE DOM TREE (Conceptual) -->
<!--
document
 └── html
      └── body
           └── div#wrapper
                ├── h1
                │    └── "Hello World" (Text Node)
                └── p
                     ├── "This is a " (Text Node)
                     ├── span
                     │    └── "special" (Text Node)
                     └── " paragraph." (Text Node)
-->

<script>
  // In JavaScript, we access this tree via the 'document' object
  console.log(document.body); // Shows the <body> object
  console.log(document.body.childNodes); // Shows all children of the body
</script>
```