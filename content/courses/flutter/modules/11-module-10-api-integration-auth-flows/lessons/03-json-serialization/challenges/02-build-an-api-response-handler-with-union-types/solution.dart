import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:dio/dio.dart';

part 'api_result.freezed.dart';

enum ApiError {
  network,
  timeout,
  unauthorized,
  notFound,
  serverError,
  unknown,
}

@freezed
class ApiResult<T> with _$ApiResult<T> {
  const factory ApiResult.loading() = ApiLoading<T>;
  const factory ApiResult.success(T data) = ApiSuccess<T>;
  const factory ApiResult.error(ApiError type, String message) = ApiFailure<T>;
}

extension ApiResultExtension<T> on ApiResult<T> {
  bool get isLoading => this is ApiLoading<T>;

  T? get dataOrNull => maybeWhen(
    success: (data) => data,
    orElse: () => null,
  );

  String? get errorMessage => maybeWhen(
    error: (_, message) => message,
    orElse: () => null,
  );
}

ApiResult<T> apiResultFromDioError<T>(DioException e) {
  switch (e.type) {
    case DioExceptionType.connectionTimeout:
    case DioExceptionType.sendTimeout:
    case DioExceptionType.receiveTimeout:
      return ApiResult.error(ApiError.timeout, 'Request timed out');
    case DioExceptionType.connectionError:
      return ApiResult.error(ApiError.network, 'No internet connection');
    case DioExceptionType.badResponse:
      final statusCode = e.response?.statusCode;
      if (statusCode == 401) {
        return ApiResult.error(ApiError.unauthorized, 'Unauthorized');
      } else if (statusCode == 404) {
        return ApiResult.error(ApiError.notFound, 'Resource not found');
      } else if (statusCode != null && statusCode >= 500) {
        return ApiResult.error(ApiError.serverError, 'Server error');
      }
      return ApiResult.error(ApiError.unknown, e.message ?? 'Unknown error');
    default:
      return ApiResult.error(ApiError.unknown, e.message ?? 'Unknown error');
  }
}