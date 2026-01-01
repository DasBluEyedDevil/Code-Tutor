// Step 1: Create User interface
interface User {
  // Add properties
}

// Step 2: Add types to function
function getFullName(user) {
  return user.firstName + ' ' + user.lastName;
}

// Step 3: Create typed user
let user = {
  firstName: 'Jane',
  lastName: 'Doe'
};

console.log(getFullName(user));