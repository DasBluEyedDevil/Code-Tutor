import 'package:my_project_client/my_project_client.dart';

class ProductService {
  final Client client;

  ProductService(this.client);

  /// Fetch all products from the server.
  Future<List<Product>> fetchAllProducts() async {
    try {
      return await client.product.getAllProducts();
    } catch (e) {
      print('Error fetching products: $e');
      rethrow;
    }
  }

  /// Fetch a single product by ID.
  /// Returns null if not found.
  Future<Product?> fetchProduct(int id) async {
    try {
      return await client.product.getProduct(id);
    } on Exception catch (e) {
      if (e.toString().contains('not found')) {
        return null;
      }
      rethrow;
    }
  }

  /// Create a new product.
  Future<Product> createProduct(String name, double price) async {
    if (name.trim().isEmpty) {
      throw ArgumentError('Product name cannot be empty');
    }
    if (price <= 0) {
      throw ArgumentError('Price must be positive');
    }

    final product = Product(
      name: name,
      price: price,
      stockQuantity: 0,
      isAvailable: true,
      createdAt: DateTime.now(),
      categoryId: 1, // Default category
    );

    try {
      return await client.product.createProduct(product);
    } catch (e) {
      print('Error creating product: $e');
      rethrow;
    }
  }

  /// Delete a product.
  Future<bool> deleteProduct(int id) async {
    try {
      return await client.product.deleteProduct(id);
    } catch (e) {
      print('Error deleting product: $e');
      return false;
    }
  }
}