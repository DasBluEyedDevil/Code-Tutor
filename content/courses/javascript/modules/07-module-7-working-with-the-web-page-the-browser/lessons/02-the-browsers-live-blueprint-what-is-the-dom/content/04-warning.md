---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Confusing the DOM with HTML:
   - HTML: Static code you write
   - DOM: Live structure in browser
   - Changing HTML file doesn't change running page
   - Changing DOM changes page immediately

2. Null reference errors:
   let element = document.getElementById('wrong-id');
   element.textContent = 'Hi';  // ERROR - element is null!
   
   Always check:
   if (element !== null) {
     element.textContent = 'Hi';
   }

3. Mixing up textContent and innerHTML:
   element.textContent = '<b>Bold</b>';  // Shows literal <b> tags
   element.innerHTML = '<b>Bold</b>';  // Renders as bold
   
   Use textContent for safety (prevents XSS attacks)

4. Timing issues:
   If JavaScript runs before HTML loads, elements don't exist yet
   Solution: Put <script> at end of <body> or use DOMContentLoaded event

5. getElementsByClassName returns collection, not array:
   let items = document.getElementsByClassName('item');
   items.forEach(...)  // ERROR - not an array!
   Array.from(items).forEach(...)  // Correct