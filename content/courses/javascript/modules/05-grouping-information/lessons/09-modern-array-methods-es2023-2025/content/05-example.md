---
type: "EXAMPLE"
title: "Object.groupBy() - Grouping Array Items"
---

Object.groupBy() is a powerful new method that groups array items by a key you define. It returns an object where each key is a group name and each value is an array of items belonging to that group.

```javascript
// Object.groupBy() groups array items by a key you define
// This is SUPER useful for organizing data!

let products = [
  { name: 'Apple', category: 'Fruit', price: 1.50 },
  { name: 'Banana', category: 'Fruit', price: 0.75 },
  { name: 'Carrot', category: 'Vegetable', price: 0.50 },
  { name: 'Broccoli', category: 'Vegetable', price: 1.25 },
  { name: 'Milk', category: 'Dairy', price: 3.00 }
];

// Group by category
let byCategory = Object.groupBy(products, product => product.category);

console.log(byCategory);
// {
//   Fruit: [{name: 'Apple', ...}, {name: 'Banana', ...}],
//   Vegetable: [{name: 'Carrot', ...}, {name: 'Broccoli', ...}],
//   Dairy: [{name: 'Milk', ...}]
// }

// Access a specific group
console.log(byCategory.Fruit);  // All fruit products
console.log(byCategory.Vegetable.length);  // 2 vegetables

// Group numbers by even/odd
let numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
let byEvenOdd = Object.groupBy(numbers, n => n % 2 === 0 ? 'even' : 'odd');
console.log(byEvenOdd);
// { odd: [1, 3, 5, 7, 9], even: [2, 4, 6, 8, 10] }

// Group students by grade
let students = [
  { name: 'Alice', score: 95 },
  { name: 'Bob', score: 82 },
  { name: 'Charlie', score: 78 },
  { name: 'Diana', score: 91 },
  { name: 'Eve', score: 65 }
];

let byGrade = Object.groupBy(students, student => {
  if (student.score >= 90) return 'A';
  if (student.score >= 80) return 'B';
  if (student.score >= 70) return 'C';
  return 'D';
});

console.log(byGrade);
// {
//   A: [Alice, Diana],
//   B: [Bob],
//   C: [Charlie],
//   D: [Eve]
// }
console.log('A students:', byGrade.A.map(s => s.name));  // ['Alice', 'Diana']
```
