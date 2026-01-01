// ========== REFACTOR THIS CODE ==========

// OLD CODE 1: Extract user properties
let user = { name: 'Bob', age: 30, city: 'Seattle', country: 'USA' };
let userName = user.name;
let userAge = user.age;
let userCity = user.city;
console.log(userName, userAge, userCity);

// YOUR REFACTORED VERSION:
// (Use destructuring to extract all three in one line)


// OLD CODE 2: Get first and rest of array
let numbers = [10, 20, 30, 40, 50];
let firstNumber = numbers[0];
let restNumbers = numbers.slice(1);
console.log('First:', firstNumber, 'Rest:', restNumbers);

// YOUR REFACTORED VERSION:
// (Use array destructuring with rest)


// OLD CODE 3: Merge objects
let defaults = { volume: 50, brightness: 80 };
let userSettings = { volume: 75 };
let merged = {};
for (let key in defaults) {
  merged[key] = defaults[key];
}
for (let key in userSettings) {
  merged[key] = userSettings[key];
}
console.log('Merged:', merged);

// YOUR REFACTORED VERSION:
// (Use spread to merge in one line)


// OLD CODE 4: Function with object parameter
function displayProduct(product) {
  let name = product.name;
  let price = product.price;
  let inStock = product.inStock !== undefined ? product.inStock : true;
  console.log(name + ': $' + price + (inStock ? ' (In Stock)' : ' (Out of Stock)'));
}
displayProduct({ name: 'Laptop', price: 999 });

// YOUR REFACTORED VERSION:
// (Use parameter destructuring with default value)


// OLD CODE 5: Swap two variables
let x = 100;
let y = 200;
let temp = x;
x = y;
y = temp;
console.log('After swap: x =', x, ', y =', y);

// YOUR REFACTORED VERSION:
// (Use array destructuring to swap in one line)