---
type: "EXAMPLE"
title: "Step 9: File Attachment Handling"
---

File attachments require special handling. We use Serverpod's cloud storage integration to securely upload and serve files.



```dart
// File: lib/src/endpoints/file_endpoint.dart

import 'dart:typed_data';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class FileEndpoint extends Endpoint {
  // Maximum file size: 25 MB
  static const int maxFileSize = 25 * 1024 * 1024;
  
  // Allowed MIME types for chat attachments
  static const allowedMimeTypes = [
    // Images
    'image/jpeg',
    'image/png',
    'image/gif',
    'image/webp',
    // Documents
    'application/pdf',
    'application/msword',
    'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
    // Archives
    'application/zip',
    // Audio
    'audio/mpeg',
    'audio/wav',
    // Video
    'video/mp4',
    'video/webm',
  ];
  
  /// Get a signed upload URL for direct-to-cloud upload.
  /// Client uploads directly to cloud storage, then calls confirmUpload.
  Future<String> getUploadUrl(
    Session session, {
    required String fileName,
    required String mimeType,
    required int fileSize,
    required int chatRoomId,
  }) async {
    await _requireAuth(session);
    await _requireMembership(session, chatRoomId);
    
    // Validate file
    if (fileSize > maxFileSize) {
      throw InvalidParameterException(
        'File too large. Maximum size is ${maxFileSize ~/ (1024 * 1024)} MB',
      );
    }
    
    if (!allowedMimeTypes.contains(mimeType)) {
      throw InvalidParameterException(
        'File type not allowed: $mimeType',
      );
    }
    
    // Generate unique path
    final timestamp = DateTime.now().millisecondsSinceEpoch;
    final sanitizedName = _sanitizeFileName(fileName);
    final path = 'chat/$chatRoomId/$timestamp-$sanitizedName';
    
    // Get signed upload URL from Serverpod cloud storage
    // This allows the client to upload directly to S3/GCS
    final uploadUrl = await session.storage.createDirectFileUploadDescription(
      storageId: 'public', // Configure in config/production.yaml
      path: path,
    );
    
    return uploadUrl.uploadUrl ?? '';
  }
  
  /// Alternative: Upload file data directly through the server.
  /// Use this for smaller files or when direct upload is not available.
  Future<String> uploadFile(
    Session session, {
    required String fileName,
    required String mimeType,
    required Uint8List fileData,
    required int chatRoomId,
  }) async {
    await _requireAuth(session);
    await _requireMembership(session, chatRoomId);
    
    // Validate
    if (fileData.length > maxFileSize) {
      throw InvalidParameterException(
        'File too large. Maximum size is ${maxFileSize ~/ (1024 * 1024)} MB',
      );
    }
    
    if (!allowedMimeTypes.contains(mimeType)) {
      throw InvalidParameterException('File type not allowed: $mimeType');
    }
    
    // Generate unique path
    final timestamp = DateTime.now().millisecondsSinceEpoch;
    final sanitizedName = _sanitizeFileName(fileName);
    final path = 'chat/$chatRoomId/$timestamp-$sanitizedName';
    
    // Upload via Serverpod storage
    final stream = Stream.fromIterable([fileData]);
    await session.storage.storeFile(
      storageId: 'public',
      path: path,
      stream: stream,
      length: fileData.length,
    );
    
    // Get public URL
    final url = await session.storage.getPublicUrl(
      storageId: 'public',
      path: path,
    );
    
    return url ?? '';
  }
  
  /// Delete a file (only by message sender or room admin).
  Future<bool> deleteFile(
    Session session, {
    required int messageId,
  }) async {
    final currentUser = await _getCurrentUser(session);
    
    final message = await ChatMessage.db.findById(session, messageId);
    if (message == null) {
      throw NotFoundException('Message not found');
    }
    
    // Check permission
    final membership = await _requireMembership(session, message.chatRoomId);
    if (message.senderId != currentUser.id && membership.role != 'admin') {
      throw ForbiddenException('Cannot delete this file');
    }
    
    // Delete from storage
    if (message.attachmentUrl != null) {
      // Extract path from URL and delete
      // Implementation depends on your storage setup
    }
    
    return true;
  }
  
  /// Sanitize file name for storage.
  String _sanitizeFileName(String fileName) {
    // Remove path separators and special characters
    return fileName
        .replaceAll(RegExp(r'[/\\:*?"<>|]'), '_')
        .replaceAll(RegExp(r'\s+'), '_')
        .toLowerCase();
  }
  
  // Helper methods (same as other endpoints)
  Future<int> _requireAuth(Session session) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    return authUserId;
  }
  
  Future<ChatUser> _getCurrentUser(Session session) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    
    final user = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
    
    if (user == null) {
      throw NotFoundException('User profile not found');
    }
    
    return user;
  }
  
  Future<ChatMember> _requireMembership(
    Session session,
    int roomId,
  ) async {
    final currentUser = await _getCurrentUser(session);
    
    final membership = await ChatMember.db.findFirstRow(
      session,
      where: (t) => 
        t.chatRoomId.equals(roomId) & 
        t.userId.equals(currentUser.id!),
    );
    
    if (membership == null) {
      throw ForbiddenException('Not a member of this room');
    }
    
    return membership;
  }
}
```
