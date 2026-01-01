---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting the . or # prefix:
   document.querySelector('myClass')  // WRONG - looks for <myClass> tag
   document.querySelector('.myClass')  // CORRECT - looks for class

2. Expecting querySelectorAll to be an array:
   let items = document.querySelectorAll('.item');
   items.map(...)  // ERROR - NodeList doesn't have map
   
   Convert first:
   Array.from(items).map(...)
   // Or use forEach (works on NodeList):
   items.forEach(...)

3. Confusing querySelector with querySelectorAll:
   querySelector returns: first match or null
   querySelectorAll returns: NodeList of all matches (can be empty)

4. Complex selectors with typos:
   'div.container button.primary'  // Correct
   'div .container button .primary'  // WRONG - extra spaces change meaning

5. Not checking for null:
   let element = document.querySelector('.doesnt-exist');
   element.textContent = 'Hi';  // ERROR - element is null!
   
   Always check:
   if (element) {
     element.textContent = 'Hi';
   }