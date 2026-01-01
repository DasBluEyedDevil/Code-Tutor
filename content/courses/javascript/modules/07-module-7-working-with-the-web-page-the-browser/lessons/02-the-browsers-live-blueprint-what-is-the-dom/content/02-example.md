---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// The DOM is a tree structure
// HTML:
// <html>
//   <body>
//     <h1>Title</h1>
//     <p>Paragraph</p>
//   </body>
// </html>
//
// Becomes this tree:
// document
//   └─ html
//      └─ body
//         ├─ h1 ("Title")
//         └─ p ("Paragraph")

// Accessing the DOM
console.log(document);  // The entire page
console.log(document.body);  // The <body> element
console.log(document.title);  // Page title

// Finding elements (we'll learn more about this next)
let element = document.getElementById('myId');
let elements = document.getElementsByClassName('myClass');
let firstDiv = document.querySelector('div');
let allDivs = document.querySelectorAll('div');

// The DOM is LIVE - changes appear immediately
let heading = document.getElementById('title');
heading.textContent = 'New Title';  // Page updates instantly!

// DOM nodes have properties
console.log(heading.tagName);  // 'H1'
console.log(heading.id);  // 'title'
console.log(heading.className);  // CSS classes

// DOM nodes have relationships (tree structure)
let parent = heading.parentElement;  // Element above
let children = parent.children;  // Elements below
let nextSibling = heading.nextElementSibling;  // Next element at same level
```
