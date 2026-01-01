---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Changing text content
let heading = document.querySelector('h1');
heading.textContent = 'New Heading';

let paragraph = document.querySelector('p');
paragraph.textContent = 'New paragraph text';

// Changing inline styles (CSS properties)
let box = document.querySelector('.box');
box.style.color = 'red';  // Text color
box.style.backgroundColor = 'yellow';  // Background (note camelCase!)
box.style.fontSize = '24px';  // Font size
box.style.width = '200px';  // Width
box.style.padding = '20px';  // Padding
box.style.border = '2px solid black';  // Border

// CSS properties with hyphens become camelCase in JavaScript
// CSS: background-color → JavaScript: backgroundColor
// CSS: font-size → JavaScript: fontSize
// CSS: margin-top → JavaScript: marginTop

// Working with classes (better than inline styles!)
let element = document.querySelector('.item');

// Add a class
element.classList.add('active');

// Remove a class
element.classList.remove('inactive');

// Toggle a class (add if missing, remove if present)
element.classList.toggle('highlighted');

// Check if class exists
if (element.classList.contains('active')) {
  console.log('Element is active');
}

// Practical example: Dark mode toggle
let button = document.querySelector('#darkModeBtn');
button.addEventListener('click', function() {
  document.body.classList.toggle('dark-mode');
});

// Changing multiple styles at once
let card = document.querySelector('.card');
Object.assign(card.style, {
  width: '300px',
  height: '200px',
  backgroundColor: '#f0f0f0',
  borderRadius: '10px',
  padding: '20px'
});

// Getting current styles
let computedStyle = window.getComputedStyle(element);
console.log(computedStyle.color);  // Current color
```
