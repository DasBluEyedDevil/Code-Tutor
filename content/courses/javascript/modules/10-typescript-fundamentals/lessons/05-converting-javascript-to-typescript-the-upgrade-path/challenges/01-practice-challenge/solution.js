// Step 1: Create User interface
interface User {
  firstName: string;
  lastName: string;
}

// Step 2: Add types to function
function getFullName(user: User): string {
  return user.firstName + ' ' + user.lastName;
}

// Step 3: Create typed user
let user: User = {
  firstName: 'Jane',
  lastName: 'Doe'
};

console.log(getFullName(user)); // 'Jane Doe'

// Bonus: Array of users
let users: User[] = [
  { firstName: 'Jane', lastName: 'Doe' },
  { firstName: 'John', lastName: 'Smith' }
];

for (let u of users) {
  console.log(getFullName(u));
}
// Output:
// 'Jane Doe'
// 'John Smith'