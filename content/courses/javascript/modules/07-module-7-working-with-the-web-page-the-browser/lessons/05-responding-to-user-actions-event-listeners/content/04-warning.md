---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Calling the function immediately:
   button.addEventListener('click', myFunction());  // WRONG - runs now!
   button.addEventListener('click', myFunction);  // CORRECT - runs on click

2. Forgetting event.preventDefault() for forms:
   form.addEventListener('submit', function(event) {
     // Form submits and page reloads before your code runs!
   });
   
   Must prevent default:
   form.addEventListener('submit', function(event) {
     event.preventDefault();  // Now your code can run
   });

3. Confusing input vs change:
   'input' - Fires on every keystroke (real-time)
   'change' - Fires when field loses focus (final value)

4. Not getting input values correctly:
   let value = input;  // WRONG - this is the element!
   let value = input.value;  // CORRECT - this is the text

5. Trying to remove anonymous function:
   element.addEventListener('click', function() { });
   element.removeEventListener('click', function() { });  // Doesn't work!
   
   Must use named function:
   function handleClick() { }
   element.addEventListener('click', handleClick);
   element.removeEventListener('click', handleClick);  // Works!

6. Forgetting 'this' context in arrow functions:
   // Traditional function: 'this' is the element
   button.addEventListener('click', function() {
     console.log(this);  // The button
   });
   
   // Arrow function: 'this' is lexical scope
   button.addEventListener('click', () => {
     console.log(this);  // NOT the button!
   });