// Simulated product database
const productsDb = [
  { id: '1', name: 'Laptop', price: 999.99, quantity: 10, category: 'electronics' },
  { id: '2', name: 'Mouse', price: 29.99, quantity: 50, category: 'electronics' },
  { id: '3', name: 'Keyboard', price: 79.99, quantity: 0, category: 'electronics' }
];

// Custom error for validation failures
class ValidationError extends Error {
  constructor(message) {
    super(message);
    this.name = 'ValidationError';
  }
}

// ProductService - All business logic lives here
class ProductService {
  // Get all products
  getAllProducts() {
    return [...productsDb];  // Return copy to prevent mutation
  }
  
  // Get single product by ID
  getProductById(id) {
    const product = productsDb.find(p => p.id === id);
    return product ? { ...product } : null;  // Return copy
  }
  
  // Create new product with validation
  createProduct(data) {
    // Business rule: name is required
    if (!data.name || data.name.trim() === '') {
      throw new ValidationError('Product name is required');
    }
    
    // Business rule: price cannot be negative
    if (typeof data.price !== 'number' || data.price < 0) {
      throw new ValidationError('Price must be a non-negative number');
    }
    
    // Business rule: quantity cannot be negative
    if (typeof data.quantity !== 'number' || data.quantity < 0) {
      throw new ValidationError('Quantity must be a non-negative number');
    }
    
    const newProduct = {
      id: String(productsDb.length + 1),
      name: data.name.trim(),
      price: Math.round(data.price * 100) / 100,  // Round to 2 decimals
      quantity: Math.floor(data.quantity),  // Ensure integer
      category: data.category || 'uncategorized'
    };
    
    productsDb.push(newProduct);
    return { ...newProduct };
  }
  
  // Update existing product
  updateProduct(id, data) {
    const index = productsDb.findIndex(p => p.id === id);
    if (index === -1) {
      return null;
    }
    
    const existing = productsDb[index];
    const updates = {};
    
    // Validate each field if provided
    if (data.name !== undefined) {
      if (data.name.trim() === '') {
        throw new ValidationError('Product name cannot be empty');
      }
      updates.name = data.name.trim();
    }
    
    if (data.price !== undefined) {
      if (typeof data.price !== 'number' || data.price < 0) {
        throw new ValidationError('Price must be a non-negative number');
      }
      updates.price = Math.round(data.price * 100) / 100;
    }
    
    if (data.quantity !== undefined) {
      if (typeof data.quantity !== 'number' || data.quantity < 0) {
        throw new ValidationError('Quantity must be a non-negative number');
      }
      updates.quantity = Math.floor(data.quantity);
    }
    
    if (data.category !== undefined) {
      updates.category = data.category;
    }
    
    // Apply updates
    productsDb[index] = { ...existing, ...updates };
    return { ...productsDb[index] };
  }
  
  // Delete product
  deleteProduct(id) {
    const index = productsDb.findIndex(p => p.id === id);
    if (index === -1) {
      return false;
    }
    
    productsDb.splice(index, 1);
    return true;
  }
  
  // Check if product is in stock
  isInStock(id) {
    const product = productsDb.find(p => p.id === id);
    return product ? product.quantity > 0 : false;
  }
  
  // Apply discount to product
  applyDiscount(id, percentage) {
    if (percentage < 0 || percentage > 100) {
      throw new ValidationError('Discount percentage must be between 0 and 100');
    }
    
    const index = productsDb.findIndex(p => p.id === id);
    if (index === -1) {
      return null;
    }
    
    const product = productsDb[index];
    const discountMultiplier = 1 - (percentage / 100);
    const newPrice = Math.round(product.price * discountMultiplier * 100) / 100;
    
    productsDb[index] = { ...product, price: newPrice };
    return { ...productsDb[index] };
  }
}

// Test the service
const productService = new ProductService();

console.log('=== Testing ProductService ===');

// Test getAllProducts
console.log('\n1. All products:');
console.log(productService.getAllProducts());

// Test getProductById
console.log('\n2. Get product by ID:');
console.log('Product 1:', productService.getProductById('1'));
console.log('Product 999:', productService.getProductById('999'));

// Test isInStock
console.log('\n3. Stock check:');
console.log('Laptop in stock?', productService.isInStock('1'));  // true
console.log('Keyboard in stock?', productService.isInStock('3'));  // false

// Test createProduct
console.log('\n4. Create product:');
try {
  const newProduct = productService.createProduct({
    name: 'Webcam',
    price: 49.99,
    quantity: 25,
    category: 'electronics'
  });
  console.log('Created:', newProduct);
} catch (e) {
  console.log('Error:', e.message);
}

// Test validation
console.log('\n5. Validation test:');
try {
  productService.createProduct({ name: '', price: -10, quantity: 5 });
} catch (e) {
  console.log('Caught validation error:', e.message);
}

// Test applyDiscount
console.log('\n6. Apply 20% discount to Laptop:');
const discounted = productService.applyDiscount('1', 20);
console.log('After discount:', discounted);

// Test updateProduct
console.log('\n7. Update product:');
const updated = productService.updateProduct('2', { quantity: 100 });
console.log('Updated Mouse:', updated);