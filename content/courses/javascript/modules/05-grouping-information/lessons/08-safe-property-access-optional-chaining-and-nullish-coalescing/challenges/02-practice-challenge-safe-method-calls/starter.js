let calculator = {
  add: (a, b) => a + b,
  multiply: (a, b) => a * b
  // Note: no subtract or divide methods!
};

// Safely call methods (return undefined if method doesn't exist)
let sum = // call add(5, 3)
let product = // call multiply(4, 2)
let difference = // call subtract(10, 4) - doesn't exist!
let quotient = // call divide(20, 5) - doesn't exist!

console.log('Sum:', sum);
console.log('Product:', product);
console.log('Difference:', difference ?? 'Method not available');
console.log('Quotient:', quotient ?? 'Method not available');