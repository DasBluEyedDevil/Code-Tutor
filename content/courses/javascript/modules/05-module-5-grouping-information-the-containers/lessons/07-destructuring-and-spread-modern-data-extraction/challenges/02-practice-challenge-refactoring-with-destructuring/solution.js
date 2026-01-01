// ========== REFACTORED CODE ==========

// REFACTORED 1: Extract user properties
let user = { name: 'Bob', age: 30, city: 'Seattle', country: 'USA' };
let { name: userName, age: userAge, city: userCity } = user;
console.log(userName, userAge, userCity);
// Bob 30 Seattle

// REFACTORED 2: Get first and rest of array
let numbers = [10, 20, 30, 40, 50];
let [firstNumber, ...restNumbers] = numbers;
console.log('First:', firstNumber, 'Rest:', restNumbers);
// First: 10 Rest: [20, 30, 40, 50]

// REFACTORED 3: Merge objects
let defaults = { volume: 50, brightness: 80 };
let userSettings = { volume: 75 };
let merged = { ...defaults, ...userSettings };
console.log('Merged:', merged);
// Merged: { volume: 75, brightness: 80 }

// REFACTORED 4: Function with object parameter
function displayProduct({ name, price, inStock = true }) {
  console.log(`${name}: $${price}${inStock ? ' (In Stock)' : ' (Out of Stock)'}`);
}
displayProduct({ name: 'Laptop', price: 999 });
// Laptop: $999 (In Stock)

// REFACTORED 5: Swap two variables
let x = 100;
let y = 200;
[x, y] = [y, x];
console.log('After swap: x =', x, ', y =', y);
// After swap: x = 200, y = 100