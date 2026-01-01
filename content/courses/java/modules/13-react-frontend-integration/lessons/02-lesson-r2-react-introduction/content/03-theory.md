---
type: "THEORY"
title: "JSX: HTML in JavaScript"
---

JSX lets you write HTML-like syntax in JavaScript:

// This looks like HTML, but it's JSX!
function Greeting() {
    return (
        <div className="greeting">
            <h1>Hello!</h1>
            <p>Welcome to React.</p>
        </div>
    );
}

KEY DIFFERENCES FROM HTML:

1. className instead of class:
<div className="container">  // Not class="container"

2. camelCase for attributes:
<button onClick={handleClick}>  // Not onclick
<input tabIndex={1}>            // Not tabindex

3. JavaScript expressions in curly braces:
const name = "John";
<h1>Hello, {name}!</h1>         // Outputs: Hello, John!
<p>2 + 2 = {2 + 2}</p>          // Outputs: 2 + 2 = 4

4. Self-closing tags required:
<img src="photo.jpg" />          // Not <img src="photo.jpg">
<input type="text" />

5. Must return single parent element:
// WRONG - multiple roots
return (
    <h1>Title</h1>
    <p>Content</p>
);

// RIGHT - single parent
return (
    <div>
        <h1>Title</h1>
        <p>Content</p>
    </div>
);

// Or use Fragment
return (
    <>
        <h1>Title</h1>
        <p>Content</p>
    </>
);