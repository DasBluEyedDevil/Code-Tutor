---
type: "THEORY"
title: "The Web Stack Fundamentals"
---

A modern webpage is a combination of three distinct technologies, often referred to as the "Front-End Stack."

### 1. HTML (HyperText Markup Language)
HTML is a system of **Tags**. These tags tell the browser "This is a heading" (`<h1>`), "This is a link" (`<a>`), or "This is an image" (`<img>`).
*   **Purpose:** Definition, hierarchy, and semantics.

### 2. CSS (Cascading Style Sheets)
CSS is a system of **Rules**. A rule targets a specific HTML tag and applies styles to it.
*   **Cascading:** This means that multiple rules can apply to the same element, and the browser has to "calculate" which one wins based on specific priority rules.
*   **Purpose:** Layout, design, typography, and visual accessibility.

### 3. JavaScript
JavaScript is the **Logic**. It is the only part of the stack that can "think." 
*   It can talk to the HTML to change the text.
*   It can talk to the CSS to change the color.
*   It can talk to a server to get new data without refreshing the page.
*   **Purpose:** Interactivity, data management, and dynamic updates.

### How they connect
The browser reads the HTML file first. Inside that file, it finds "links" to CSS and JavaScript files. The browser then downloads and combines all three to create the experience the user sees.
