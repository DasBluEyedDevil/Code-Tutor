---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// JSX - The Syntax That Powers React

// IMPORTANT: JSX compiles to JavaScript!
// This JSX:
// <h1>Hello World</h1>
//
// Becomes this JavaScript:
// React.createElement('h1', null, 'Hello World')

console.log('=== JSX Fundamentals ===\n');

// 1. JSX LOOKS LIKE HTML (but isn't!)
let jsxElement = '<h1>Hello, React!</h1>'; // This is just a string
console.log('String:', jsxElement);

// Real JSX (conceptual - won't run in plain JavaScript):
// let jsxElement = <h1>Hello, React!</h1>;
// This compiles to: React.createElement('h1', null, 'Hello, React!');

// 2. EMBEDDING JAVASCRIPT WITH {}
let name = 'Alice';
let age = 25;

// JSX allows JavaScript expressions inside curly braces
let greeting = `<h1>Hello, ${name}!</h1>`;  // Template literal (similar concept)
console.log('\nGreeting:', greeting);

// In real JSX:
// <h1>Hello, {name}!</h1>
// <p>You are {age} years old</p>
// <p>Next year: {age + 1}</p>

// 3. JSX VS HTML - KEY DIFFERENCES
console.log('\n=== JSX vs HTML Differences ===\n');

let differences = {
  'class': {
    html: '<div class="card">',
    jsx: '<div className="card">',
    reason: 'class is a JavaScript keyword'
  },
  'for': {
    html: '<label for="name">',
    jsx: '<label htmlFor="name">',
    reason: 'for is a JavaScript keyword'
  },
  'style': {
    html: '<div style="color: red; font-size: 16px">',
    jsx: '<div style={{ color: "red", fontSize: "16px" }}>',
    reason: 'JSX style is a JavaScript object'
  },
  'onclick': {
    html: '<button onclick="handleClick()">',
    jsx: '<button onClick={handleClick}>',
    reason: 'camelCase in JSX, function reference not string'
  },
  'self-closing': {
    html: '<img src="pic.jpg">',
    jsx: '<img src="pic.jpg" />',
    reason: 'JSX requires closing slash for void elements'
  }
};

for (let [feature, diff] of Object.entries(differences)) {
  console.log(`${feature.toUpperCase()}:`);
  console.log(`  HTML: ${diff.html}`);
  console.log(`  JSX:  ${diff.jsx}`);
  console.log(`  Why:  ${diff.reason}\n`);
}

// 4. JAVASCRIPT EXPRESSIONS IN JSX
console.log('=== JavaScript in JSX ===\n');

let user = {
  firstName: 'Bob',
  lastName: 'Smith',
  age: 30
};

// You can use ANY JavaScript expression inside {}
let examples = [
  `{user.firstName}           → ${user.firstName}`,
  `{user.firstName + ' ' + user.lastName} → ${user.firstName + ' ' + user.lastName}`,
  `{age > 18 ? 'Adult' : 'Minor'} → ${user.age > 18 ? 'Adult' : 'Minor'}`,
  `{[1,2,3].map(n => n * 2)}  → ${[1,2,3].map(n => n * 2).join(', ')}`
];

examples.forEach(ex => console.log(ex));

// 5. MUST RETURN SINGLE ROOT ELEMENT
console.log('\n=== JSX Rules ===\n');

// WRONG (in JSX - multiple roots):
// return (
//   <h1>Title</h1>
//   <p>Text</p>
// );

// CORRECT - Wrapped in div:
// return (
//   <div>
//     <h1>Title</h1>
//     <p>Text</p>
//   </div>
// );

// BETTER - React Fragment (no extra DOM node):
// return (
//   <>
//     <h1>Title</h1>
//     <p>Text</p>
//   </>
// );

console.log('✓ JSX must have ONE root element');
console.log('✓ Use <> </> (Fragment) to avoid extra divs');
console.log('✓ All tags must be closed (including <img />, <br />)');
console.log('✓ Use className not class');
console.log('✓ Use camelCase for attributes (onClick, onChange)');

// 6. WHY JSX?
let benefits = [
  'Familiar HTML-like syntax',
  'JavaScript power with {} expressions',
  'Type checking and autocomplete',
  'Prevents injection attacks (auto-escapes)',
  'Easier to visualize component structure',
  'Not required but highly recommended'
];

console.log('\nWhy use JSX:');
benefits.forEach(b => console.log(`  ✓ ${b}`));
```
