let products = [
  { name: 'Laptop', price: 1000, category: 'electronics', inStock: true },
  { name: 'Shirt', price: 25, category: 'clothing', inStock: true },
  { name: 'Mouse', price: 25, category: 'electronics', inStock: true },
  { name: 'Pants', price: 50, category: 'clothing', inStock: false },
  { name: 'Keyboard', price: 75, category: 'electronics', inStock: true },
  { name: 'Monitor', price: 300, category: 'electronics', inStock: false },
  { name: 'Headphones', price: 150, category: 'electronics', inStock: false }
];

const TAX_RATE = 0.10;  // 10% tax

// Step 1: Filter to in-stock electronics only
let inStockElectronics = products.filter(/* your code */);
console.log('In-stock electronics:', inStockElectronics.map(p => p.name));
// Expected: ['Laptop', 'Mouse', 'Keyboard']

// Step 2: Map to add tax to prices
let pricesWithTax = inStockElectronics.map(/* your code */);
console.log('Prices with tax:', pricesWithTax);
// Expected: [1100, 27.5, 82.5]

// Step 3: Reduce to get total
let total = pricesWithTax.reduce(/* your code */);
console.log('Total:', total);
// Expected: 1210

// Step 4: Chain all three into one elegant pipeline
let totalChained = products
  // .filter(...)
  // .map(...)
  // .reduce(...);

console.log('Total (chained): $' + totalChained.toFixed(2));
// Expected: $1210.00