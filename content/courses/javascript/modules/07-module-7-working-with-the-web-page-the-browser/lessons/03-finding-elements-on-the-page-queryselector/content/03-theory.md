---
type: "THEORY"
title: "Breaking Down the Syntax"
---

querySelector syntax (uses CSS selectors):

**Basic Selectors:**

1. By ID: #idName
   document.querySelector('#myId')
   // Same as: document.getElementById('myId')

2. By class: .className
   document.querySelector('.myClass')
   document.querySelectorAll('.myClass')  // All matches

3. By tag: tagname
   document.querySelector('p')
   document.querySelectorAll('div')

4. By attribute: [attribute]
   document.querySelector('[href]')  // Has href attribute
   document.querySelector('[type="submit"]')  // type="submit"

**Combinators:**

1. Descendant: space (anywhere inside)
   document.querySelector('div p')  // p inside any div

2. Direct child: >
   document.querySelector('div > p')  // p directly inside div

3. Multiple selectors: comma
   document.querySelectorAll('h1, h2, h3')  // All headings

4. Combined: no space
   document.querySelector('button.primary')  // button with class 'primary'

**Pseudo-selectors:**

1. :first-child, :last-child
2. :nth-child(n) - The nth child
3. :nth-child(even), :nth-child(odd)
4. :not(selector) - Exclude matches
5. :hover, :focus, :checked (for CSS, less useful in JS)

**Differences from getElementBy...:**

querySelector:
✓ More flexible (any CSS selector)
✓ Consistent syntax
✓ Returns null if not found
✓ Modern and recommended
✗ Slightly slower (negligible)

getElementBy...:
✓ Slightly faster
✓ Returns live collections (auto-updates)
✗ Limited to ID, class, or tag
✗ Different syntax for each

**Best Practice:**
Use querySelector/querySelectorAll for everything. They're more powerful and the syntax is consistent.

**NodeList vs Array:**
querySelectorAll returns a NodeList (not an Array)
- Has .length
- Has .forEach() (modern browsers)
- Doesn't have .map(), .filter(), etc.

Convert to array if needed:
let itemsArray = Array.from(document.querySelectorAll('.item'));