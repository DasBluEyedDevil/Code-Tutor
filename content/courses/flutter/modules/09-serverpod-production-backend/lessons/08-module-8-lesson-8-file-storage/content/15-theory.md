---
type: "THEORY"
title: "File Permissions and Access Control"
---

Controlling who can access files is crucial for security and privacy. Here are the common patterns:

**1. Owner-Only Access:**
Only the user who uploaded the file can access it.

```dart
if (fileRecord.userId != currentUserId) {
  throw UnauthorizedException('Access denied');
}
```

**2. Role-Based Access:**
Admins can access all files, regular users only their own.

```dart
final isAdmin = await hasRole(session, userId, 'admin');
if (!isAdmin && fileRecord.userId != userId) {
  throw UnauthorizedException('Access denied');
}
```

**3. Shared Access:**
Files can be shared with specific users or groups.

```dart
// Check for explicit share
final share = await FileShare.db.findFirstRow(
  session,
  where: (t) => t.fileId.equals(fileId) & t.userId.equals(userId),
);

if (share != null && share.expiresAt.isAfter(DateTime.now())) {
  return true; // Access granted via share
}
```

**4. Link-Based Sharing:**
Anyone with a special link can access (like Google Docs sharing).

```dart
// Create shareable link
final shareToken = generateSecureToken();
await FileShareLink.db.insertRow(session, FileShareLink(
  fileId: fileId,
  token: shareToken,
  createdBy: userId,
  expiresAt: DateTime.now().add(Duration(days: 7)),
));

// Verify share link
Future<bool> verifyShareLink(Session session, String token) async {
  final shareLink = await FileShareLink.db.findFirstRow(
    session,
    where: (t) => t.token.equals(token),
  );
  
  if (shareLink == null) return false;
  if (shareLink.expiresAt.isBefore(DateTime.now())) return false;
  
  return true;
}
```

**5. Public Access:**
Files in the public bucket are accessible to anyone.
- Use for avatars, public media
- Do not store sensitive content
- Consider rate limiting downloads

