---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding the three layers:

**1. HTML (HyperText Markup Language)**
- Defines the STRUCTURE and CONTENT
- Uses tags: <tagname>content</tagname>
- Common tags:
  - <h1> to <h6>: Headings
  - <p>: Paragraphs
  - <div>: Generic container
  - <button>: Clickable button
  - <input>: Form input
- Attributes give extra info:
  - id="unique-name" (unique identifier)
  - class="style-class" (styling group)

**2. CSS (Cascading Style Sheets)**
- Defines how HTML elements LOOK
- Syntax: selector { property: value; }
- Can be:
  - Inline: <p style="color: red">Text</p>
  - Internal: <style> tags in <head>
  - External: <link rel="stylesheet" href="style.css">
- Selectors:
  - h1 { } - All <h1> tags
  - #myId { } - Element with id="myId"
  - .myClass { } - Elements with class="myClass"

**3. JavaScript (The Programming Language)**
- Makes pages INTERACTIVE and DYNAMIC
- Can:
  - Find HTML elements
  - Change their content
  - Change their styling
  - Respond to user actions (clicks, typing, etc.)
  - Fetch data from servers
  - Validate forms
  - Animate elements

How they work together:
1. HTML creates structure
2. CSS makes it pretty
3. JavaScript makes it interactive

Example flow:
1. User sees button (HTML)
2. Button is styled blue (CSS)
3. User clicks button (JavaScript detects)
4. Message appears (JavaScript changes HTML)
5. Message is highlighted (JavaScript adds CSS class)