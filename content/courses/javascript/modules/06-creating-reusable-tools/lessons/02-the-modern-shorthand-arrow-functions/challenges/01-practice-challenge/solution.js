const square = x => x * x;

const isEven = num => num % 2 === 0;

const getFullName = (first, last) => first + ' ' + last;

// Test them
console.log(square(5));  // 25
console.log(isEven(4));  // true
console.log(getFullName('John', 'Doe'));  // John Doe