let frontendTeam = new Set(['Alice', 'Bob', 'Charlie', 'Diana']);
let backendTeam = new Set(['Charlie', 'Diana', 'Eve', 'Frank']);
let seniorDevs = new Set(['Alice', 'Eve']);

// 1. Find full-stack developers (in BOTH teams)
let fullStack = // YOUR CODE HERE
console.log('Full-stack:', [...fullStack]);

// 2. Find frontend-only developers (in frontend but NOT backend)
let frontendOnly = // YOUR CODE HERE
console.log('Frontend only:', [...frontendOnly]);

// 3. Combine all developers
let allDevs = // YOUR CODE HERE
console.log('All developers:', [...allDevs]);

// 4. Check if all seniors are in the combined team
let allSeniorsIncluded = // YOUR CODE HERE
console.log('All seniors included:', allSeniorsIncluded);