import 'dart:async';
import 'package:dio/dio.dart';

/// Interceptor that handles JWT token refresh automatically.
class TokenRefreshInterceptor extends Interceptor {
  final Dio dio;
  final Future<String?> Function() refreshToken;
  final void Function() onRefreshFailed;
  
  // TODO: Add a flag to prevent multiple refresh attempts
  
  TokenRefreshInterceptor({
    required this.dio,
    required this.refreshToken,
    required this.onRefreshFailed,
  });

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) async {
    // TODO: Implement token refresh logic
    // 1. Check if error is 401
    // 2. If already refreshing, queue request or reject
    // 3. Call refreshToken()
    // 4. If new token received, update request and retry
    // 5. If refresh fails, call onRefreshFailed and reject
    
    handler.next(err);
  }
}