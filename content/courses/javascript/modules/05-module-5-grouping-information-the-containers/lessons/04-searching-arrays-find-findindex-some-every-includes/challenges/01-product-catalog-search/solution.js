let products = [
  { id: 1, name: 'Laptop', price: 999, stock: 15, onSale: false },
  { id: 2, name: 'Mouse', price: 29, stock: 50, onSale: true },
  { id: 3, name: 'Keyboard', price: 79, stock: 0, onSale: false },
  { id: 4, name: 'Monitor', price: 349, stock: 8, onSale: true },
  { id: 5, name: 'Headphones', price: 149, stock: 25, onSale: false }
];

let categories = ['Electronics', 'Clothing', 'Home', 'Books'];

// 1. Find product with id: 3
let product3 = products.find(p => p.id === 3);
console.log('Product 3:', product3?.name);  // 'Keyboard'

// 2. Check if any products are on sale
let hasAnySale = products.some(p => p.onSale);
console.log('Has sales:', hasAnySale);  // true

// 3. Verify all products have valid prices (> 0)
let allValidPrices = products.every(p => p.price > 0);
console.log('All valid prices:', allValidPrices);  // true

// 4. Check if 'Electronics' category exists
let hasElectronics = categories.includes('Electronics');
console.log('Has Electronics:', hasElectronics);  // true

// 5. Get the last product in the array
let lastProduct = products.at(-1);
console.log('Last product:', lastProduct?.name);  // 'Headphones'

// 6. Find index of first out-of-stock item (stock: 0)
let outOfStockIndex = products.findIndex(p => p.stock === 0);
console.log('Out of stock at index:', outOfStockIndex);  // 2