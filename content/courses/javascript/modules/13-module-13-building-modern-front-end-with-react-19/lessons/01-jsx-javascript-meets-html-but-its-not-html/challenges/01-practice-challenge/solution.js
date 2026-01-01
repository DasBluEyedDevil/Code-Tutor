// Complete JSX simulation

let firstName = 'Alice';
let lastName = 'Johnson';
let age = 25;
let isStudent = true;

function getFullName() {
  return `${firstName} ${lastName}`;
}

function getStatus() {
  if (age < 18) return 'Minor';
  if (isStudent) return 'Adult Student';
  return 'Adult';
}

// Simulated JSX with all features
let jsxOutput = `
<div className="user-card">
  <h1>Welcome, ${getFullName()}!</h1>
  <div className="user-info">
    <p>First Name: ${firstName}</p>
    <p>Last Name: ${lastName}</p>
    <p>Age: ${age}</p>
    <p>Status: ${getStatus()}</p>
    <p>Can Vote: ${age >= 18 ? 'Yes' : 'No'}</p>
  </div>
  ${isStudent ? '<p className="badge">Student Discount Available!</p>' : ''}
</div>
`;

console.log('=== Simulated JSX Output ===');
console.log(jsxOutput);

// Demonstrate JavaScript expressions
console.log('\n=== Expression Examples ===');
console.log('Full name:', getFullName());
console.log('Next year age:', age + 1);
console.log('Name length:', getFullName().length);
console.log('Uppercase:', getFullName().toUpperCase());
console.log('Adult?', age >= 18);

// Array mapping (common in JSX)
let hobbies = ['Reading', 'Coding', 'Gaming'];
let hobbiesList = hobbies.map(hobby => `<li>${hobby}</li>`).join('\n');
console.log('\nHobbies list:');
console.log('<ul>');
console.log(hobbiesList);
console.log('</ul>');