let products = [
  { id: 1, name: 'Laptop', price: 999, stock: 15, onSale: false },
  { id: 2, name: 'Mouse', price: 29, stock: 50, onSale: true },
  { id: 3, name: 'Keyboard', price: 79, stock: 0, onSale: false },
  { id: 4, name: 'Monitor', price: 349, stock: 8, onSale: true },
  { id: 5, name: 'Headphones', price: 149, stock: 25, onSale: false }
];

let categories = ['Electronics', 'Clothing', 'Home', 'Books'];

// 1. Find product with id: 3
let product3 = // YOUR CODE
console.log('Product 3:', product3?.name);

// 2. Check if any products are on sale
let hasAnySale = // YOUR CODE
console.log('Has sales:', hasAnySale);

// 3. Verify all products have valid prices (> 0)
let allValidPrices = // YOUR CODE
console.log('All valid prices:', allValidPrices);

// 4. Check if 'Electronics' category exists
let hasElectronics = // YOUR CODE
console.log('Has Electronics:', hasElectronics);

// 5. Get the last product in the array
let lastProduct = // YOUR CODE
console.log('Last product:', lastProduct?.name);

// 6. Find index of first out-of-stock item (stock: 0)
let outOfStockIndex = // YOUR CODE
console.log('Out of stock at index:', outOfStockIndex);