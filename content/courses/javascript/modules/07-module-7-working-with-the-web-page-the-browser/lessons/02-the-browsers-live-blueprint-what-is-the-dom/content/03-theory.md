---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding the DOM:

**What is the DOM?**
- Browser's representation of the HTML page
- Tree structure of objects
- Each HTML element becomes a JavaScript object
- You can read and modify these objects
- Changes appear instantly on the page

**DOM Tree Structure:**

HTML:
<body>
  <div id="container">
    <h1>Title</h1>
    <p>Text</p>
  </div>
</body>

DOM Tree:
document
  └─ body
     └─ div (id="container")
        ├─ h1 ("Title")
        └─ p ("Text")

**Key DOM Methods:**

1. Finding single elements:
   - document.getElementById('id')
   - document.querySelector('selector')

2. Finding multiple elements:
   - document.getElementsByClassName('class')
   - document.getElementsByTagName('tag')
   - document.querySelectorAll('selector')

3. Element properties:
   - element.textContent (text only)
   - element.innerHTML (HTML content)
   - element.value (for inputs)
   - element.id (element's ID)
   - element.className (CSS classes)
   - element.style (inline CSS)

4. Tree navigation:
   - element.parentElement (parent)
   - element.children (child elements)
   - element.nextElementSibling (next)
   - element.previousElementSibling (previous)

**Important Concepts:**

1. The DOM is LIVE
   - Changes happen immediately
   - No need to "refresh" or "save"

2. Elements are objects
   - They have properties you can read/write
   - They have methods you can call

3. Everything is a node
   - Elements, text, comments, etc.
   - Forms a tree structure

4. querySelector is modern and flexible
   - Uses CSS selectors
   - More powerful than getElementBy...
   - We'll use this primarily