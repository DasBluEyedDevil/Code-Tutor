---
type: "EXAMPLE"
title: "Login Endpoint Implementation"
---


**Complete Login Flow with Session Management**



```dart
// server/lib/src/endpoints/auth_endpoint.dart (continued)

/// Login with email and password
Future<AuthResponse> loginWithEmail(
  Session session, {
  required String email,
  required String password,
}) async {
  // 1. Find user by email
  final userInfo = await auth.Users.findUserByEmail(session, email);
  
  if (userInfo == null) {
    // Use generic message to prevent user enumeration
    throw AuthException(
      code: AuthErrorCode.invalidCredentials,
      message: 'Invalid email or password',
    );
  }
  
  // 2. Verify password
  final authResult = await auth.Emails.authenticate(
    session,
    email,
    password,
  );
  
  if (authResult == null) {
    // Log failed attempt for rate limiting
    await _recordFailedLogin(session, email);
    
    throw AuthException(
      code: AuthErrorCode.invalidCredentials,
      message: 'Invalid email or password',
    );
  }
  
  // 3. Check if email is verified (optional based on requirements)
  if (!userInfo.email!.contains('@')) {
    throw AuthException(
      code: AuthErrorCode.emailNotVerified,
      message: 'Please verify your email before logging in',
    );
  }
  
  // 4. Get or create user profile
  var profile = await UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userInfo.id!),
  );
  
  if (profile == null) {
    // Create profile if it doesn't exist (legacy users)
    profile = await _createProfileForUser(session, userInfo);
  }
  
  // 5. Update online status
  await UserProfile.db.updateRow(
    session,
    profile.copyWith(
      isOnline: true,
      updatedAt: DateTime.now(),
    ),
  );
  
  // 6. Return success with auth info
  return AuthResponse(
    success: true,
    userProfile: profile,
    authInfo: AuthInfo(
      userId: userInfo.id!,
      userInfoId: userInfo.id!,
      keyId: authResult.keyId,
      key: authResult.key,
    ),
    message: 'Login successful',
  );
}

/// Logout and invalidate session
Future<bool> logout(Session session) async {
  // 1. Get current authenticated user
  final userId = await session.auth.authenticatedUserId;
  
  if (userId == null) {
    return true; // Already logged out
  }
  
  // 2. Update online status
  final profile = await UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userId),
  );
  
  if (profile != null) {
    await UserProfile.db.updateRow(
      session,
      profile.copyWith(
        isOnline: false,
        lastSeenAt: DateTime.now(),
        updatedAt: DateTime.now(),
      ),
    );
  }
  
  // 3. Invalidate the session token
  await session.auth.signOut();
  
  return true;
}

/// Refresh session token
Future<AuthResponse> refreshSession(Session session) async {
  final userId = await session.auth.authenticatedUserId;
  
  if (userId == null) {
    throw AuthException(
      code: AuthErrorCode.sessionExpired,
      message: 'Session expired. Please login again.',
    );
  }
  
  // Get user info
  final userInfo = await auth.Users.findUserByUserId(session, userId);
  
  if (userInfo == null) {
    throw AuthException(
      code: AuthErrorCode.userNotFound,
      message: 'User not found',
    );
  }
  
  // Get profile
  final profile = await UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userId),
  );
  
  return AuthResponse(
    success: true,
    userProfile: profile,
    message: 'Session refreshed',
  );
}

/// Get current authenticated user
Future<UserProfile?> getCurrentUser(Session session) async {
  final userId = await session.auth.authenticatedUserId;
  
  if (userId == null) {
    return null;
  }
  
  return UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userId),
  );
}

/// Check if user is authenticated
Future<bool> isAuthenticated(Session session) async {
  final userId = await session.auth.authenticatedUserId;
  return userId != null;
}

// Helper methods

Future<void> _recordFailedLogin(Session session, String email) async {
  // Implement rate limiting logic
  // Store failed attempts with timestamp
  // Block after X attempts
}

Future<UserProfile> _createProfileForUser(
  Session session,
  auth.UserInfo userInfo,
) async {
  final profile = UserProfile(
    userInfoId: userInfo.id!,
    username: _generateUsername(userInfo.email ?? 'user'),
    displayName: userInfo.fullName ?? 'User',
    email: userInfo.email ?? '',
    isOnline: false,
    isVerified: true,
    isDeleted: false,
    createdAt: DateTime.now(),
  );
  
  return UserProfile.db.insertRow(session, profile);
}
```
