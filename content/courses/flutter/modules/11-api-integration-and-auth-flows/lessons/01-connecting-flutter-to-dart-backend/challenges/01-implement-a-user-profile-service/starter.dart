import 'package:my_app_client/my_app_client.dart';

// Implement the ApiResult classes from the lesson
sealed class ApiResult<T> {}
class ApiSuccess<T> extends ApiResult<T> {
  final T data;
  ApiSuccess(this.data);
}
class ApiFailure<T> extends ApiResult<T> {
  final String message;
  ApiFailure(this.message);
}

class UserProfileService {
  // TODO: Add client field
  
  // TODO: Add constructor
  
  // TODO: Implement getProfile()
  // Should return ApiResult<UserProfile>
  
  // TODO: Implement updateProfile(UserProfile profile)
  // Should return ApiResult<UserProfile>
  
  // TODO: Implement isUsernameAvailable(String username)
  // Should return ApiResult<bool>
}