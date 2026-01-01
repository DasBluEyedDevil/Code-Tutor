/// Permission levels for file sharing.
enum Permission { view, download }

/// Represents a file in the system.
class FileRecord {
  final int id;
  final int ownerId;
  final String name;
  
  FileRecord({required this.id, required this.ownerId, required this.name});
}

/// Represents a share with a specific user.
class UserShare {
  final int fileId;
  final int sharedWithUserId;
  final Permission permission;
  final DateTime? expiresAt;
  final DateTime createdAt;
  
  UserShare({
    required this.fileId,
    required this.sharedWithUserId,
    required this.permission,
    this.expiresAt,
    required this.createdAt,
  });
  
  bool get isExpired => expiresAt != null && expiresAt!.isBefore(DateTime.now());
}

/// Represents a shareable link.
class ShareLink {
  int fileId;
  final String token;
  final Permission permission;
  int accessCount;
  final int? maxAccessCount;
  final DateTime? expiresAt;
  final DateTime createdAt;
  
  ShareLink({
    required this.fileId,
    required this.token,
    required this.permission,
    required this.accessCount,
    this.maxAccessCount,
    this.expiresAt,
    required this.createdAt,
  });
  
  bool get isExpired => expiresAt != null && expiresAt!.isBefore(DateTime.now());
  bool get isExhausted => maxAccessCount != null && accessCount >= maxAccessCount!;
}

/// Result of an access check.
class AccessResult {
  final bool hasAccess;
  final String reason;
  final Permission? grantedPermission;
  
  AccessResult({
    required this.hasAccess,
    required this.reason,
    this.grantedPermission,
  });
}

/// Manages file sharing.
class ShareManager {
  final Map<int, FileRecord> _files = {};
  final List<UserShare> _userShares = [];
  final List<ShareLink> _shareLinks = [];
  int _tokenCounter = 0;
  
  ShareManager() {
    _files[1] = FileRecord(id: 1, ownerId: 100, name: 'document.pdf');
    _files[2] = FileRecord(id: 2, ownerId: 100, name: 'photo.jpg');
    _files[3] = FileRecord(id: 3, ownerId: 200, name: 'spreadsheet.xlsx');
  }
  
  /// Share a file with a specific user.
  UserShare? shareWithUser({
    required int fileId,
    required int requestingUserId,
    required int targetUserId,
    required Permission permission,
    Duration? expiresIn,
  }) {
    // Verify the file exists
    final file = _files[fileId];
    if (file == null) return null;
    
    // Verify requestingUserId is the owner
    if (file.ownerId != requestingUserId) return null;
    
    // Don't allow sharing with yourself
    if (targetUserId == requestingUserId) return null;
    
    // Check if share already exists
    final existingIndex = _userShares.indexWhere(
      (s) => s.fileId == fileId && s.sharedWithUserId == targetUserId,
    );
    
    final newShare = UserShare(
      fileId: fileId,
      sharedWithUserId: targetUserId,
      permission: permission,
      expiresAt: expiresIn != null ? DateTime.now().add(expiresIn) : null,
      createdAt: DateTime.now(),
    );
    
    if (existingIndex >= 0) {
      // Update existing share
      _userShares[existingIndex] = newShare;
    } else {
      // Create new share
      _userShares.add(newShare);
    }
    
    return newShare;
  }
  
  /// Create a shareable link.
  ShareLink? createShareLink({
    required int fileId,
    required int requestingUserId,
    required Permission permission,
    int? maxAccessCount,
    Duration? expiresIn,
  }) {
    // Verify the file exists
    final file = _files[fileId];
    if (file == null) return null;
    
    // Verify requestingUserId is the owner
    if (file.ownerId != requestingUserId) return null;
    
    // Generate unique token
    final token = _generateToken();
    
    final link = ShareLink(
      fileId: fileId,
      token: token,
      permission: permission,
      accessCount: 0,
      maxAccessCount: maxAccessCount,
      expiresAt: expiresIn != null ? DateTime.now().add(expiresIn) : null,
      createdAt: DateTime.now(),
    );
    
    _shareLinks.add(link);
    return link;
  }
  
  /// Check if a user can access a file.
  AccessResult checkAccess({
    required int fileId,
    required int userId,
    String? shareToken,
  }) {
    // Check if file exists
    final file = _files[fileId];
    if (file == null) {
      return AccessResult(hasAccess: false, reason: 'File not found');
    }
    
    // Check if user is owner (full access)
    if (file.ownerId == userId) {
      return AccessResult(
        hasAccess: true,
        reason: 'Owner access',
        grantedPermission: Permission.download, // Full access
      );
    }
    
    // If token provided, check share link first
    if (shareToken != null) {
      final link = _shareLinks.firstWhere(
        (l) => l.token == shareToken && l.fileId == fileId,
        orElse: () => ShareLink(
          fileId: -1,
          token: '',
          permission: Permission.view,
          accessCount: 0,
          createdAt: DateTime.now(),
        ),
      );
      
      if (link.fileId == fileId) {
        if (link.isExpired) {
          return AccessResult(hasAccess: false, reason: 'Share link expired');
        }
        if (link.isExhausted) {
          return AccessResult(hasAccess: false, reason: 'Share link access limit reached');
        }
        return AccessResult(
          hasAccess: true,
          reason: 'Share link access',
          grantedPermission: link.permission,
        );
      }
    }
    
    // Check user shares
    final userShare = _userShares.firstWhere(
      (s) => s.fileId == fileId && s.sharedWithUserId == userId,
      orElse: () => UserShare(
        fileId: -1,
        sharedWithUserId: -1,
        permission: Permission.view,
        createdAt: DateTime.now(),
      ),
    );
    
    if (userShare.fileId == fileId) {
      if (userShare.isExpired) {
        return AccessResult(hasAccess: false, reason: 'Share expired');
      }
      return AccessResult(
        hasAccess: true,
        reason: 'Shared access',
        grantedPermission: userShare.permission,
      );
    }
    
    return AccessResult(hasAccess: false, reason: 'No access to this file');
  }
  
  /// Use a share link (increments access count).
  bool useShareLink(String token) {
    final index = _shareLinks.indexWhere((l) => l.token == token);
    if (index < 0) return false;
    
    final link = _shareLinks[index];
    
    // Check if expired or exhausted
    if (link.isExpired || link.isExhausted) return false;
    
    // Increment access count
    link.accessCount++;
    
    return true;
  }
  
  /// Revoke a user share.
  bool revokeUserShare({
    required int fileId,
    required int sharedWithUserId,
    required int requestingUserId,
  }) {
    // Verify requesting user is owner
    final file = _files[fileId];
    if (file == null || file.ownerId != requestingUserId) return false;
    
    // Find and remove the share
    final initialLength = _userShares.length;
    _userShares.removeWhere(
      (s) => s.fileId == fileId && s.sharedWithUserId == sharedWithUserId,
    );
    
    return _userShares.length < initialLength;
  }
  
  /// Revoke a share link.
  bool revokeShareLink({
    required String token,
    required int requestingUserId,
  }) {
    // Find the link
    final index = _shareLinks.indexWhere((l) => l.token == token);
    if (index < 0) return false;
    
    final link = _shareLinks[index];
    
    // Verify requesting user is file owner
    final file = _files[link.fileId];
    if (file == null || file.ownerId != requestingUserId) return false;
    
    // Remove the link
    _shareLinks.removeAt(index);
    return true;
  }
  
  /// List all shares for a file.
  Map<String, dynamic> getFileShares({
    required int fileId,
    required int requestingUserId,
  }) {
    // Verify requesting user is owner
    final file = _files[fileId];
    if (file == null || file.ownerId != requestingUserId) {
      return {'userShares': <UserShare>[], 'shareLinks': <ShareLink>[]};
    }
    
    return {
      'userShares': _userShares.where((s) => s.fileId == fileId).toList(),
      'shareLinks': _shareLinks.where((l) => l.fileId == fileId).toList(),
    };
  }
  
  String _generateToken() {
    _tokenCounter++;
    return 'token_${DateTime.now().millisecondsSinceEpoch}_$_tokenCounter';
  }
}

void main() {
  final manager = ShareManager();
  
  print('--- Test 1: Share with user ---');
  final share1 = manager.shareWithUser(
    fileId: 1,
    requestingUserId: 100,
    targetUserId: 101,
    permission: Permission.download,
    expiresIn: Duration(days: 7),
  );
  print('Share created: ${share1 != null}');
  
  print('\n--- Test 2: Check shared user access ---');
  final access1 = manager.checkAccess(fileId: 1, userId: 101);
  print('Has access: ${access1.hasAccess}');
  print('Permission: ${access1.grantedPermission}');
  
  print('\n--- Test 3: Create share link ---');
  final link1 = manager.createShareLink(
    fileId: 1,
    requestingUserId: 100,
    permission: Permission.view,
    maxAccessCount: 5,
  );
  print('Link created: ${link1 != null}');
  print('Token: ${link1?.token}');
  
  print('\n--- Test 4: Access with link ---');
  if (link1 != null) {
    final access2 = manager.checkAccess(
      fileId: 1,
      userId: 999,
      shareToken: link1.token,
    );
    print('Has access: ${access2.hasAccess}');
    
    final used = manager.useShareLink(link1.token);
    print('Link used: $used');
  }
  
  print('\n--- Test 5: Non-owner cannot share ---');
  final share2 = manager.shareWithUser(
    fileId: 1,
    requestingUserId: 101,
    targetUserId: 102,
    permission: Permission.view,
  );
  print('Share created: ${share2 != null}');
  
  print('\n--- Test 6: Owner access ---');
  final access3 = manager.checkAccess(fileId: 1, userId: 100);
  print('Has access: ${access3.hasAccess}');
  print('Reason: ${access3.reason}');
  
  print('\n--- Test 7: Unshared user ---');
  final access4 = manager.checkAccess(fileId: 1, userId: 999);
  print('Has access: ${access4.hasAccess}');
  print('Reason: ${access4.reason}');
}