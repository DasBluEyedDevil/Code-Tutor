// This API data might have missing fields
let userData = {
  name: 'Alice',
  age: 0,  // Just born!
  address: {
    street: '123 Main St'
    // Note: no city!
  },
  // Note: no email!
  hobbies: ['reading', 'coding']
};

// Extract data safely with defaults
let city = // YOUR CODE HERE
let email = // YOUR CODE HERE  
let age = // YOUR CODE HERE
let firstHobby = // YOUR CODE HERE

console.log('City:', city);
console.log('Email:', email);
console.log('Age:', age);
console.log('First hobby:', firstHobby);