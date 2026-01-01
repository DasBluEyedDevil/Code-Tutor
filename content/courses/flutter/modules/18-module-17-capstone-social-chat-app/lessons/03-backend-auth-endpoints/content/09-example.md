---
type: "EXAMPLE"
title: "Google Sign-In Implementation"
---


**Server-Side Google OAuth**



```dart
// server/lib/src/endpoints/auth_endpoint.dart (continued)

import 'package:http/http.dart' as http;
import 'dart:convert';

/// Sign in with Google ID token
/// 
/// The client obtains an ID token from Google Sign-In SDK,
/// then sends it to this endpoint for verification.
Future<AuthResponse> signInWithGoogle(
  Session session, {
  required String idToken,
  required String? serverAuthCode,
}) async {
  // 1. Verify the ID token with Google
  final googleUser = await _verifyGoogleToken(idToken);
  
  if (googleUser == null) {
    throw AuthException(
      code: AuthErrorCode.invalidToken,
      message: 'Invalid Google sign-in token',
    );
  }
  
  // 2. Check if user exists with this Google ID
  var userInfo = await auth.Users.findUserByIdentifier(
    session,
    'google',
    googleUser.id,
  );
  
  bool isNewUser = false;
  
  if (userInfo == null) {
    // 3a. Create new user
    isNewUser = true;
    
    // Check if email already exists
    userInfo = await auth.Users.findUserByEmail(
      session, 
      googleUser.email,
    );
    
    if (userInfo != null) {
      // Link Google to existing account
      await auth.Users.linkUserToIdentifier(
        session,
        userInfo,
        'google',
        googleUser.id,
      );
    } else {
      // Create new user with Google identity
      userInfo = await auth.Users.createUser(
        session,
        auth.UserInfoCreateRequest(
          email: googleUser.email,
          fullName: googleUser.name,
          imageUrl: googleUser.picture,
        ),
        'google',
        googleUser.id,
      );
    }
  }
  
  // 4. Create or update UserProfile
  var profile = await UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userInfo!.id!),
  );
  
  if (profile == null) {
    profile = UserProfile(
      userInfoId: userInfo!.id!,
      username: _generateUsername(googleUser.email),
      displayName: googleUser.name,
      email: googleUser.email,
      avatarUrl: googleUser.picture,
      isOnline: true,
      isVerified: true,  // Google emails are pre-verified
      isDeleted: false,
      createdAt: DateTime.now(),
    );
    
    profile = await UserProfile.db.insertRow(session, profile);
    
    // Create default settings for new user
    await _createDefaultSettings(session, profile.id!);
  } else {
    // Update avatar if changed
    if (googleUser.picture != null && 
        googleUser.picture != profile.avatarUrl) {
      profile = await UserProfile.db.updateRow(
        session,
        profile.copyWith(
          avatarUrl: googleUser.picture,
          isOnline: true,
          updatedAt: DateTime.now(),
        ),
      );
    }
  }
  
  // 5. Create session
  final authKey = await session.auth.signInUser(
    userInfo!.id!,
    'google',
  );
  
  return AuthResponse(
    success: true,
    userProfile: profile,
    authInfo: AuthInfo(
      userId: profile!.id!,
      userInfoId: userInfo.id!,
      keyId: authKey.id!,
      key: authKey.key,
    ),
    isNewUser: isNewUser,
    message: isNewUser 
        ? 'Account created successfully' 
        : 'Welcome back!',
  );
}

/// Verify Google ID token and extract user info
Future<GoogleUserInfo?> _verifyGoogleToken(String idToken) async {
  try {
    // Verify token with Google's endpoint
    final response = await http.get(
      Uri.parse(
        'https://oauth2.googleapis.com/tokeninfo?id_token=$idToken',
      ),
    );
    
    if (response.statusCode != 200) {
      return null;
    }
    
    final data = json.decode(response.body) as Map<String, dynamic>;
    
    // Verify the audience matches our client ID
    final clientId = Platform.environment['GOOGLE_CLIENT_ID'];
    if (data['aud'] != clientId) {
      print('Token audience mismatch');
      return null;
    }
    
    // Verify token hasn't expired
    final exp = int.tryParse(data['exp'] ?? '0') ?? 0;
    if (DateTime.now().millisecondsSinceEpoch ~/ 1000 > exp) {
      print('Token expired');
      return null;
    }
    
    return GoogleUserInfo(
      id: data['sub'] as String,
      email: data['email'] as String,
      name: data['name'] as String? ?? '',
      picture: data['picture'] as String?,
      emailVerified: data['email_verified'] == 'true',
    );
  } catch (e) {
    print('Error verifying Google token: $e');
    return null;
  }
}

Future<void> _createDefaultSettings(Session session, int profileId) async {
  await UserSettings.db.insertRow(session, UserSettings(
    userProfileId: profileId,
    pushNotificationsEnabled: true,
    emailNotificationsEnabled: true,
    messagePreviewsEnabled: true,
    showOnlineStatus: true,
    showLastSeen: true,
    allowDirectMessages: 'everyone',
    theme: 'system',
    language: 'en',
  ));
}

/// Helper class for Google user info
class GoogleUserInfo {
  final String id;
  final String email;
  final String name;
  final String? picture;
  final bool emailVerified;
  
  GoogleUserInfo({
    required this.id,
    required this.email,
    required this.name,
    this.picture,
    required this.emailVerified,
  });
}
```
