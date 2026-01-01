---
type: "EXAMPLE"
title: "Advanced Query Examples"
---

Here are practical examples of complex queries:



```dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class UserEndpoint extends Endpoint {
  
  /// Find users registered in the last week who are verified
  Future<List<User>> getRecentVerifiedUsers(Session session) async {
    final oneWeekAgo = DateTime.now().subtract(Duration(days: 7));
    
    return await User.db.find(
      session,
      where: (t) => 
        t.createdAt.greaterOrEqual(oneWeekAgo) &
        t.isVerified.equals(true),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }
  
  /// Find users by email domain (e.g., all gmail users)
  Future<List<User>> getUsersByEmailDomain(
    Session session,
    String domain,
  ) async {
    return await User.db.find(
      session,
      where: (t) => t.email.ilike('%@$domain'),
    );
  }
  
  /// Search with multiple optional filters
  Future<List<User>> searchUsers(
    Session session, {
    String? usernameQuery,
    bool? isVerified,
    bool? isActive,
    int limit = 50,
    int offset = 0,
  }) async {
    return await User.db.find(
      session,
      where: (t) {
        // Start with a condition that is always true
        var condition = t.id.notEquals(null);
        
        // Add filters only if provided
        if (usernameQuery != null) {
          condition = condition & t.username.ilike('%$usernameQuery%');
        }
        if (isVerified != null) {
          condition = condition & t.isVerified.equals(isVerified);
        }
        if (isActive != null) {
          condition = condition & t.isActive.equals(isActive);
        }
        
        return condition;
      },
      limit: limit,
      offset: offset,
      orderBy: (t) => t.username,
    );
  }
  
  /// Find top contributors (most posts)
  Future<List<User>> getTopContributors(
    Session session, {
    int limit = 10,
  }) async {
    return await User.db.find(
      session,
      where: (t) => 
        t.isActive.equals(true) &
        t.postCount.greaterThan(0),
      limit: limit,
      orderBy: (t) => t.postCount,
      orderDescending: true,
    );
  }
  
  /// Find users who have never logged in
  Future<List<User>> getNeverLoggedInUsers(Session session) async {
    return await User.db.find(
      session,
      where: (t) => t.lastLoginAt.equals(null),
    );
  }
  
  /// Pagination example with total count
  Future<Map<String, dynamic>> getUsersPage(
    Session session,
    int page,
    int pageSize,
  ) async {
    final offset = (page - 1) * pageSize;
    
    // Get the total count for pagination UI
    final totalCount = await User.db.count(
      session,
      where: (t) => t.isActive.equals(true),
    );
    
    // Get the page of users
    final users = await User.db.find(
      session,
      where: (t) => t.isActive.equals(true),
      limit: pageSize,
      offset: offset,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
    
    return {
      'users': users,
      'totalCount': totalCount,
      'page': page,
      'pageSize': pageSize,
      'totalPages': (totalCount / pageSize).ceil(),
    };
  }
}
```
