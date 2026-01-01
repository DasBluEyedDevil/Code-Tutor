---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting camelCase for CSS properties:
   element.style.background-color = 'red';  // SYNTAX ERROR
   element.style.backgroundColor = 'red';  // CORRECT

2. Forgetting units:
   element.style.width = 200;  // WRONG - no effect
   element.style.width = '200px';  // CORRECT

3. Overusing inline styles instead of classes:
   Bad:
   element.style.color = 'red';
   element.style.fontWeight = 'bold';
   
   Better:
   element.classList.add('error');
   // Define .error in CSS

4. Confusing textContent, innerHTML, and innerText:
   textContent: Safest, pure text
   innerHTML: Can include HTML (security risk if user input!)
   innerText: Respects CSS (slower)

5. Not checking if element exists:
   let el = document.querySelector('.missing');
   el.textContent = 'Hi';  // ERROR if el is null!
   
   Always check:
   if (el) {
     el.textContent = 'Hi';
   }