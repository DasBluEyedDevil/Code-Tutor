// Simulate JSX with template literals

let firstName = 'Alice';
let lastName = 'Johnson';
let age = 25;

function getFullName() {
  return firstName + ' ' + lastName;
}

// Simulate JSX structure
let jsxOutput = `
<div className="user-card">
  <h1>Name: ${getFullName()}</h1>
  <p>Age: ${age}</p>
  <p>Status: ${age >= 18 ? 'Adult' : 'Minor'}</p>
</div>
`;

console.log('Simulated JSX:');
console.log(jsxOutput);