import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ProductEndpoint extends Endpoint {
  
  /// Get a single product by ID
  Future<Product?> getProduct(Session session, int productId) async {
    return await Product.db.findById(session, productId);
  }
  
  /// List products with optional filtering
  Future<List<Product>> listProducts(
    Session session, {
    int limit = 50,
    int offset = 0,
    String? category,
    double? minPrice,
    double? maxPrice,
  }) async {
    return await Product.db.find(
      session,
      where: (t) {
        // Start with available products
        var condition = t.isAvailable.equals(true);
        
        // Add category filter if provided
        if (category != null) {
          condition = condition & t.category.equals(category);
        }
        
        // Add price range filters if provided
        if (minPrice != null) {
          condition = condition & t.price.greaterOrEqual(minPrice);
        }
        if (maxPrice != null) {
          condition = condition & t.price.lessOrEqual(maxPrice);
        }
        
        return condition;
      },
      limit: limit,
      offset: offset,
      orderBy: (t) => t.name,
    );
  }
  
  /// Create a new product
  Future<Product> createProduct(Session session, Product product) async {
    // Validate required fields
    if (product.name.trim().isEmpty) {
      throw ArgumentError('Product name cannot be empty');
    }
    if (product.price < 0) {
      throw ArgumentError('Price cannot be negative');
    }
    
    // Set server-controlled fields
    final productToInsert = product.copyWith(
      createdAt: DateTime.now(),
      isAvailable: true,
    );
    
    final savedProduct = await Product.db.insertRow(session, productToInsert);
    session.log('Created product: ${savedProduct.id} - ${savedProduct.name}');
    
    return savedProduct;
  }
  
  /// Update an existing product
  Future<Product> updateProduct(Session session, Product product) async {
    if (product.id == null) {
      throw ArgumentError('Product ID is required for update');
    }
    
    // Verify product exists
    final existing = await Product.db.findById(session, product.id!);
    if (existing == null) {
      throw Exception('Product not found');
    }
    
    // Validate
    if (product.name.trim().isEmpty) {
      throw ArgumentError('Product name cannot be empty');
    }
    if (product.price < 0) {
      throw ArgumentError('Price cannot be negative');
    }
    
    return await Product.db.updateRow(session, product);
  }
  
  /// Delete a product by ID
  Future<bool> deleteProduct(Session session, int productId) async {
    final product = await Product.db.findById(session, productId);
    if (product == null) {
      return false;
    }
    
    await Product.db.deleteRow(session, product);
    session.log('Deleted product: $productId');
    
    return true;
  }
  
  /// Search products by name (case-insensitive)
  Future<List<Product>> searchProducts(
    Session session,
    String query,
  ) async {
    if (query.length < 2) {
      throw ArgumentError('Search query must be at least 2 characters');
    }
    
    return await Product.db.find(
      session,
      where: (t) => 
        t.isAvailable.equals(true) &
        t.name.ilike('%$query%'),
      limit: 100,
      orderBy: (t) => t.name,
    );
  }
}