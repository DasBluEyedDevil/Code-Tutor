// server/lib/src/endpoints/profile_picture_endpoint.dart
import 'dart:typed_data';
import 'package:image/image.dart' as img;
import 'package:serverpod/serverpod.dart';
import 'package:uuid/uuid.dart';
import '../generated/protocol.dart';
import '../util/storage_config.dart';

class ProfilePictureEndpoint extends Endpoint {
  static const _uuid = Uuid();
  
  // Profile picture size configurations
  static const Map<String, int> profileSizes = {
    'small': 64,
    'medium': 128,
    'large': 256,
    'xlarge': 512,
  };

  /// Upload and set a new profile picture
  Future<ProfilePictureResult> uploadProfilePicture(
    Session session, {
    required Uint8List imageData,
    required String mimeType,
    int? cropX,
    int? cropY,
    int? cropWidth,
    int? cropHeight,
  }) async {
    // 1. Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ProfilePictureException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw ProfilePictureException(
        code: 'user_not_found',
        message: 'User profile not found',
      );
    }
    
    // 2. Validate image type
    if (!StorageConfig.allowedMimeTypes['image']!.contains(mimeType)) {
      throw ProfilePictureException(
        code: 'invalid_type',
        message: 'Image type not supported',
      );
    }
    
    // Validate size (max 5MB for profile pictures)
    if (imageData.length > 5 * 1024 * 1024) {
      throw ProfilePictureException(
        code: 'file_too_large',
        message: 'Profile picture must be smaller than 5MB',
      );
    }
    
    // 3. Store old avatar URL for later deletion
    final oldAvatarUrl = user.avatarUrl;
    final oldMediaFileId = user.avatarMediaFileId;
    
    // 4. Decode and validate image
    var image = img.decodeImage(imageData);
    if (image == null) {
      throw ProfilePictureException(
        code: 'decode_failed',
        message: 'Failed to decode image',
      );
    }
    
    // Minimum dimensions
    if (image.width < 100 || image.height < 100) {
      throw ProfilePictureException(
        code: 'too_small',
        message: 'Image must be at least 100x100 pixels',
      );
    }
    
    // 5. Apply crop if coordinates provided
    if (cropX != null && cropY != null && 
        cropWidth != null && cropHeight != null) {
      // Validate crop bounds
      if (cropX < 0 || cropY < 0 ||
          cropX + cropWidth > image.width ||
          cropY + cropHeight > image.height) {
        throw ProfilePictureException(
          code: 'invalid_crop',
          message: 'Crop coordinates are out of bounds',
        );
      }
      
      image = img.copyCrop(
        image,
        x: cropX,
        y: cropY,
        width: cropWidth,
        height: cropHeight,
      );
    }
    
    // Strip EXIF data for privacy
    image.exif.clear();
    
    // 6. Generate unique file ID and base path
    final fileId = _uuid.v4();
    final basePath = 'profiles/user_${user.id}/$fileId';
    
    // Generate all sizes
    final avatarUrls = <String, String>{};
    
    for (final entry in profileSizes.entries) {
      final sizeName = entry.key;
      final size = entry.value;
      
      // Resize to square
      final resized = img.copyResizeCropSquare(image, size: size);
      
      // Encode as JPEG
      final bytes = img.encodeJpg(resized, quality: 90);
      
      // Upload
      final path = '${basePath}_$sizeName.jpg';
      await session.storage.storeFile(
        storageId: StorageConfig.publicStorage,
        path: path,
        byteData: ByteData.view(Uint8List.fromList(bytes).buffer),
      );
      
      // Get public URL
      final url = await session.storage.getPublicUrl(
        storageId: StorageConfig.publicStorage,
        path: path,
      );
      
      avatarUrls[sizeName] = url?.toString() ?? '';
    }
    
    // Upload original (cropped) as well
    final originalBytes = img.encodeJpg(image, quality: 95);
    final originalPath = '${basePath}_original.jpg';
    await session.storage.storeFile(
      storageId: StorageConfig.publicStorage,
      path: originalPath,
      byteData: ByteData.view(Uint8List.fromList(originalBytes).buffer),
    );
    
    final originalUrl = await session.storage.getPublicUrl(
      storageId: StorageConfig.publicStorage,
      path: originalPath,
    );
    avatarUrls['original'] = originalUrl?.toString() ?? '';
    
    // Create media file record
    final mediaFile = await MediaFile.db.insertRow(
      session,
      MediaFile(
        userId: user.id!,
        fileName: fileId,
        originalFileName: 'profile_picture.jpg',
        mimeType: 'image/jpeg',
        fileSize: originalBytes.length,
        storageKey: originalPath,
        storageBucket: StorageConfig.publicStorage,
        publicUrl: avatarUrls['original'],
        thumbnailUrl: avatarUrls['small'],
        mediumUrl: avatarUrls['medium'],
        mediaType: 'image',
        width: image.width,
        height: image.height,
        status: 'ready',
        metadata: jsonEncode({'profileSizes': avatarUrls}),
        createdAt: DateTime.now(),
        processedAt: DateTime.now(),
      ),
    );
    
    // 7. Update user profile
    final updatedUser = await UserProfile.db.updateRow(
      session,
      user.copyWith(
        avatarUrl: avatarUrls['medium'], // Default display size
        avatarMediaFileId: mediaFile.id,
        updatedAt: DateTime.now(),
      ),
    );
    
    // 8. Delete old profile picture files
    if (oldMediaFileId != null) {
      await _deleteOldProfilePicture(session, oldMediaFileId);
    }
    
    return ProfilePictureResult(
      userProfile: updatedUser,
      avatarUrls: avatarUrls,
      mediaFileId: mediaFile.id!,
    );
  }

  /// Delete current profile picture
  Future<UserProfile> removeProfilePicture(
    Session session,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ProfilePictureException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw ProfilePictureException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    final oldMediaFileId = user.avatarMediaFileId;
    
    // Update profile to remove avatar
    final updatedUser = await UserProfile.db.updateRow(
      session,
      user.copyWith(
        avatarUrl: null,
        avatarMediaFileId: null,
        updatedAt: DateTime.now(),
      ),
    );
    
    // Delete old files
    if (oldMediaFileId != null) {
      await _deleteOldProfilePicture(session, oldMediaFileId);
    }
    
    return updatedUser;
  }
  
  /// Delete old profile picture and all its sizes
  Future<void> _deleteOldProfilePicture(
    Session session,
    int mediaFileId,
  ) async {
    final mediaFile = await MediaFile.db.findById(session, mediaFileId);
    if (mediaFile == null) return;
    
    // Parse metadata to get all paths
    if (mediaFile.metadata != null) {
      try {
        final metadata = jsonDecode(mediaFile.metadata!);
        final profileSizes = metadata['profileSizes'] as Map<String, dynamic>?;
        
        if (profileSizes != null) {
          // Extract paths from URLs and delete
          // This is simplified; in production, store paths separately
        }
      } catch (e) {
        // Ignore metadata parse errors
      }
    }
    
    // Delete main file
    try {
      await session.storage.deleteFile(
        storageId: StorageConfig.publicStorage,
        path: mediaFile.storageKey,
      );
    } catch (e) {
      // Ignore deletion errors
    }
    
    // Delete all size variants
    for (final sizeName in profileSizes.keys) {
      final sizePath = mediaFile.storageKey
          .replaceAll('_original.jpg', '_$sizeName.jpg');
      try {
        await session.storage.deleteFile(
          storageId: StorageConfig.publicStorage,
          path: sizePath,
        );
      } catch (e) {
        // Ignore
      }
    }
    
    // Soft delete media record
    await MediaFile.db.updateRow(
      session,
      mediaFile.copyWith(deletedAt: DateTime.now()),
    );
  }
}

class ProfilePictureResult {
  final UserProfile userProfile;
  final Map<String, String> avatarUrls;
  final int mediaFileId;
  
  ProfilePictureResult({
    required this.userProfile,
    required this.avatarUrls,
    required this.mediaFileId,
  });
}

class ProfilePictureException implements Exception {
  final String code;
  final String message;
  
  ProfilePictureException({required this.code, required this.message});
  
  @override
  String toString() => 'ProfilePictureException: [$code] $message';
}