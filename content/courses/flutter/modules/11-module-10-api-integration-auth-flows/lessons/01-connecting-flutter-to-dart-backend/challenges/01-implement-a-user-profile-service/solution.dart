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

class UserProfileService {
  final Client _client;
  
  UserProfileService(this._client);
  
  Future<ApiResult<UserProfile>> getProfile() async {
    try {
      final profile = await _client.userProfile.getCurrentProfile();
      if (profile == null) {
        return ApiFailure('Profile not found');
      }
      return ApiSuccess(profile);
    } on ServerpodClientException catch (e) {
      return ApiFailure(e.message);
    } catch (e) {
      return ApiFailure('An unexpected error occurred: $e');
    }
  }
  
  Future<ApiResult<UserProfile>> updateProfile(UserProfile profile) async {
    try {
      final updated = await _client.userProfile.updateProfile(profile);
      return ApiSuccess(updated);
    } on ServerpodClientException catch (e) {
      return ApiFailure(e.message);
    } catch (e) {
      return ApiFailure('Failed to update profile: $e');
    }
  }
  
  Future<ApiResult<bool>> isUsernameAvailable(String username) async {
    try {
      final isAvailable = await _client.userProfile.checkUsernameAvailable(username);
      return ApiSuccess(isAvailable);
    } on ServerpodClientException catch (e) {
      return ApiFailure(e.message);
    } catch (e) {
      return ApiFailure('Failed to check username: $e');
    }
  }
}