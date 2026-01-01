---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Modifying DOM elements:

**1. Changing Text Content:**

// textContent (recommended - safe, text only)
element.textContent = 'New text';

// innerHTML (can include HTML, but risky!)
element.innerHTML = '<b>Bold text</b>';

// innerText (similar to textContent, but respects CSS visibility)
element.innerText = 'New text';

**Best practice**: Use textContent unless you specifically need HTML

**2. Changing Inline Styles:**

element.style.propertyName = 'value';

Key points:
- CSS properties with hyphens become camelCase
- Values are strings (include units: '20px', '50%')
- This adds inline styles (highest priority)

Examples:
CSS Property        → JavaScript Property
color               → color
background-color    → backgroundColor
font-size           → fontSize
margin-top          → marginTop
border-radius       → borderRadius
z-index             → zIndex

**3. Working with Classes (Preferred Method):**

// classList provides methods to manipulate classes
element.classList.add('class1', 'class2');
element.classList.remove('class1');
element.classList.toggle('class1');  // Add if missing, remove if present
element.classList.contains('class1');  // Returns true/false
element.classList.replace('old', 'new');

// Old way (not recommended):
element.className = 'class1 class2';  // Replaces all classes

**Why use classes instead of inline styles?**

✓ Separation of concerns (CSS in CSS, JS in JS)
✓ Easier to maintain
✓ Can change multiple properties at once
✓ Better performance
✓ CSS can be cached
✓ Easier to override

Example:
// Instead of:
element.style.color = 'red';
element.style.fontWeight = 'bold';
element.style.fontSize = '20px';

// Do this:
element.classList.add('error');

// And in CSS:
.error {
  color: red;
  font-weight: bold;
  font-size: 20px;
}

**4. Reading Styles:**

// Inline styles only
element.style.color  // Only returns inline styles

// All computed styles (including CSS)
let styles = window.getComputedStyle(element);
styles.color
styles.fontSize