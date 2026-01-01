---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting quotes around IDs:
   document.getElementById(heading)  // WRONG - thinks heading is a variable
   document.getElementById('heading')  // CORRECT - string literal

2. Running JavaScript before HTML loads:
   <script>document.getElementById('myButton')</script>
   <button id="myButton">Click</button>
   The script runs before button exists! Put scripts at end of <body>

3. Typos in IDs (case-sensitive!):
   HTML: <div id="myDiv"></div>
   JS: document.getElementById('mydiv')  // WRONG - lowercase d
   JS: document.getElementById('myDiv')  // CORRECT

4. Confusing textContent, innerHTML, and value:
   - textContent: Text only (safe, no HTML)
   - innerHTML: HTML content (can be dangerous!)
   - value: For form inputs (<input>, <textarea>)

5. Forgetting to attach event listener:
   button.addEventListener('click', myFunction)  // Correct
   button.addEventListener('click', myFunction())  // WRONG - calls immediately!