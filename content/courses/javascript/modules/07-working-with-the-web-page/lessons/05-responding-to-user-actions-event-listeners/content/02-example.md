---
type: "EXAMPLE"
title: "Handling User Input"
---

```html
<button id="click-me">Click Me!</button>
<input type="text" id="name-input" placeholder="Type your name...">
<p id="output"></p>

<form id="my-form">
  <button type="submit">Submit Form</button>
</form>

<script>
  const btn = document.querySelector('#click-me');
  const input = document.querySelector('#name-input');
  const output = document.querySelector('#output');
  const form = document.querySelector('#my-form');

  // 1. Click Event
  btn.addEventListener('click', () => {
    alert('Button was clicked!');
  });

  // 2. Input Event (fires every time you type)
  input.addEventListener('input', (event) => {
    // The 'event' object contains info about what happened
    // event.target is the element that triggered the event (the input)
    output.textContent = `You are typing: ${event.target.value}`;
  });

  // 3. Submit Event and preventing default behavior
  form.addEventListener('submit', (e) => {
    e.preventDefault(); // STOP the page from refreshing!
    console.log("Form submitted, but page didn't refresh.");
  });

  // 4. Mouse Events
  output.addEventListener('mouseenter', () => {
    output.style.color = 'red';
  });
  output.addEventListener('mouseleave', () => {
    output.style.color = 'black';
  });
</script>
```