---
type: "EXAMPLE"
title: "Image Upload Endpoint"
---


**Implementing Secure Image Uploads**

Our upload endpoint validates files, generates unique names, stores metadata, and returns URLs for the uploaded media.



```dart
// server/lib/src/protocol/media_file.yaml
class: MediaFile
table: media_files
fields:
  # Owner
  userId: int, relation(parent=user_profiles)
  
  # File info
  fileName: String
  originalFileName: String
  mimeType: String
  fileSize: int
  
  # Storage location
  storageKey: String
  storageBucket: String
  
  # URLs (cached for performance)
  publicUrl: String?
  thumbnailUrl: String?
  mediumUrl: String?
  
  # Media type
  mediaType: String  # 'image', 'video', 'audio'
  
  # Dimensions (for images/videos)
  width: int?
  height: int?
  duration: int?  # For video/audio in seconds
  
  # Processing status
  status: String  # 'pending', 'processing', 'ready', 'failed'
  processingError: String?
  
  # Content hash for deduplication
  contentHash: String?
  
  # Metadata
  metadata: String?  # JSON for additional info
  
  # Timestamps
  createdAt: DateTime
  processedAt: DateTime?
  deletedAt: DateTime?

indexes:
  media_user_idx:
    fields: userId, createdAt
  media_hash_idx:
    fields: contentHash
  media_status_idx:
    fields: status, createdAt
  media_type_idx:
    fields: userId, mediaType, createdAt

---

// server/lib/src/endpoints/media_upload_endpoint.dart
import 'dart:convert';
import 'dart:typed_data';
import 'package:crypto/crypto.dart';
import 'package:serverpod/serverpod.dart';
import 'package:uuid/uuid.dart';
import '../generated/protocol.dart';
import '../util/storage_config.dart';

class MediaUploadEndpoint extends Endpoint {
  static const _uuid = Uuid();

  /// Request a signed URL for uploading an image
  Future<UploadUrlResponse> requestImageUploadUrl(
    Session session, {
    required String fileName,
    required String mimeType,
    required int fileSize,
  }) async {
    // Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw MediaUploadException(
        code: 'unauthenticated',
        message: 'Please log in to upload files',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw MediaUploadException(
        code: 'user_not_found',
        message: 'User profile not found',
      );
    }
    
    // Validate MIME type
    if (!StorageConfig.allowedMimeTypes['image']!.contains(mimeType)) {
      throw MediaUploadException(
        code: 'invalid_type',
        message: 'File type not allowed. Supported: JPEG, PNG, GIF, WebP',
      );
    }
    
    // Validate file size
    if (!StorageConfig.isValidFileSize('image', fileSize)) {
      final maxMB = StorageConfig.maxFileSizes['image']! ~/ (1024 * 1024);
      throw MediaUploadException(
        code: 'file_too_large',
        message: 'Image must be smaller than ${maxMB}MB',
      );
    }
    
    // Check user's upload quota (optional)
    await _checkUploadQuota(session, user.id!, fileSize);
    
    // Generate unique file ID and path
    final fileId = _uuid.v4();
    final extension = StorageConfig.getExtensionFromMimeType(mimeType);
    final storageKey = StorageConfig.getMediaPath(
      contentType: mimeType,
      userId: user.id!,
      fileId: fileId,
      extension: extension,
    );
    
    // Create pending media record
    final mediaFile = await MediaFile.db.insertRow(
      session,
      MediaFile(
        userId: user.id!,
        fileName: fileId,
        originalFileName: _sanitizeFileName(fileName),
        mimeType: mimeType,
        fileSize: fileSize,
        storageKey: storageKey,
        storageBucket: StorageConfig.publicStorage,
        mediaType: 'image',
        status: 'pending',
        createdAt: DateTime.now(),
      ),
    );
    
    // Generate signed upload URL
    final uploadUrl = await session.storage.createDirectFileUploadDescription(
      storageId: StorageConfig.publicStorage,
      path: storageKey,
      validDuration: Duration(seconds: StorageConfig.uploadUrlExpiration),
    );
    
    return UploadUrlResponse(
      uploadUrl: uploadUrl.uploadUrl,
      uploadHeaders: uploadUrl.httpHeaders,
      mediaFileId: mediaFile.id!,
      storageKey: storageKey,
      expiresAt: DateTime.now().add(
        Duration(seconds: StorageConfig.uploadUrlExpiration),
      ),
    );
  }

  /// Confirm upload completion and trigger processing
  Future<MediaFile> confirmImageUpload(
    Session session, {
    required int mediaFileId,
    int? width,
    int? height,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw MediaUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw MediaUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Get media record
    final mediaFile = await MediaFile.db.findById(session, mediaFileId);
    if (mediaFile == null) {
      throw MediaUploadException(
        code: 'not_found',
        message: 'Upload record not found',
      );
    }
    
    // Verify ownership
    if (mediaFile.userId != user.id) {
      throw MediaUploadException(
        code: 'access_denied',
        message: 'You do not own this upload',
      );
    }
    
    // Check status
    if (mediaFile.status != 'pending') {
      throw MediaUploadException(
        code: 'invalid_status',
        message: 'Upload already processed',
      );
    }
    
    // Verify file exists in storage
    final exists = await session.storage.fileExists(
      storageId: StorageConfig.publicStorage,
      path: mediaFile.storageKey,
    );
    
    if (!exists) {
      throw MediaUploadException(
        code: 'file_not_found',
        message: 'File not found in storage. Please upload again.',
      );
    }
    
    // Get public URL
    final publicUrl = await session.storage.getPublicUrl(
      storageId: StorageConfig.publicStorage,
      path: mediaFile.storageKey,
    );
    
    // Update status to processing
    final updatedMedia = await MediaFile.db.updateRow(
      session,
      mediaFile.copyWith(
        status: 'processing',
        publicUrl: publicUrl?.toString(),
        width: width,
        height: height,
      ),
    );
    
    // Queue thumbnail generation (async)
    await _queueThumbnailGeneration(session, updatedMedia);
    
    return updatedMedia;
  }

  /// Direct upload for small images (< 1MB)
  Future<MediaFile> uploadImageDirect(
    Session session, {
    required String fileName,
    required String mimeType,
    required Uint8List imageData,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw MediaUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw MediaUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Validate size (1MB max for direct upload)
    if (imageData.length > 1024 * 1024) {
      throw MediaUploadException(
        code: 'file_too_large',
        message: 'Use signed URL upload for files larger than 1MB',
      );
    }
    
    // Validate MIME type
    if (!StorageConfig.allowedMimeTypes['image']!.contains(mimeType)) {
      throw MediaUploadException(
        code: 'invalid_type',
        message: 'File type not allowed',
      );
    }
    
    // Verify actual content type matches claimed type
    final detectedType = _detectImageType(imageData);
    if (detectedType != null && detectedType != mimeType) {
      throw MediaUploadException(
        code: 'type_mismatch',
        message: 'File content does not match declared type',
      );
    }
    
    // Generate content hash for deduplication
    final contentHash = sha256.convert(imageData).toString();
    
    // Check for duplicate
    final existing = await MediaFile.db.findFirstRow(
      session,
      where: (t) => t.contentHash.equals(contentHash) &
                    t.userId.equals(user.id!) &
                    t.deletedAt.equals(null),
    );
    
    if (existing != null) {
      // Return existing file instead of uploading duplicate
      return existing;
    }
    
    // Generate path
    final fileId = _uuid.v4();
    final extension = StorageConfig.getExtensionFromMimeType(mimeType);
    final storageKey = StorageConfig.getMediaPath(
      contentType: mimeType,
      userId: user.id!,
      fileId: fileId,
      extension: extension,
    );
    
    // Upload to storage
    await session.storage.storeFile(
      storageId: StorageConfig.publicStorage,
      path: storageKey,
      byteData: ByteData.view(imageData.buffer),
    );
    
    // Get public URL
    final publicUrl = await session.storage.getPublicUrl(
      storageId: StorageConfig.publicStorage,
      path: storageKey,
    );
    
    // Create media record
    final mediaFile = await MediaFile.db.insertRow(
      session,
      MediaFile(
        userId: user.id!,
        fileName: fileId,
        originalFileName: _sanitizeFileName(fileName),
        mimeType: mimeType,
        fileSize: imageData.length,
        storageKey: storageKey,
        storageBucket: StorageConfig.publicStorage,
        publicUrl: publicUrl?.toString(),
        mediaType: 'image',
        status: 'processing',
        contentHash: contentHash,
        createdAt: DateTime.now(),
      ),
    );
    
    // Queue thumbnail generation
    await _queueThumbnailGeneration(session, mediaFile);
    
    return mediaFile;
  }

  /// Get user's media files
  Future<PaginatedMedia> getUserMedia(
    Session session, {
    String? mediaType,
    String? cursor,
    int limit = 20,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw MediaUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      return PaginatedMedia(items: [], hasMore: false);
    }
    
    // Parse cursor
    DateTime? cursorTime;
    if (cursor != null) {
      cursorTime = DateTime.tryParse(cursor);
    }
    
    // Build query
    var whereClause = MediaFile.t.userId.equals(user.id!) &
        MediaFile.t.deletedAt.equals(null) &
        MediaFile.t.status.notEquals('failed');
    
    if (mediaType != null) {
      whereClause = whereClause & MediaFile.t.mediaType.equals(mediaType);
    }
    
    if (cursorTime != null) {
      whereClause = whereClause & 
          MediaFile.t.createdAt.lessThan(cursorTime);
    }
    
    final media = await MediaFile.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: limit + 1,
    );
    
    final hasMore = media.length > limit;
    final items = hasMore ? media.take(limit).toList() : media;
    
    String? nextCursor;
    if (hasMore && items.isNotEmpty) {
      nextCursor = items.last.createdAt.toIso8601String();
    }
    
    return PaginatedMedia(
      items: items,
      hasMore: hasMore,
      nextCursor: nextCursor,
    );
  }

  /// Delete a media file
  Future<void> deleteMedia(
    Session session, {
    required int mediaFileId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw MediaUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw MediaUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    final mediaFile = await MediaFile.db.findById(session, mediaFileId);
    if (mediaFile == null || mediaFile.deletedAt != null) {
      throw MediaUploadException(
        code: 'not_found',
        message: 'Media file not found',
      );
    }
    
    if (mediaFile.userId != user.id) {
      throw MediaUploadException(
        code: 'access_denied',
        message: 'You do not own this file',
      );
    }
    
    // Soft delete the record
    await MediaFile.db.updateRow(
      session,
      mediaFile.copyWith(deletedAt: DateTime.now()),
    );
    
    // Queue actual file deletion (run async to not block response)
    await _queueFileDeletion(session, mediaFile);
  }

  // Helper methods
  
  String _sanitizeFileName(String fileName) {
    // Remove path separators and special characters
    return fileName
        .replaceAll(RegExp(r'[/\\:*?"<>|]'), '_')
        .replaceAll(RegExp(r'\s+'), '_')
        .toLowerCase();
  }
  
  String? _detectImageType(Uint8List data) {
    if (data.length < 4) return null;
    
    // Check magic bytes
    if (data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF) {
      return 'image/jpeg';
    }
    if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47) {
      return 'image/png';
    }
    if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46) {
      return 'image/gif';
    }
    if (data[0] == 0x52 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x46) {
      return 'image/webp';
    }
    
    return null;
  }
  
  Future<void> _checkUploadQuota(
    Session session,
    int userId,
    int newFileSize,
  ) async {
    // Calculate user's total storage usage
    final result = await session.db.unsafeQuery(
      '''
      SELECT COALESCE(SUM(file_size), 0) as total_size
      FROM media_files
      WHERE user_id = @userId AND deleted_at IS NULL
      ''',
      parameters: QueryParameters.named({'userId': userId}),
    );
    
    final currentUsage = result.first['total_size'] as int;
    const maxStorage = 1024 * 1024 * 1024; // 1 GB per user
    
    if (currentUsage + newFileSize > maxStorage) {
      throw MediaUploadException(
        code: 'quota_exceeded',
        message: 'Storage quota exceeded. Please delete some files.',
      );
    }
  }
  
  Future<void> _queueThumbnailGeneration(
    Session session,
    MediaFile mediaFile,
  ) async {
    // Queue for background processing
    // In production, use a proper job queue (e.g., BullMQ, Celery)
    await session.messages.postMessage(
      'media-processor',
      MediaProcessingJob(
        type: 'generate_thumbnails',
        mediaFileId: mediaFile.id!,
        storageKey: mediaFile.storageKey,
        mimeType: mediaFile.mimeType,
      ),
    );
  }
  
  Future<void> _queueFileDeletion(
    Session session,
    MediaFile mediaFile,
  ) async {
    await session.messages.postMessage(
      'media-processor',
      MediaProcessingJob(
        type: 'delete_files',
        mediaFileId: mediaFile.id!,
        storageKey: mediaFile.storageKey,
        mimeType: mediaFile.mimeType,
      ),
    );
  }
}

/// Response containing signed upload URL
class UploadUrlResponse {
  final String uploadUrl;
  final Map<String, String> uploadHeaders;
  final int mediaFileId;
  final String storageKey;
  final DateTime expiresAt;
  
  UploadUrlResponse({
    required this.uploadUrl,
    required this.uploadHeaders,
    required this.mediaFileId,
    required this.storageKey,
    required this.expiresAt,
  });
}

/// Paginated media response
class PaginatedMedia {
  final List<MediaFile> items;
  final bool hasMore;
  final String? nextCursor;
  
  PaginatedMedia({
    required this.items,
    required this.hasMore,
    this.nextCursor,
  });
}

/// Media processing job for queue
class MediaProcessingJob {
  final String type;
  final int mediaFileId;
  final String storageKey;
  final String mimeType;
  
  MediaProcessingJob({
    required this.type,
    required this.mediaFileId,
    required this.storageKey,
    required this.mimeType,
  });
}

class MediaUploadException implements Exception {
  final String code;
  final String message;
  
  MediaUploadException({required this.code, required this.message});
  
  @override
  String toString() => 'MediaUploadException: [$code] $message';
}
```
