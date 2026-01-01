let frontendTeam = new Set(['Alice', 'Bob', 'Charlie', 'Diana']);
let backendTeam = new Set(['Charlie', 'Diana', 'Eve', 'Frank']);
let seniorDevs = new Set(['Alice', 'Eve']);

// 1. Find full-stack developers (in BOTH teams)
let fullStack = frontendTeam.intersection(backendTeam);
console.log('Full-stack:', [...fullStack]);  // ['Charlie', 'Diana']

// 2. Find frontend-only developers (in frontend but NOT backend)
let frontendOnly = frontendTeam.difference(backendTeam);
console.log('Frontend only:', [...frontendOnly]);  // ['Alice', 'Bob']

// 3. Combine all developers
let allDevs = frontendTeam.union(backendTeam);
console.log('All developers:', [...allDevs]);
// ['Alice', 'Bob', 'Charlie', 'Diana', 'Eve', 'Frank']

// 4. Check if all seniors are in the combined team
let allSeniorsIncluded = seniorDevs.isSubsetOf(allDevs);
console.log('All seniors included:', allSeniorsIncluded);  // true