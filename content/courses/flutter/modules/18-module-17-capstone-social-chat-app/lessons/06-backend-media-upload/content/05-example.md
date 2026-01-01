---
type: "EXAMPLE"
title: "Video Upload Handling"
---


**Large File Uploads with Progress Tracking**

Videos require special handling due to their size. We implement chunked uploads for reliability and progress tracking for user feedback.



```dart
// server/lib/src/endpoints/video_upload_endpoint.dart
import 'dart:convert';
import 'package:serverpod/serverpod.dart';
import 'package:uuid/uuid.dart';
import '../generated/protocol.dart';
import '../util/storage_config.dart';

/// Endpoint for handling video uploads with chunked upload support
class VideoUploadEndpoint extends Endpoint {
  static const _uuid = Uuid();
  
  /// Chunk size for multipart uploads (5MB minimum for S3)
  static const int chunkSize = 5 * 1024 * 1024;  // 5MB

  /// Initialize a chunked video upload
  Future<ChunkedUploadSession> initializeVideoUpload(
    Session session, {
    required String fileName,
    required String mimeType,
    required int totalSize,
    int? durationSeconds,
  }) async {
    // Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw VideoUploadException(
        code: 'unauthenticated',
        message: 'Please log in to upload videos',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw VideoUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Validate MIME type
    if (!StorageConfig.allowedMimeTypes['video']!.contains(mimeType)) {
      throw VideoUploadException(
        code: 'invalid_type',
        message: 'Video type not supported. Use MP4, WebM, or MOV.',
      );
    }
    
    // Validate size
    if (!StorageConfig.isValidFileSize('video', totalSize)) {
      final maxMB = StorageConfig.maxFileSizes['video']! ~/ (1024 * 1024);
      throw VideoUploadException(
        code: 'file_too_large',
        message: 'Video must be smaller than ${maxMB}MB',
      );
    }
    
    // Check quota
    await _checkUploadQuota(session, user.id!, totalSize);
    
    // Generate file ID and path
    final fileId = _uuid.v4();
    final extension = StorageConfig.getExtensionFromMimeType(mimeType);
    final storageKey = StorageConfig.getMediaPath(
      contentType: mimeType,
      userId: user.id!,
      fileId: fileId,
      extension: extension,
    );
    
    // Calculate number of chunks
    final totalChunks = (totalSize / chunkSize).ceil();
    
    // Create upload session record
    final uploadSession = await ChunkedUploadSession.db.insertRow(
      session,
      ChunkedUploadSession(
        userId: user.id!,
        fileId: fileId,
        fileName: _sanitizeFileName(fileName),
        mimeType: mimeType,
        totalSize: totalSize,
        storageKey: storageKey,
        totalChunks: totalChunks,
        uploadedChunks: 0,
        uploadedBytes: 0,
        status: 'initialized',
        expiresAt: DateTime.now().add(Duration(hours: 24)),
        createdAt: DateTime.now(),
      ),
    );
    
    // Create media file record (pending status)
    final mediaFile = await MediaFile.db.insertRow(
      session,
      MediaFile(
        userId: user.id!,
        fileName: fileId,
        originalFileName: _sanitizeFileName(fileName),
        mimeType: mimeType,
        fileSize: totalSize,
        storageKey: storageKey,
        storageBucket: StorageConfig.publicStorage,
        mediaType: 'video',
        duration: durationSeconds,
        status: 'pending',
        createdAt: DateTime.now(),
      ),
    );
    
    // Update session with media file ID
    await ChunkedUploadSession.db.updateRow(
      session,
      uploadSession.copyWith(mediaFileId: mediaFile.id),
    );
    
    return uploadSession.copyWith(mediaFileId: mediaFile.id);
  }

  /// Get signed URL for uploading a specific chunk
  Future<ChunkUploadUrl> getChunkUploadUrl(
    Session session, {
    required int uploadSessionId,
    required int chunkIndex,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw VideoUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw VideoUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Get upload session
    final uploadSession = await ChunkedUploadSession.db.findById(
      session,
      uploadSessionId,
    );
    
    if (uploadSession == null) {
      throw VideoUploadException(
        code: 'session_not_found',
        message: 'Upload session not found',
      );
    }
    
    // Verify ownership
    if (uploadSession.userId != user.id) {
      throw VideoUploadException(
        code: 'access_denied',
        message: 'You do not own this upload session',
      );
    }
    
    // Check expiration
    if (DateTime.now().isAfter(uploadSession.expiresAt)) {
      throw VideoUploadException(
        code: 'session_expired',
        message: 'Upload session has expired. Please start a new upload.',
      );
    }
    
    // Validate chunk index
    if (chunkIndex < 0 || chunkIndex >= uploadSession.totalChunks) {
      throw VideoUploadException(
        code: 'invalid_chunk',
        message: 'Invalid chunk index',
      );
    }
    
    // Generate chunk key
    final chunkKey = '${uploadSession.storageKey}.chunk_$chunkIndex';
    
    // Get signed URL for this chunk
    final uploadDescription = await session.storage
        .createDirectFileUploadDescription(
      storageId: StorageConfig.publicStorage,
      path: chunkKey,
      validDuration: Duration(minutes: 30),
    );
    
    return ChunkUploadUrl(
      chunkIndex: chunkIndex,
      uploadUrl: uploadDescription.uploadUrl,
      uploadHeaders: uploadDescription.httpHeaders,
      expiresAt: DateTime.now().add(Duration(minutes: 30)),
    );
  }

  /// Confirm a chunk has been uploaded
  Future<ChunkUploadProgress> confirmChunkUpload(
    Session session, {
    required int uploadSessionId,
    required int chunkIndex,
    required int chunkSize,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw VideoUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw VideoUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Get and validate session
    final uploadSession = await ChunkedUploadSession.db.findById(
      session,
      uploadSessionId,
    );
    
    if (uploadSession == null || uploadSession.userId != user.id) {
      throw VideoUploadException(
        code: 'session_not_found',
        message: 'Upload session not found',
      );
    }
    
    // Verify chunk exists in storage
    final chunkKey = '${uploadSession.storageKey}.chunk_$chunkIndex';
    final exists = await session.storage.fileExists(
      storageId: StorageConfig.publicStorage,
      path: chunkKey,
    );
    
    if (!exists) {
      throw VideoUploadException(
        code: 'chunk_not_found',
        message: 'Chunk file not found in storage',
      );
    }
    
    // Update progress
    final newUploadedChunks = uploadSession.uploadedChunks + 1;
    final newUploadedBytes = uploadSession.uploadedBytes + chunkSize;
    final isComplete = newUploadedChunks >= uploadSession.totalChunks;
    
    await ChunkedUploadSession.db.updateRow(
      session,
      uploadSession.copyWith(
        uploadedChunks: newUploadedChunks,
        uploadedBytes: newUploadedBytes,
        status: isComplete ? 'assembling' : 'uploading',
        updatedAt: DateTime.now(),
      ),
    );
    
    final progress = newUploadedBytes / uploadSession.totalSize;
    
    return ChunkUploadProgress(
      uploadSessionId: uploadSessionId,
      uploadedChunks: newUploadedChunks,
      totalChunks: uploadSession.totalChunks,
      uploadedBytes: newUploadedBytes,
      totalBytes: uploadSession.totalSize,
      progress: progress,
      isComplete: isComplete,
    );
  }

  /// Complete the upload by assembling chunks
  Future<MediaFile> completeVideoUpload(
    Session session, {
    required int uploadSessionId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw VideoUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw VideoUploadException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    final uploadSession = await ChunkedUploadSession.db.findById(
      session,
      uploadSessionId,
    );
    
    if (uploadSession == null || uploadSession.userId != user.id) {
      throw VideoUploadException(
        code: 'session_not_found',
        message: 'Upload session not found',
      );
    }
    
    // Verify all chunks uploaded
    if (uploadSession.uploadedChunks < uploadSession.totalChunks) {
      throw VideoUploadException(
        code: 'incomplete',
        message: 'Not all chunks have been uploaded',
      );
    }
    
    try {
      // Assemble chunks into final file
      await _assembleChunks(session, uploadSession);
      
      // Get public URL
      final publicUrl = await session.storage.getPublicUrl(
        storageId: StorageConfig.publicStorage,
        path: uploadSession.storageKey,
      );
      
      // Update media file
      final mediaFile = await MediaFile.db.findById(
        session,
        uploadSession.mediaFileId!,
      );
      
      if (mediaFile == null) {
        throw VideoUploadException(
          code: 'media_not_found',
          message: 'Media record not found',
        );
      }
      
      final updatedMedia = await MediaFile.db.updateRow(
        session,
        mediaFile.copyWith(
          publicUrl: publicUrl?.toString(),
          status: 'processing',
        ),
      );
      
      // Update session status
      await ChunkedUploadSession.db.updateRow(
        session,
        uploadSession.copyWith(
          status: 'completed',
          completedAt: DateTime.now(),
        ),
      );
      
      // Queue video processing (thumbnail extraction, transcoding)
      await _queueVideoProcessing(session, updatedMedia);
      
      return updatedMedia;
    } catch (e) {
      // Mark as failed
      await ChunkedUploadSession.db.updateRow(
        session,
        uploadSession.copyWith(
          status: 'failed',
          errorMessage: e.toString(),
        ),
      );
      
      rethrow;
    }
  }

  /// Get upload progress
  Future<ChunkUploadProgress> getUploadProgress(
    Session session, {
    required int uploadSessionId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw VideoUploadException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    final uploadSession = await ChunkedUploadSession.db.findById(
      session,
      uploadSessionId,
    );
    
    if (uploadSession == null || uploadSession.userId != user?.id) {
      throw VideoUploadException(
        code: 'session_not_found',
        message: 'Upload session not found',
      );
    }
    
    final progress = uploadSession.uploadedBytes / uploadSession.totalSize;
    
    return ChunkUploadProgress(
      uploadSessionId: uploadSessionId,
      uploadedChunks: uploadSession.uploadedChunks,
      totalChunks: uploadSession.totalChunks,
      uploadedBytes: uploadSession.uploadedBytes,
      totalBytes: uploadSession.totalSize,
      progress: progress,
      isComplete: uploadSession.status == 'completed',
      status: uploadSession.status,
    );
  }

  /// Cancel an upload and clean up
  Future<void> cancelUpload(
    Session session, {
    required int uploadSessionId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) return;
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    final uploadSession = await ChunkedUploadSession.db.findById(
      session,
      uploadSessionId,
    );
    
    if (uploadSession == null || uploadSession.userId != user?.id) {
      return;
    }
    
    // Delete uploaded chunks
    for (var i = 0; i < uploadSession.uploadedChunks; i++) {
      final chunkKey = '${uploadSession.storageKey}.chunk_$i';
      try {
        await session.storage.deleteFile(
          storageId: StorageConfig.publicStorage,
          path: chunkKey,
        );
      } catch (e) {
        // Ignore cleanup errors
      }
    }
    
    // Delete media record if exists
    if (uploadSession.mediaFileId != null) {
      final mediaFile = await MediaFile.db.findById(
        session,
        uploadSession.mediaFileId!,
      );
      if (mediaFile != null) {
        await MediaFile.db.deleteRow(session, mediaFile);
      }
    }
    
    // Delete session
    await ChunkedUploadSession.db.deleteRow(session, uploadSession);
  }

  // Helper methods
  
  Future<void> _assembleChunks(
    Session session,
    ChunkedUploadSession uploadSession,
  ) async {
    // For S3-compatible storage, we'd use multipart upload completion
    // For simpler storage, we concatenate chunks
    
    final chunks = <int>[];
    
    for (var i = 0; i < uploadSession.totalChunks; i++) {
      final chunkKey = '${uploadSession.storageKey}.chunk_$i';
      final chunkData = await session.storage.retrieveFile(
        storageId: StorageConfig.publicStorage,
        path: chunkKey,
      );
      
      if (chunkData == null) {
        throw VideoUploadException(
          code: 'chunk_missing',
          message: 'Chunk $i is missing',
        );
      }
      
      chunks.addAll(chunkData.buffer.asUint8List());
    }
    
    // Upload assembled file
    await session.storage.storeFile(
      storageId: StorageConfig.publicStorage,
      path: uploadSession.storageKey,
      byteData: ByteData.view(Uint8List.fromList(chunks).buffer),
    );
    
    // Clean up chunks
    for (var i = 0; i < uploadSession.totalChunks; i++) {
      final chunkKey = '${uploadSession.storageKey}.chunk_$i';
      await session.storage.deleteFile(
        storageId: StorageConfig.publicStorage,
        path: chunkKey,
      );
    }
  }
  
  Future<void> _queueVideoProcessing(
    Session session,
    MediaFile mediaFile,
  ) async {
    await session.messages.postMessage(
      'media-processor',
      MediaProcessingJob(
        type: 'process_video',
        mediaFileId: mediaFile.id!,
        storageKey: mediaFile.storageKey,
        mimeType: mediaFile.mimeType,
      ),
    );
  }
  
  Future<void> _checkUploadQuota(
    Session session,
    int userId,
    int newFileSize,
  ) async {
    // Same as image upload quota check
    final result = await session.db.unsafeQuery(
      '''
      SELECT COALESCE(SUM(file_size), 0) as total_size
      FROM media_files
      WHERE user_id = @userId AND deleted_at IS NULL
      ''',
      parameters: QueryParameters.named({'userId': userId}),
    );
    
    final currentUsage = result.first['total_size'] as int;
    const maxStorage = 5 * 1024 * 1024 * 1024; // 5 GB for video
    
    if (currentUsage + newFileSize > maxStorage) {
      throw VideoUploadException(
        code: 'quota_exceeded',
        message: 'Storage quota exceeded',
      );
    }
  }
  
  String _sanitizeFileName(String fileName) {
    return fileName
        .replaceAll(RegExp(r'[/\\:*?"<>|]'), '_')
        .replaceAll(RegExp(r'\s+'), '_')
        .toLowerCase();
  }
}

/// Chunked upload session model
class ChunkedUploadSession {
  final int? id;
  final int userId;
  final int? mediaFileId;
  final String fileId;
  final String fileName;
  final String mimeType;
  final int totalSize;
  final String storageKey;
  final int totalChunks;
  final int uploadedChunks;
  final int uploadedBytes;
  final String status;
  final String? errorMessage;
  final DateTime expiresAt;
  final DateTime createdAt;
  final DateTime? updatedAt;
  final DateTime? completedAt;
  
  ChunkedUploadSession({
    this.id,
    required this.userId,
    this.mediaFileId,
    required this.fileId,
    required this.fileName,
    required this.mimeType,
    required this.totalSize,
    required this.storageKey,
    required this.totalChunks,
    required this.uploadedChunks,
    required this.uploadedBytes,
    required this.status,
    this.errorMessage,
    required this.expiresAt,
    required this.createdAt,
    this.updatedAt,
    this.completedAt,
  });
  
  ChunkedUploadSession copyWith({
    int? mediaFileId,
    int? uploadedChunks,
    int? uploadedBytes,
    String? status,
    String? errorMessage,
    DateTime? updatedAt,
    DateTime? completedAt,
  }) {
    return ChunkedUploadSession(
      id: id,
      userId: userId,
      mediaFileId: mediaFileId ?? this.mediaFileId,
      fileId: fileId,
      fileName: fileName,
      mimeType: mimeType,
      totalSize: totalSize,
      storageKey: storageKey,
      totalChunks: totalChunks,
      uploadedChunks: uploadedChunks ?? this.uploadedChunks,
      uploadedBytes: uploadedBytes ?? this.uploadedBytes,
      status: status ?? this.status,
      errorMessage: errorMessage ?? this.errorMessage,
      expiresAt: expiresAt,
      createdAt: createdAt,
      updatedAt: updatedAt ?? this.updatedAt,
      completedAt: completedAt ?? this.completedAt,
    );
  }
}

/// Chunk upload URL response
class ChunkUploadUrl {
  final int chunkIndex;
  final String uploadUrl;
  final Map<String, String> uploadHeaders;
  final DateTime expiresAt;
  
  ChunkUploadUrl({
    required this.chunkIndex,
    required this.uploadUrl,
    required this.uploadHeaders,
    required this.expiresAt,
  });
}

/// Upload progress information
class ChunkUploadProgress {
  final int uploadSessionId;
  final int uploadedChunks;
  final int totalChunks;
  final int uploadedBytes;
  final int totalBytes;
  final double progress;
  final bool isComplete;
  final String? status;
  
  ChunkUploadProgress({
    required this.uploadSessionId,
    required this.uploadedChunks,
    required this.totalChunks,
    required this.uploadedBytes,
    required this.totalBytes,
    required this.progress,
    required this.isComplete,
    this.status,
  });
  
  int get remainingBytes => totalBytes - uploadedBytes;
  int get remainingChunks => totalChunks - uploadedChunks;
  String get progressPercent => '${(progress * 100).toStringAsFixed(1)}%';
}

class VideoUploadException implements Exception {
  final String code;
  final String message;
  
  VideoUploadException({required this.code, required this.message});
  
  @override
  String toString() => 'VideoUploadException: [$code] $message';
}
```
