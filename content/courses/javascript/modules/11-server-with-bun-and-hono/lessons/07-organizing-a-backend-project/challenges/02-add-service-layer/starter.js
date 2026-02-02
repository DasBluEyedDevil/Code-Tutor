// Simulated product database
const productsDb = [
  { id: '1', name: 'Laptop', price: 999.99, quantity: 10, category: 'electronics' },
  { id: '2', name: 'Mouse', price: 29.99, quantity: 50, category: 'electronics' },
  { id: '3', name: 'Keyboard', price: 79.99, quantity: 0, category: 'electronics' }
];

// TODO: Create ProductService class
class ProductService {
  // Implement methods here
  
  getAllProducts() {
    // Return all products
  }
  
  getProductById(id) {
    // Return product or null
  }
  
  createProduct(data) {
    // Validate and create product
    // Throw error if validation fails
  }
  
  updateProduct(id, data) {
    // Update product if exists
    // Return updated product or null
  }
  
  deleteProduct(id) {
    // Delete product, return true/false
  }
  
  isInStock(id) {
    // Return true if quantity > 0
  }
  
  applyDiscount(id, percentage) {
    // Reduce price by percentage (e.g., 10 = 10% off)
    // Return updated product or null
  }
}

// Test your service
const productService = new ProductService();

console.log('=== Testing ProductService ===');
console.log('All products:', productService.getAllProducts());
console.log('Product 1:', productService.getProductById('1'));
console.log('Product 999:', productService.getProductById('999'));
console.log('Laptop in stock?', productService.isInStock('1'));
console.log('Keyboard in stock?', productService.isInStock('3'));
