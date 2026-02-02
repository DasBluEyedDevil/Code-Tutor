---
type: "EXAMPLE"
title: "Implementing File Upload API"
---

Let us create a complete file upload system in Serverpod. We will build an endpoint that handles file uploads securely.



```dart
// File: lib/src/endpoints/file_endpoint.dart

import 'dart:typed_data';
import 'package:serverpod/serverpod.dart';
import 'package:uuid/uuid.dart';
import '../generated/protocol.dart';

/// Handles file upload and download operations.
class FileEndpoint extends Endpoint {
  static const _uuid = Uuid();
  
  // Allowed file types with their MIME types
  static const _allowedImageTypes = {
    'jpg': 'image/jpeg',
    'jpeg': 'image/jpeg',
    'png': 'image/png',
    'gif': 'image/gif',
    'webp': 'image/webp',
  };
  
  static const _allowedDocumentTypes = {
    'pdf': 'application/pdf',
    'doc': 'application/msword',
    'docx': 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
  };
  
  // Maximum file sizes in bytes
  static const _maxImageSize = 5 * 1024 * 1024; // 5 MB
  static const _maxDocumentSize = 20 * 1024 * 1024; // 20 MB
  
  /// Get a URL for uploading a file directly to cloud storage.
  /// This returns a pre-signed URL that the client can use to upload.
  Future<FileUploadInfo> getUploadUrl(
    Session session, {
    required String fileName,
    required String contentType,
    required int fileSize,
    required String category, // 'avatar', 'document', 'media'
  }) async {
    // Verify user is authenticated
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationRequiredException();
    }
    
    // Validate file extension
    final extension = _getExtension(fileName).toLowerCase();
    final allowedTypes = category == 'document' 
        ? _allowedDocumentTypes 
        : _allowedImageTypes;
    
    if (!allowedTypes.containsKey(extension)) {
      throw InvalidFileTypeException(
        'File type .$extension is not allowed for $category',
      );
    }
    
    // Validate content type matches extension
    final expectedContentType = allowedTypes[extension];
    if (contentType != expectedContentType) {
      throw InvalidFileTypeException(
        'Content type $contentType does not match extension .$extension',
      );
    }
    
    // Validate file size
    final maxSize = category == 'document' ? _maxDocumentSize : _maxImageSize;
    if (fileSize > maxSize) {
      throw FileTooLargeException(
        'File size ${fileSize ~/ 1024}KB exceeds maximum ${maxSize ~/ 1024}KB',
      );
    }
    
    // Generate safe storage path
    final storagePath = _generateStoragePath(
      userId: userId,
      category: category,
      extension: extension,
    );
    
    // Create upload description for Serverpod storage
    final uploadDescription = await session.storage.createDirectFileUploadDescription(
      storageId: category == 'avatar' ? 'public' : 'private',
      path: storagePath,
    );
    
    // Record file metadata in database (pending upload)
    final fileRecord = FileRecord(
      userId: userId,
      storagePath: storagePath,
      originalFileName: _sanitizeFileName(fileName),
      contentType: contentType,
      fileSize: fileSize,
      category: category,
      uploadStatus: 'pending',
      createdAt: DateTime.now(),
    );
    
    final savedRecord = await FileRecord.db.insertRow(session, fileRecord);
    
    return FileUploadInfo(
      fileId: savedRecord.id!,
      uploadUrl: uploadDescription.url,
      uploadMethod: uploadDescription.httpMethod,
      uploadHeaders: uploadDescription.headers,
      expiresAt: DateTime.now().add(Duration(minutes: 15)),
    );
  }
  
  /// Confirm that a file upload was completed.
  /// Client calls this after successfully uploading to the signed URL.
  Future<FileRecord> confirmUpload(
    Session session, {
    required int fileId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationRequiredException();
    }
    
    // Get the file record
    final fileRecord = await FileRecord.db.findById(session, fileId);
    if (fileRecord == null) {
      throw FileNotFoundException('File record not found');
    }
    
    // Verify ownership
    if (fileRecord.userId != userId) {
      throw UnauthorizedException('You do not own this file');
    }
    
    // Verify file exists in storage
    final exists = await session.storage.fileExists(
      storageId: fileRecord.category == 'avatar' ? 'public' : 'private',
      path: fileRecord.storagePath,
    );
    
    if (!exists) {
      throw FileNotFoundException('File was not uploaded to storage');
    }
    
    // Update status to completed
    final updatedRecord = fileRecord.copyWith(
      uploadStatus: 'completed',
      uploadedAt: DateTime.now(),
    );
    
    await FileRecord.db.updateRow(session, updatedRecord);
    return updatedRecord;
  }
  
  /// Get a download URL for a private file.
  Future<String> getDownloadUrl(
    Session session, {
    required int fileId,
    int expirationMinutes = 60,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationRequiredException();
    }
    
    // Get file record
    final fileRecord = await FileRecord.db.findById(session, fileId);
    if (fileRecord == null) {
      throw FileNotFoundException('File not found');
    }
    
    // Check access permission
    if (!await _canAccessFile(session, userId, fileRecord)) {
      throw UnauthorizedException('You do not have access to this file');
    }
    
    // For public files, return the public URL
    if (fileRecord.category == 'avatar') {
      return session.storage.getPublicUrl(
        storageId: 'public',
        path: fileRecord.storagePath,
      );
    }
    
    // For private files, create a signed URL
    final signedUrl = await session.storage.createDirectFileDownloadUrl(
      storageId: 'private',
      path: fileRecord.storagePath,
      expiration: Duration(minutes: expirationMinutes),
    );
    
    return signedUrl;
  }
  
  /// Delete a file.
  Future<bool> deleteFile(
    Session session, {
    required int fileId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationRequiredException();
    }
    
    final fileRecord = await FileRecord.db.findById(session, fileId);
    if (fileRecord == null) {
      return false; // Already deleted
    }
    
    // Only owner can delete
    if (fileRecord.userId != userId) {
      throw UnauthorizedException('You do not own this file');
    }
    
    // Delete from storage
    await session.storage.deleteFile(
      storageId: fileRecord.category == 'avatar' ? 'public' : 'private',
      path: fileRecord.storagePath,
    );
    
    // Delete database record
    await FileRecord.db.deleteRow(session, fileRecord);
    
    return true;
  }
  
  // Helper: Generate a safe storage path
  String _generateStoragePath({
    required int userId,
    required String category,
    required String extension,
  }) {
    final uniqueId = _uuid.v4();
    final date = DateTime.now();
    final datePath = '${date.year}/${date.month.toString().padLeft(2, '0')}';
    
    return '$category/$datePath/user_$userId/$uniqueId.$extension';
  }
  
  // Helper: Extract file extension
  String _getExtension(String fileName) {
    final lastDot = fileName.lastIndexOf('.');
    if (lastDot == -1 || lastDot == fileName.length - 1) {
      throw InvalidFileTypeException('File must have an extension');
    }
    return fileName.substring(lastDot + 1);
  }
  
  // Helper: Sanitize file name (remove dangerous characters)
  String _sanitizeFileName(String fileName) {
    return fileName
        .replaceAll(RegExp(r'[^a-zA-Z0-9._-]'), '_')
        .replaceAll(RegExp(r'_{2,}'), '_');
  }
  
  // Helper: Check if user can access a file
  Future<bool> _canAccessFile(
    Session session,
    int userId,
    FileRecord fileRecord,
  ) async {
    // Owner always has access
    if (fileRecord.userId == userId) return true;
    
    // Public files (avatars) are accessible to all authenticated users
    if (fileRecord.category == 'avatar') return true;
    
    // Check for shared access (implement your sharing logic here)
    // final hasSharedAccess = await FileShare.db.findFirstRow(
    //   session,
    //   where: (t) => t.fileId.equals(fileRecord.id) & t.userId.equals(userId),
    // );
    // return hasSharedAccess != null;
    
    return false;
  }
}
```
