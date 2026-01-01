---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Event listeners syntax:

element.addEventListener(eventType, callbackFunction);
│                        │          │
│                        │          └─ Function to run when event fires
│                        └──────────── Type of event to listen for
└───────────────────────────────────── Element to listen on

**Common Event Types:**

Mouse Events:
- 'click' - Element is clicked
- 'dblclick' - Element is double-clicked
- 'mouseenter' - Mouse enters element
- 'mouseleave' - Mouse leaves element
- 'mousemove' - Mouse moves over element
- 'mousedown' - Mouse button pressed
- 'mouseup' - Mouse button released

Keyboard Events:
- 'keydown' - Key is pressed down
- 'keyup' - Key is released
- 'keypress' - Key is pressed (deprecated, use keydown)

Form Events:
- 'submit' - Form is submitted
- 'input' - Input value changes (real-time)
- 'change' - Input value changes (on blur)
- 'focus' - Element receives focus
- 'blur' - Element loses focus

Window/Document Events:
- 'load' - Page fully loaded
- 'DOMContentLoaded' - HTML loaded (before images)
- 'resize' - Window resized
- 'scroll' - Page scrolled

**Event Object:**

The callback receives an event object:

element.addEventListener('click', function(event) {
  // event contains information about the event
});

Useful properties:
- event.type - Type of event ('click', 'keydown', etc.)
- event.target - Element that triggered the event
- event.currentTarget - Element the listener is attached to
- event.key - Key that was pressed (keyboard events)
- event.clientX, event.clientY - Mouse position
- event.preventDefault() - Prevent default behavior
- event.stopPropagation() - Stop event bubbling

**Preventing Defaults:**

form.addEventListener('submit', function(event) {
  event.preventDefault();  // Stop form from actually submitting
  // Handle with JavaScript instead
});

link.addEventListener('click', function(event) {
  event.preventDefault();  // Stop link from navigating
  // Do something else
});

**Arrow Functions:**

// Traditional function
element.addEventListener('click', function() {
  console.log('Clicked');
});

// Arrow function (more modern)
element.addEventListener('click', () => {
  console.log('Clicked');
});

// With event parameter
element.addEventListener('click', (event) => {
  console.log(event.target);
});