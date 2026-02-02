---
type: "EXAMPLE"
title: "Apple Sign-In Implementation"
---


**Server-Side Apple OAuth**



```dart
// server/lib/src/endpoints/auth_endpoint.dart (continued)

import 'package:jose/jose.dart';  // For JWT verification

/// Sign in with Apple identity token
/// 
/// Apple provides an identity token after user consent.
/// We verify this token and create/link the user.
Future<AuthResponse> signInWithApple(
  Session session, {
  required String identityToken,
  required String authorizationCode,
  String? givenName,
  String? familyName,
}) async {
  // 1. Verify the identity token with Apple
  final appleUser = await _verifyAppleToken(identityToken);
  
  if (appleUser == null) {
    throw AuthException(
      code: AuthErrorCode.invalidToken,
      message: 'Invalid Apple sign-in token',
    );
  }
  
  // 2. Check if user exists with this Apple ID
  var userInfo = await auth.Users.findUserByIdentifier(
    session,
    'apple',
    appleUser.sub,
  );
  
  bool isNewUser = false;
  
  // Build display name from provided names (only available on first sign-in)
  String displayName = 'Apple User';
  if (givenName != null || familyName != null) {
    displayName = [givenName, familyName]
        .where((n) => n != null && n.isNotEmpty)
        .join(' ');
  }
  
  if (userInfo == null) {
    isNewUser = true;
    
    // Check if email already exists (if Apple provides it)
    if (appleUser.email != null) {
      userInfo = await auth.Users.findUserByEmail(
        session, 
        appleUser.email!,
      );
      
      if (userInfo != null) {
        // Link Apple to existing account
        await auth.Users.linkUserToIdentifier(
          session,
          userInfo,
          'apple',
          appleUser.sub,
        );
        isNewUser = false;
      }
    }
    
    if (userInfo == null) {
      // Create new user
      userInfo = await auth.Users.createUser(
        session,
        auth.UserInfoCreateRequest(
          email: appleUser.email,
          fullName: displayName,
        ),
        'apple',
        appleUser.sub,
      );
    }
  }
  
  // 3. Create or update UserProfile
  var profile = await UserProfile.db.findFirstRow(
    session,
    where: (t) => t.userInfoId.equals(userInfo!.id!),
  );
  
  if (profile == null) {
    profile = UserProfile(
      userInfoId: userInfo!.id!,
      username: _generateUsername(appleUser.email ?? 'apple_user'),
      displayName: displayName,
      email: appleUser.email ?? '',
      isOnline: true,
      isVerified: true,  // Apple emails are verified
      isDeleted: false,
      createdAt: DateTime.now(),
    );
    
    profile = await UserProfile.db.insertRow(session, profile);
    await _createDefaultSettings(session, profile.id!);
  } else {
    // Update display name if this is a subsequent sign-in with name provided
    if (displayName != 'Apple User' && profile.displayName == 'Apple User') {
      profile = await UserProfile.db.updateRow(
        session,
        profile.copyWith(
          displayName: displayName,
          isOnline: true,
          updatedAt: DateTime.now(),
        ),
      );
    }
  }
  
  // 4. Create session
  final authKey = await session.auth.signInUser(
    userInfo!.id!,
    'apple',
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

/// Verify Apple identity token
/// 
/// Apple uses RS256-signed JWTs. We need to:
/// 1. Fetch Apple's public keys
/// 2. Verify the JWT signature
/// 3. Validate claims (issuer, audience, expiry)
Future<AppleUserInfo?> _verifyAppleToken(String identityToken) async {
  try {
    // 1. Parse the JWT without verification first to get the key ID
    final jwt = JsonWebToken.unverified(identityToken);
    final keyId = jwt.header['kid'] as String?;
    
    if (keyId == null) {
      print('No key ID in Apple token');
      return null;
    }
    
    // 2. Fetch Apple's public keys
    final keysResponse = await http.get(
      Uri.parse('https://appleid.apple.com/auth/keys'),
    );
    
    if (keysResponse.statusCode != 200) {
      print('Failed to fetch Apple public keys');
      return null;
    }
    
    final keysData = json.decode(keysResponse.body) as Map<String, dynamic>;
    final keys = keysData['keys'] as List<dynamic>;
    
    // 3. Find the matching key
    final keyData = keys.firstWhere(
      (k) => k['kid'] == keyId,
      orElse: () => null,
    );
    
    if (keyData == null) {
      print('Matching key not found');
      return null;
    }
    
    // 4. Verify the token
    final jwk = JsonWebKey.fromJson(keyData as Map<String, dynamic>);
    final keyStore = JsonWebKeyStore()..addKey(jwk);
    
    final verified = await jwt.verify(keyStore);
    if (!verified) {
      print('Token signature verification failed');
      return null;
    }
    
    // 5. Validate claims
    final claims = jwt.claims;
    
    // Check issuer
    if (claims.issuer != 'https://appleid.apple.com') {
      print('Invalid issuer');
      return null;
    }
    
    // Check audience (your app's bundle ID)
    final bundleId = Platform.environment['APPLE_BUNDLE_ID'];
    if (claims.audience?.first != bundleId) {
      print('Invalid audience');
      return null;
    }
    
    // Check expiry
    if (claims.expiry != null && 
        claims.expiry!.isBefore(DateTime.now())) {
      print('Token expired');
      return null;
    }
    
    return AppleUserInfo(
      sub: claims.subject!,
      email: claims['email'] as String?,
      emailVerified: claims['email_verified'] == true ||
                     claims['email_verified'] == 'true',
      isPrivateEmail: claims['is_private_email'] == true ||
                      claims['is_private_email'] == 'true',
    );
  } catch (e) {
    print('Error verifying Apple token: $e');
    return null;
  }
}

/// Helper class for Apple user info
class AppleUserInfo {
  final String sub;  // Apple's unique user identifier
  final String? email;
  final bool emailVerified;
  final bool isPrivateEmail;  // User chose to hide real email
  
  AppleUserInfo({
    required this.sub,
    this.email,
    required this.emailVerified,
    required this.isPrivateEmail,
  });
}
```
