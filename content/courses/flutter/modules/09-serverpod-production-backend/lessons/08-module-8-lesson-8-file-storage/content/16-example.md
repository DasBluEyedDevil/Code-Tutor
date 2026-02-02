---
type: "EXAMPLE"
title: "Complete File Sharing System"
---

Here is a complete implementation of a file sharing system with multiple access modes.



```dart
// File: lib/src/protocol/file_share.yaml
// class: FileShare
// fields:
//   fileId: int
//   sharedWithUserId: int
//   permission: String  # 'view', 'download', 'edit'
//   sharedByUserId: int
//   expiresAt: DateTime?
//   createdAt: DateTime

// class: FileShareLink
// fields:
//   fileId: int
//   token: String
//   permission: String
//   createdByUserId: int
//   accessCount: int
//   maxAccessCount: int?
//   expiresAt: DateTime?
//   createdAt: DateTime

// File: lib/src/services/file_access_service.dart

import 'dart:math';
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Service for managing file access permissions.
class FileAccessService {
  /// Check if a user can access a file.
  static Future<FileAccessResult> checkAccess(
    Session session, {
    required int fileId,
    required int userId,
    String? shareToken,
    required String requiredPermission, // 'view', 'download', 'edit'
  }) async {
    // Get file record
    final file = await FileRecord.db.findById(session, fileId);
    if (file == null) {
      return FileAccessResult(
        granted: false,
        reason: 'File not found',
      );
    }
    
    // Owner always has full access
    if (file.userId == userId) {
      return FileAccessResult(
        granted: true,
        reason: 'Owner access',
        effectivePermission: 'owner',
      );
    }
    
    // Check for admin role
    final userInfo = await session.auth.authenticatedUser;
    if (userInfo != null && await _isAdmin(session, userId)) {
      return FileAccessResult(
        granted: true,
        reason: 'Admin access',
        effectivePermission: 'admin',
      );
    }
    
    // Check for share token (link-based sharing)
    if (shareToken != null) {
      return _checkShareToken(session, fileId, shareToken, requiredPermission);
    }
    
    // Check for direct user share
    return _checkUserShare(session, fileId, userId, requiredPermission);
  }
  
  /// Share a file with a specific user.
  static Future<FileShare> shareWithUser(
    Session session, {
    required int fileId,
    required int targetUserId,
    required String permission,
    required int sharedByUserId,
    Duration? expiresIn,
  }) async {
    // Verify sharer owns the file or is admin
    final file = await FileRecord.db.findById(session, fileId);
    if (file == null) {
      throw FileNotFoundException('File not found');
    }
    
    if (file.userId != sharedByUserId && !await _isAdmin(session, sharedByUserId)) {
      throw UnauthorizedException('Only owner can share this file');
    }
    
    // Check if share already exists
    final existingShare = await FileShare.db.findFirstRow(
      session,
      where: (t) => t.fileId.equals(fileId) & t.sharedWithUserId.equals(targetUserId),
    );
    
    if (existingShare != null) {
      // Update existing share
      final updated = existingShare.copyWith(
        permission: permission,
        expiresAt: expiresIn != null ? DateTime.now().add(expiresIn) : null,
      );
      return await FileShare.db.updateRow(session, updated);
    }
    
    // Create new share
    final share = FileShare(
      fileId: fileId,
      sharedWithUserId: targetUserId,
      permission: permission,
      sharedByUserId: sharedByUserId,
      expiresAt: expiresIn != null ? DateTime.now().add(expiresIn) : null,
      createdAt: DateTime.now(),
    );
    
    return await FileShare.db.insertRow(session, share);
  }
  
  /// Create a shareable link for a file.
  static Future<FileShareLink> createShareLink(
    Session session, {
    required int fileId,
    required int createdByUserId,
    required String permission,
    int? maxAccessCount,
    Duration? expiresIn,
  }) async {
    // Verify creator owns the file or is admin
    final file = await FileRecord.db.findById(session, fileId);
    if (file == null) {
      throw FileNotFoundException('File not found');
    }
    
    if (file.userId != createdByUserId && !await _isAdmin(session, createdByUserId)) {
      throw UnauthorizedException('Only owner can create share links');
    }
    
    // Generate secure random token
    final token = _generateSecureToken(32);
    
    final shareLink = FileShareLink(
      fileId: fileId,
      token: token,
      permission: permission,
      createdByUserId: createdByUserId,
      accessCount: 0,
      maxAccessCount: maxAccessCount,
      expiresAt: expiresIn != null ? DateTime.now().add(expiresIn) : null,
      createdAt: DateTime.now(),
    );
    
    return await FileShareLink.db.insertRow(session, shareLink);
  }
  
  /// Revoke a share link.
  static Future<void> revokeShareLink(
    Session session, {
    required String token,
    required int userId,
  }) async {
    final shareLink = await FileShareLink.db.findFirstRow(
      session,
      where: (t) => t.token.equals(token),
    );
    
    if (shareLink == null) return;
    
    // Verify user can revoke (creator or file owner)
    final file = await FileRecord.db.findById(session, shareLink.fileId);
    if (shareLink.createdByUserId != userId && 
        (file == null || file.userId != userId) &&
        !await _isAdmin(session, userId)) {
      throw UnauthorizedException('Cannot revoke this share link');
    }
    
    await FileShareLink.db.deleteRow(session, shareLink);
  }
  
  /// List all shares for a file.
  static Future<List<FileShare>> getFileShares(
    Session session, {
    required int fileId,
    required int requestingUserId,
  }) async {
    final file = await FileRecord.db.findById(session, fileId);
    if (file == null) {
      throw FileNotFoundException('File not found');
    }
    
    // Only owner or admin can see shares
    if (file.userId != requestingUserId && !await _isAdmin(session, requestingUserId)) {
      throw UnauthorizedException('Cannot view shares for this file');
    }
    
    return await FileShare.db.find(
      session,
      where: (t) => t.fileId.equals(fileId),
    );
  }
  
  // Private helper methods
  
  static Future<FileAccessResult> _checkShareToken(
    Session session,
    int fileId,
    String token,
    String requiredPermission,
  ) async {
    final shareLink = await FileShareLink.db.findFirstRow(
      session,
      where: (t) => t.token.equals(token) & t.fileId.equals(fileId),
    );
    
    if (shareLink == null) {
      return FileAccessResult(
        granted: false,
        reason: 'Invalid share link',
      );
    }
    
    // Check expiration
    if (shareLink.expiresAt != null && shareLink.expiresAt!.isBefore(DateTime.now())) {
      return FileAccessResult(
        granted: false,
        reason: 'Share link expired',
      );
    }
    
    // Check access count limit
    if (shareLink.maxAccessCount != null && shareLink.accessCount >= shareLink.maxAccessCount!) {
      return FileAccessResult(
        granted: false,
        reason: 'Share link access limit reached',
      );
    }
    
    // Check permission level
    if (!_hasPermission(shareLink.permission, requiredPermission)) {
      return FileAccessResult(
        granted: false,
        reason: 'Insufficient permission on share link',
      );
    }
    
    // Increment access count
    final updated = shareLink.copyWith(accessCount: shareLink.accessCount + 1);
    await FileShareLink.db.updateRow(session, updated);
    
    return FileAccessResult(
      granted: true,
      reason: 'Share link access',
      effectivePermission: shareLink.permission,
    );
  }
  
  static Future<FileAccessResult> _checkUserShare(
    Session session,
    int fileId,
    int userId,
    String requiredPermission,
  ) async {
    final share = await FileShare.db.findFirstRow(
      session,
      where: (t) => t.fileId.equals(fileId) & t.sharedWithUserId.equals(userId),
    );
    
    if (share == null) {
      return FileAccessResult(
        granted: false,
        reason: 'No access to this file',
      );
    }
    
    // Check expiration
    if (share.expiresAt != null && share.expiresAt!.isBefore(DateTime.now())) {
      return FileAccessResult(
        granted: false,
        reason: 'Share expired',
      );
    }
    
    // Check permission level
    if (!_hasPermission(share.permission, requiredPermission)) {
      return FileAccessResult(
        granted: false,
        reason: 'Insufficient permission',
      );
    }
    
    return FileAccessResult(
      granted: true,
      reason: 'Shared access',
      effectivePermission: share.permission,
    );
  }
  
  static bool _hasPermission(String granted, String required) {
    const levels = {'view': 1, 'download': 2, 'edit': 3};
    final grantedLevel = levels[granted] ?? 0;
    final requiredLevel = levels[required] ?? 0;
    return grantedLevel >= requiredLevel;
  }
  
  static Future<bool> _isAdmin(Session session, int userId) async {
    // Implement your admin check logic
    // This is a placeholder - integrate with your auth system
    return false;
  }
  
  static String _generateSecureToken(int length) {
    const chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    final random = Random.secure();
    return List.generate(length, (_) => chars[random.nextInt(chars.length)]).join();
  }
}

/// Result of an access check.
class FileAccessResult {
  final bool granted;
  final String reason;
  final String? effectivePermission;
  
  FileAccessResult({
    required this.granted,
    required this.reason,
    this.effectivePermission,
  });
}
```
