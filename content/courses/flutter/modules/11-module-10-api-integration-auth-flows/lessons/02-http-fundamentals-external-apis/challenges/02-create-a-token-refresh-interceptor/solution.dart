import 'dart:async';
import 'package:dio/dio.dart';

class TokenRefreshInterceptor extends Interceptor {
  final Dio dio;
  final Future<String?> Function() refreshToken;
  final void Function() onRefreshFailed;
  
  bool _isRefreshing = false;
  final List<Completer<void>> _refreshQueue = [];
  String? _newToken;

  TokenRefreshInterceptor({
    required this.dio,
    required this.refreshToken,
    required this.onRefreshFailed,
  });

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) async {
    // Only handle 401 errors
    if (err.response?.statusCode != 401) {
      return handler.next(err);
    }
    
    // Skip if this is already a refresh token request
    if (err.requestOptions.extra['isRefreshRequest'] == true) {
      return handler.next(err);
    }

    try {
      // If already refreshing, wait for it to complete
      if (_isRefreshing) {
        final completer = Completer<void>();
        _refreshQueue.add(completer);
        await completer.future;
        
        // Use the token from the successful refresh
        if (_newToken != null) {
          final response = await _retryRequest(err.requestOptions, _newToken!);
          return handler.resolve(response);
        } else {
          return handler.next(err);
        }
      }

      // Start refreshing
      _isRefreshing = true;
      _newToken = null;

      // Attempt to refresh the token
      final newToken = await refreshToken();

      if (newToken != null && newToken.isNotEmpty) {
        _newToken = newToken;
        
        // Complete all queued requests
        for (final completer in _refreshQueue) {
          completer.complete();
        }
        _refreshQueue.clear();
        
        // Retry the original request with new token
        final response = await _retryRequest(err.requestOptions, newToken);
        _isRefreshing = false;
        return handler.resolve(response);
      } else {
        // Refresh failed
        _handleRefreshFailure();
        return handler.next(err);
      }
    } catch (e) {
      _handleRefreshFailure();
      return handler.next(err);
    }
  }

  Future<Response> _retryRequest(
    RequestOptions requestOptions,
    String token,
  ) async {
    // Update the authorization header
    requestOptions.headers['Authorization'] = 'Bearer $token';
    
    // Mark as retry to avoid infinite loops
    requestOptions.extra['isRetry'] = true;
    
    // Create a new request with updated options
    return await dio.fetch(requestOptions);
  }

  void _handleRefreshFailure() {
    _isRefreshing = false;
    _newToken = null;
    
    // Reject all queued requests
    for (final completer in _refreshQueue) {
      completer.completeError('Token refresh failed');
    }
    _refreshQueue.clear();
    
    // Notify the app (usually triggers logout)
    onRefreshFailed();
  }
}