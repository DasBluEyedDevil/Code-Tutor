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
  // TODO: Add client field
  
  // TODO: Add cache storage (Map<int, Product>)
  
  // TODO: Add cache timestamp tracking
  
  // TODO: Define cache duration (e.g., 5 minutes)
  
  // TODO: Add constructor
  
  // TODO: Implement getProducts({bool forceRefresh = false})
  // Should check cache first, fetch from server if needed
  
  // TODO: Implement getProduct(int id)
  // Should check cache first for single product
  
  // TODO: Implement clearCache()
}