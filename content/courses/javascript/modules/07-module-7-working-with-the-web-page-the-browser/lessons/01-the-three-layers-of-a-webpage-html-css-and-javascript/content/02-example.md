---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
<!-- HTML: The structure (skeleton) -->
<!DOCTYPE html>
<html>
<head>
  <title>My Page</title>
  <!-- CSS: The styling (clothes) -->
  <style>
    h1 {
      color: blue;        /* Make heading blue */
      font-size: 32px;    /* Make it big */
    }
    .highlight {
      background-color: yellow;
    }
  </style>
</head>
<body>
  <h1 id="title">Hello, World!</h1>
  <button id="myButton">Click Me!</button>
  <p id="message"></p>

  <!-- JavaScript: The behavior (brain) -->
  <script>
    // This code runs when the page loads
    
    // Find the button element
    let button = document.getElementById('myButton');
    
    // Make something happen when button is clicked
    button.addEventListener('click', function() {
      // Find the message paragraph
      let message = document.getElementById('message');
      
      // Change its text content
      message.textContent = 'Button was clicked!';
      
      // Add styling class
      message.className = 'highlight';
    });
  </script>
</body>
</html>
```
