---
type: "WARNING"
title: "The Boundaries"
---

### 1. Don't use JS for everything
A common beginner mistake is using JavaScript to do things that CSS can do better. For example, changing a button color when the mouse hovers over it should be done with CSS (`:hover`) instead of a JavaScript event listener. 
*   **Reason:** CSS is faster and smoother for basic animations and styles.

### 2. Semantic HTML
Don't use JavaScript to make a `<div>` act like a button. Use a `<button>` tag! 
*   **Reason:** Screen readers (for blind users) and search engines (like Google) depend on you using the correct HTML tags to understand what your page is about.

### 3. Order Matters
Browsers usually read files from top to bottom. If your JavaScript tries to find a button on the page, but the button HTML hasn't been "read" by the browser yet, your script will fail.
*   **Fix:** Most developers place their `<script>` tags at the very bottom of the HTML file, or use the `defer` keyword.

### 4. JavaScript is Optional (for the user)
Users can disable JavaScript in their browsers. If your website's entire layout depends on JavaScript, those users will see a blank page. 
*   **Rule:** Use HTML/CSS for the "skeleton" and "skin" and JavaScript for the "muscles."
