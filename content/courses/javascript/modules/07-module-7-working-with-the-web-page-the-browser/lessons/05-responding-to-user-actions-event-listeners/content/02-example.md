---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Basic event listener
let button = document.querySelector('#myButton');

button.addEventListener('click', function() {
  console.log('Button was clicked!');
});

// Common events
let input = document.querySelector('#nameInput');

// 'input' fires when user types
input.addEventListener('input', function(event) {
  console.log('User typed:', event.target.value);
});

// 'change' fires when input loses focus after being changed
input.addEventListener('change', function(event) {
  console.log('Input changed to:', event.target.value);
});

// 'focus' fires when element receives focus
input.addEventListener('focus', function() {
  console.log('Input focused');
});

// 'blur' fires when element loses focus
input.addEventListener('blur', function() {
  console.log('Input lost focus');
});

// Mouse events
let box = document.querySelector('.box');

box.addEventListener('mouseenter', function() {
  console.log('Mouse entered box');
});

box.addEventListener('mouseleave', function() {
  console.log('Mouse left box');
});

box.addEventListener('mousemove', function(event) {
  console.log('Mouse position:', event.clientX, event.clientY);
});

// Keyboard events
document.addEventListener('keydown', function(event) {
  console.log('Key pressed:', event.key);
  
  if (event.key === 'Enter') {
    console.log('Enter key pressed!');
  }
});

// Form events
let form = document.querySelector('#myForm');

form.addEventListener('submit', function(event) {
  event.preventDefault();  // Prevent default form submission
  console.log('Form submitted');
  
  // Get form data
  let formData = new FormData(form);
  console.log(formData.get('username'));
});

// Event object
button.addEventListener('click', function(event) {
  console.log('Event type:', event.type);  // 'click'
  console.log('Target element:', event.target);  // The button
  console.log('Mouse position:', event.clientX, event.clientY);
});

// Removing event listeners
function handleClick() {
  console.log('Clicked!');
}

button.addEventListener('click', handleClick);
button.removeEventListener('click', handleClick);  // Must be same function

// Multiple listeners on same event
button.addEventListener('click', function() {
  console.log('First listener');
});

button.addEventListener('click', function() {
  console.log('Second listener');
});
// Both run when button is clicked!
```
