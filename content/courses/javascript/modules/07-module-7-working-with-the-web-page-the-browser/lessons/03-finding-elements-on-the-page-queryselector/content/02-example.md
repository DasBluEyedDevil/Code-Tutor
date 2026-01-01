---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// querySelector finds the FIRST match
// querySelectorAll finds ALL matches

// By ID (same as getElementById, but with # prefix)
let title = document.querySelector('#title');

// By class (same as getElementsByClassName, but with . prefix)
let firstItem = document.querySelector('.item');
let allItems = document.querySelectorAll('.item');

// By tag name
let firstParagraph = document.querySelector('p');
let allParagraphs = document.querySelectorAll('p');

// By attribute
let input = document.querySelector('[type="email"]');
let required = document.querySelectorAll('[required]');

// Combining selectors
let redButton = document.querySelector('button.red');
let containerDiv = document.querySelector('div#container');

// Descendant selectors (inside)
let linkInNav = document.querySelector('nav a');
let itemsInList = document.querySelectorAll('ul.menu li');

// Direct children (>)
let directChild = document.querySelector('div > p');

// Pseudo-selectors
let firstChild = document.querySelector('li:first-child');
let lastChild = document.querySelector('li:last-child');
let evenItems = document.querySelectorAll('li:nth-child(even)');

// Complex example
let specificButton = document.querySelector('div.container button.primary:not(.disabled)');
// Finds: button with class 'primary', inside div with class 'container', not having class 'disabled'

// querySelectorAll returns NodeList (array-like)
let buttons = document.querySelectorAll('button');
console.log(buttons.length);  // Number of buttons
buttons.forEach(btn => {
  console.log(btn.textContent);
});
```
