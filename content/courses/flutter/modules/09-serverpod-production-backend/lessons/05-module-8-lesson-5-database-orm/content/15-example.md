---
type: "EXAMPLE"
title: "Delete Operations"
---

Here is how to remove records from the database:



```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class UserEndpoint extends Endpoint {
  
  /// Delete a single user by ID
  Future<bool> deleteUser(Session session, int userId) async {
    // First find the user
    final user = await User.db.findById(session, userId);
    if (user == null) {
      return false; // Already deleted or never existed
    }
    
    // deleteRow removes the record
    final wasDeleted = await User.db.deleteRow(session, user);
    
    if (wasDeleted) {
      session.log('Deleted user: $userId');
    }
    
    return wasDeleted;
  }
  
  /// Delete users matching a condition
  Future<int> deleteInactiveUsers(Session session) async {
    // deleteWhere returns the number of deleted rows
    final deletedCount = await User.db.deleteWhere(
      session,
      where: (t) => t.isActive.equals(false),
    );
    
    session.log('Deleted $deletedCount inactive users');
    
    return deletedCount;
  }
  
  /// Soft delete pattern (recommended for most apps)
  /// Instead of actually deleting, mark as deleted
  Future<void> softDeleteUser(Session session, int userId) async {
    final user = await User.db.findById(session, userId);
    if (user == null) return;
    
    // Set isActive to false instead of deleting
    await User.db.updateRow(
      session,
      user.copyWith(isActive: false),
    );
    
    // Benefits of soft delete:
    // - Data can be recovered if deleted by mistake
    // - Maintains referential integrity with other tables
    // - Audit trail is preserved
    // - User can be reactivated later
  }
}
```
