// server/lib/src/endpoints/profile_picture_endpoint.dart
import 'dart:typed_data';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ProfilePictureEndpoint extends Endpoint {
  /// Upload and set a new profile picture
  Future<UserProfile> uploadProfilePicture(
    Session session, {
    required Uint8List imageData,
    required String mimeType,
    // Optional crop coordinates
    int? cropX,
    int? cropY,
    int? cropWidth,
    int? cropHeight,
  }) async {
    // TODO: Implement
    // 1. Authenticate user
    // 2. Validate image type and size
    // 3. Get current profile and old avatar
    // 4. Upload new image
    // 5. Process with crop if coordinates provided
    // 6. Generate all profile picture sizes
    // 7. Update user profile with new URLs
    // 8. Delete old profile picture files
    // 9. Return updated profile
    throw UnimplementedError();
  }

  /// Delete current profile picture
  Future<UserProfile> removeProfilePicture(
    Session session,
  ) async {
    // TODO: Implement
    // Remove profile picture and set to null
    throw UnimplementedError();
  }
}