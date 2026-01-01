---
type: "EXAMPLE"
title: "reduce() - The Swiss Army Knife"
---

The reduce() method processes an array and 'reduces' it to a SINGLE value. That value can be anything: a number (sum, product), a string, an object, or even another array. Think of it as a snowball rolling downhill, accumulating more data at each step. The 'accumulator' holds the running result, and each element adds to or modifies it.

```javascript
// reduce() accumulates array elements into a SINGLE result
// Think of it like a snowball rolling downhill, getting bigger

// Syntax: array.reduce((accumulator, currentItem) => newAccumulator, initialValue)

// Example 1: Sum of numbers (step by step)
let numbers = [10, 20, 30, 40];

let sum = numbers.reduce(function(accumulator, currentNumber) {
  console.log(`acc: ${accumulator}, current: ${currentNumber}, new acc: ${accumulator + currentNumber}`);
  return accumulator + currentNumber;
}, 0);  // 0 is the initial value

// Console output:
// acc: 0, current: 10, new acc: 10
// acc: 10, current: 20, new acc: 30
// acc: 30, current: 30, new acc: 60
// acc: 60, current: 40, new acc: 100
console.log('Sum:', sum);  // Sum: 100

// Example 2: Product of numbers
let factors = [2, 3, 4, 5];
let product = factors.reduce((acc, num) => acc * num, 1);  // Start with 1 for multiplication
console.log('Product:', product);  // Product: 120 (2*3*4*5)

// Example 3: Find maximum value
let scores = [72, 95, 88, 64, 91];
let maxScore = scores.reduce((max, score) => score > max ? score : max, scores[0]);
console.log('Max:', maxScore);  // Max: 95

// Example 4: Count occurrences (building an object)
let fruits = ['apple', 'banana', 'apple', 'cherry', 'banana', 'apple'];

let fruitCounts = fruits.reduce((counts, fruit) => {
  // If fruit exists in counts, increment it; otherwise start at 1
  counts[fruit] = (counts[fruit] || 0) + 1;
  return counts;
}, {});  // Start with empty object

console.log(fruitCounts);  // { apple: 3, banana: 2, cherry: 1 }

// Example 5: Group objects by property
let people = [
  { name: 'Alice', department: 'Engineering' },
  { name: 'Bob', department: 'Marketing' },
  { name: 'Charlie', department: 'Engineering' },
  { name: 'Diana', department: 'Marketing' },
  { name: 'Eve', department: 'Engineering' }
];

let byDepartment = people.reduce((groups, person) => {
  let dept = person.department;
  // If department doesn't exist yet, create empty array
  if (!groups[dept]) {
    groups[dept] = [];
  }
  // Add person to their department
  groups[dept].push(person);
  return groups;
}, {});

console.log(byDepartment);
// {
//   Engineering: [{name: 'Alice'...}, {name: 'Charlie'...}, {name: 'Eve'...}],
//   Marketing: [{name: 'Bob'...}, {name: 'Diana'...}]
// }

// Example 6: Flatten nested arrays
let nested = [[1, 2], [3, 4], [5, 6]];
let flat = nested.reduce((acc, innerArray) => {
  return acc.concat(innerArray);  // or [...acc, ...innerArray]
}, []);
console.log(flat);  // [1, 2, 3, 4, 5, 6]

// Example 7: Build an object from array of pairs
let pairs = [['name', 'Alice'], ['age', 25], ['city', 'NYC']];
let obj = pairs.reduce((result, [key, value]) => {
  result[key] = value;
  return result;
}, {});
console.log(obj);  // { name: 'Alice', age: 25, city: 'NYC' }

// Example 8: Calculate cart total with quantities
let cart = [
  { name: 'Laptop', price: 1000, quantity: 1 },
  { name: 'Mouse', price: 25, quantity: 2 },
  { name: 'USB Cable', price: 10, quantity: 3 }
];

let cartTotal = cart.reduce((total, item) => {
  return total + (item.price * item.quantity);
}, 0);
console.log('Cart total: $' + cartTotal);  // Cart total: $1080
```
