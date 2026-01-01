import 'package:my_app_client/my_app_client.dart';

sealed class ApiResult<T> {}
class ApiSuccess<T> extends ApiResult<T> {
  final T data;
  ApiSuccess(this.data);
}
class ApiFailure<T> extends ApiResult<T> {
  final String message;
  ApiFailure(this.message);
}

class ProductRepository {
  final Client _client;
  final Map<int, Product> _cache = {};
  DateTime? _lastFetch;
  final Duration _cacheDuration;
  
  ProductRepository(
    this._client, {
    Duration cacheDuration = const Duration(minutes: 5),
  }) : _cacheDuration = cacheDuration;
  
  bool get _isCacheValid {
    if (_lastFetch == null || _cache.isEmpty) return false;
    return DateTime.now().difference(_lastFetch!) < _cacheDuration;
  }
  
  Future<ApiResult<List<Product>>> getProducts({bool forceRefresh = false}) async {
    // Return cached data if valid and not forcing refresh
    if (!forceRefresh && _isCacheValid) {
      return ApiSuccess(_cache.values.toList());
    }
    
    try {
      final products = await _client.product.listProducts();
      
      // Update cache
      _cache.clear();
      for (final product in products) {
        if (product.id != null) {
          _cache[product.id!] = product;
        }
      }
      _lastFetch = DateTime.now();
      
      return ApiSuccess(products);
    } on ServerpodClientException catch (e) {
      // Return cached data on error if available
      if (_cache.isNotEmpty) {
        return ApiSuccess(_cache.values.toList());
      }
      return ApiFailure(e.message);
    } catch (e) {
      if (_cache.isNotEmpty) {
        return ApiSuccess(_cache.values.toList());
      }
      return ApiFailure('Failed to fetch products: $e');
    }
  }
  
  Future<ApiResult<Product>> getProduct(int id) async {
    // Check cache first
    if (_cache.containsKey(id)) {
      return ApiSuccess(_cache[id]!);
    }
    
    try {
      final product = await _client.product.getProduct(id);
      if (product == null) {
        return ApiFailure('Product not found');
      }
      
      // Add to cache
      _cache[id] = product;
      
      return ApiSuccess(product);
    } on ServerpodClientException catch (e) {
      return ApiFailure(e.message);
    } catch (e) {
      return ApiFailure('Failed to fetch product: $e');
    }
  }
  
  void clearCache() {
    _cache.clear();
    _lastFetch = null;
  }
}