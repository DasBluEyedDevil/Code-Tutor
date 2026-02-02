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
  final int fileId;
  final String token;
  final Permission permission;
  final int accessCount;
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
  // In-memory storage (in production, use database)
  final Map<int, FileRecord> _files = {};
  final List<UserShare> _userShares = [];
  final List<ShareLink> _shareLinks = [];
  int _tokenCounter = 0;
  
  ShareManager() {
    // Add some test files
    _files[1] = FileRecord(id: 1, ownerId: 100, name: 'document.pdf');
    _files[2] = FileRecord(id: 2, ownerId: 100, name: 'photo.jpg');
    _files[3] = FileRecord(id: 3, ownerId: 200, name: 'spreadsheet.xlsx');
  }
  
  /// Share a file with a specific user.
  UserShare? shareWithUser({
    required int fileId,
    required int requestingUserId, // Who is creating the share
    required int targetUserId,
    required Permission permission,
    Duration? expiresIn,
  }) {
    // TODO: Verify the file exists
    // TODO: Verify requestingUserId is the owner
    // TODO: Don't allow sharing with yourself
    // TODO: Check if share already exists and update it
    // TODO: Create new share and add to list
    return null;
  }
  
  /// Create a shareable link.
  ShareLink? createShareLink({
    required int fileId,
    required int requestingUserId,
    required Permission permission,
    int? maxAccessCount,
    Duration? expiresIn,
  }) {
    // TODO: Verify the file exists
    // TODO: Verify requestingUserId is the owner
    // TODO: Generate unique token
    // TODO: Create link and add to list
    return null;
  }
  
  /// Check if a user can access a file.
  /// Can optionally provide a share token for link-based access.
  AccessResult checkAccess({
    required int fileId,
    required int userId,
    String? shareToken,
  }) {
    // TODO: Check if file exists
    // TODO: Check if user is owner (full access)
    // TODO: If token provided, check share link
    // TODO: Check user shares
    // TODO: Return appropriate result
    return AccessResult(hasAccess: false, reason: 'Not implemented');
  }
  
  /// Use a share link (increments access count).
  /// Returns true if successful, false if link is invalid/expired/exhausted.
  bool useShareLink(String token) {
    // TODO: Find the link
    // TODO: Check if expired or exhausted
    // TODO: Increment access count
    // TODO: Return success/failure
    return false;
  }
  
  /// Revoke a user share.
  bool revokeUserShare({
    required int fileId,
    required int sharedWithUserId,
    required int requestingUserId,
  }) {
    // TODO: Verify requesting user is owner
    // TODO: Find and remove the share
    return false;
  }
  
  /// Revoke a share link.
  bool revokeShareLink({
    required String token,
    required int requestingUserId,
  }) {
    // TODO: Find the link
    // TODO: Verify requesting user is file owner
    // TODO: Remove the link
    return false;
  }
  
  /// List all shares for a file (user + links).
  Map<String, dynamic> getFileShares({
    required int fileId,
    required int requestingUserId,
  }) {
    // TODO: Verify requesting user is owner
    // TODO: Return user shares and share links for this file
    return {'userShares': <UserShare>[], 'shareLinks': <ShareLink>[]};
  }
  
  // Helper to generate unique tokens
  String _generateToken() {
    _tokenCounter++;
    return 'token_${DateTime.now().millisecondsSinceEpoch}_$_tokenCounter';
  }
}

// Test your implementation
void main() {
  final manager = ShareManager();
  
  // Test 1: Share file with user
  print('--- Test 1: Share with user ---');
  final share1 = manager.shareWithUser(
    fileId: 1,
    requestingUserId: 100, // Owner
    targetUserId: 101,
    permission: Permission.download,
    expiresIn: Duration(days: 7),
  );
  print('Share created: ${share1 != null}');
  
  // Test 2: Check access for shared user
  print('\n--- Test 2: Check shared user access ---');
  final access1 = manager.checkAccess(fileId: 1, userId: 101);
  print('Has access: ${access1.hasAccess}');
  print('Permission: ${access1.grantedPermission}');
  
  // Test 3: Create share link
  print('\n--- Test 3: Create share link ---');
  final link1 = manager.createShareLink(
    fileId: 1,
    requestingUserId: 100,
    permission: Permission.view,
    maxAccessCount: 5,
  );
  print('Link created: ${link1 != null}');
  print('Token: ${link1?.token}');
  
  // Test 4: Access with share link
  print('\n--- Test 4: Access with link ---');
  if (link1 != null) {
    final access2 = manager.checkAccess(
      fileId: 1,
      userId: 999, // Random user
      shareToken: link1.token,
    );
    print('Has access: ${access2.hasAccess}');
    
    // Use the link
    final used = manager.useShareLink(link1.token);
    print('Link used: $used');
  }
  
  // Test 5: Non-owner cannot share
  print('\n--- Test 5: Non-owner cannot share ---');
  final share2 = manager.shareWithUser(
    fileId: 1,
    requestingUserId: 101, // Not owner!
    targetUserId: 102,
    permission: Permission.view,
  );
  print('Share created: ${share2 != null}'); // Should be false
  
  // Test 6: Owner has full access
  print('\n--- Test 6: Owner access ---');
  final access3 = manager.checkAccess(fileId: 1, userId: 100);
  print('Has access: ${access3.hasAccess}');
  print('Reason: ${access3.reason}');
  
  // Test 7: Unshared user has no access
  print('\n--- Test 7: Unshared user ---');
  final access4 = manager.checkAccess(fileId: 1, userId: 999);
  print('Has access: ${access4.hasAccess}');
  print('Reason: ${access4.reason}');
}