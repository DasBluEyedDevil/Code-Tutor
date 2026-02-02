---
type: "EXAMPLE"
title: "Calling Endpoints from Flutter"
---

Now let's see how to call these endpoints from your Flutter app.


```dart
// In your Flutter app
import 'package:my_project_client/my_project_client.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';

// 1. Create the client (usually in main.dart or a service)
late Client client;

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // Initialize the Serverpod client
  client = Client(
    'http://localhost:8080/',  // Your server URL
    authenticationKeyManager: FlutterAuthenticationKeyManager(),
  );

  runApp(MyApp());
}

// 2. Use the client in your widgets/services
class ProductService {

  // Get all products
  Future<List<Product>> fetchProducts() async {
    try {
      // client.product matches ProductEndpoint
      // .getAllProducts matches the method name
      return await client.product.getAllProducts();
    } catch (e) {
      print('Error fetching products: $e');
      rethrow;
    }
  }

  // Get a single product
  Future<Product> fetchProduct(int id) async {
    // Full type safety! The return type is Product
    final product = await client.product.getProduct(id);
    return product;
  }

  // Find product by name (nullable return)
  Future<Product?> findByName(String name) async {
    // Return type is Product? - might be null
    return await client.product.findProductByName(name);
  }

  // Create a new product
  Future<Product> createProduct({
    required String name,
    required double price,
    required int categoryId,
  }) async {
    // Create the Product object
    final product = Product(
      name: name,
      price: price,
      categoryId: categoryId,
      stockQuantity: 0,
      isAvailable: true,
      createdAt: DateTime.now(),
    );

    // Send to server - returns the created product with id
    return await client.product.createProduct(product);
  }

  // Update a product
  Future<Product> updateProduct(Product product) async {
    return await client.product.updateProduct(product);
  }

  // Get stats
  Future<Map<String, dynamic>> getStats() async {
    return await client.product.getProductStats();
  }
}

// 3. Use in a widget
class ProductListScreen extends StatefulWidget {
  @override
  _ProductListScreenState createState() => _ProductListScreenState();
}

class _ProductListScreenState extends State<ProductListScreen> {
  List<Product> _products = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadProducts();
  }

  Future<void> _loadProducts() async {
    setState(() => _isLoading = true);

    try {
      // Call the endpoint - fully typed!
      _products = await client.product.getAllProducts();
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Failed to load: $e')),
      );
    } finally {
      setState(() => _isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return Center(child: CircularProgressIndicator());
    }

    return ListView.builder(
      itemCount: _products.length,
      itemBuilder: (context, index) {
        final product = _products[index];
        return ListTile(
          title: Text(product.name),  // Type-safe access
          subtitle: Text('\$${product.price.toStringAsFixed(2)}'),
        );
      },
    );
  }
}
```
