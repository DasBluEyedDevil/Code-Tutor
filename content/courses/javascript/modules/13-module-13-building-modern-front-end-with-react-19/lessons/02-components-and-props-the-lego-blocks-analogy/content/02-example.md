---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// React Components and Props

console.log('=== React Components ===\n');

// COMPONENT = Reusable UI function
// Props = Data passed to component (like function parameters)

// 1. FUNCTION COMPONENT (modern React)
function Greeting(props) {
  return `<h1>Hello, ${props.name}!</h1>`;
}

// Use it multiple times with different props
let greeting1 = Greeting({ name: 'Alice' });
let greeting2 = Greeting({ name: 'Bob' });
let greeting3 = Greeting({ name: 'Charlie' });

console.log('Same component, different props:');
console.log(greeting1);  // Hello, Alice!
console.log(greeting2);  // Hello, Bob!
console.log(greeting3);  // Hello, Charlie!

// 2. PROPS ARE READ-ONLY
function UserCard(props) {
  // props.name = 'Different'; // ERROR! Can't modify props!
  
  return `
    <div className="user-card">
      <h2>${props.name}</h2>
      <p>Email: ${props.email}</p>
      <p>Role: ${props.role || 'User'}</p>
    </div>
  `;
}

let user1 = UserCard({
  name: 'Alice Johnson',
  email: 'alice@example.com',
  role: 'Admin'
});

let user2 = UserCard({
  name: 'Bob Smith',
  email: 'bob@example.com'
  // No role = uses default 'User'
});

console.log('\nUser Cards:');
console.log(user1);
console.log(user2);

// 3. DESTRUCTURING PROPS (cleaner syntax)
function Button({ label, color, onClick }) {
  // Instead of props.label, props.color, etc.
  return `<button style="background: ${color}" onClick="${onClick}">${label}</button>`;
}

let submitBtn = Button({
  label: 'Submit',
  color: 'blue',
  onClick: 'handleSubmit()'
});

console.log('\nButton:', submitBtn);

// 4. PROPS WITH CHILDREN
function Card({ title, children }) {
  return `
    <div className="card">
      <h3>${title}</h3>
      <div className="card-body">
        ${children}
      </div>
    </div>
  `;
}

let card = Card({
  title: 'My Card',
  children: '<p>This is the card content</p><p>Multiple children!</p>'
});

console.log('\nCard with children:');
console.log(card);

// 5. COMPONENT COMPOSITION
function Header({ logo, title }) {
  return `<header><img src="${logo}" /><h1>${title}</h1></header>`;
}

function Footer({ year, company }) {
  return `<footer><p>© ${year} ${company}</p></footer>`;
}

function App() {
  return `
    <div className="app">
      ${Header({ logo: 'logo.png', title: 'My App' })}
      <main>
        ${Card({ title: 'Welcome', children: '<p>Welcome to my app!</p>' })}
      </main>
      ${Footer({ year: 2025, company: 'My Company' })}
    </div>
  `;
}

console.log('\nComplete App (composed of smaller components):');
console.log(App());

// 6. PROPS BEST PRACTICES
console.log('\n=== Props Best Practices ===\n');

let practices = [
  '✓ Props are read-only (immutable)',
  '✓ Destructure props for cleaner code',
  '✓ Provide default values: role || "User"',
  '✓ Use clear, descriptive prop names',
  '✓ Pass only what component needs',
  '✓ Children prop for nested content',
  '✓ Keep components focused and reusable'
];

practices.forEach(p => console.log(p));
```
