// lib/services/oauth_token_manager.dart

import 'dart:convert';
import 'package:flutter/foundation.dart';
import 'package:google_sign_in/google_sign_in.dart';

class OAuthTokenManager {
  final GoogleAuthService _googleAuthService;
  final AppleAuthService _appleAuthService;
  final SecureStorageService _secureStorage;
  
  // Buffer time before expiration to trigger refresh (5 minutes)
  static const _refreshBuffer = Duration(minutes: 5);
  
  OAuthTokenManager({
    required GoogleAuthService googleAuthService,
    required AppleAuthService appleAuthService,
    required SecureStorageService secureStorage,
  })  : _googleAuthService = googleAuthService,
        _appleAuthService = appleAuthService,
        _secureStorage = secureStorage;
  
  /// Checks if the current OAuth token needs refresh and refreshes if needed.
  Future<bool> ensureValidTokens() async {
    try {
      final provider = await _secureStorage.getAuthProvider();
      
      if (provider == null) {
        return false; // No auth provider stored
      }
      
      final expiration = await _secureStorage.getTokenExpiration();
      if (expiration == null) {
        return false; // No expiration stored
      }
      
      // Check if token is expired or expiring soon
      final needsRefresh = DateTime.now().add(_refreshBuffer).isAfter(expiration);
      if (!needsRefresh) {
        return true; // Tokens still valid
      }
      
      // Refresh based on provider
      switch (provider) {
        case 'google':
          return await _refreshGoogleTokens();
        case 'apple':
          return await _validateAppleToken();
        default:
          return true; // Email auth uses different refresh mechanism
      }
    } catch (e) {
      if (kDebugMode) {
        print('Token validation error: $e');
      }
      return false;
    }
  }
  
  /// Attempts to silently refresh Google tokens.
  Future<bool> _refreshGoogleTokens() async {
    try {
      final googleSignIn = GoogleSignIn(scopes: ['email', 'profile']);
      
      // Try silent sign-in first
      final account = await googleSignIn.signInSilently();
      
      if (account == null) {
        return false; // Silent sign-in failed
      }
      
      // Get new authentication tokens
      final auth = await account.authentication;
      
      if (auth.idToken == null || auth.accessToken == null) {
        return false;
      }
      
      // Update stored tokens
      await _secureStorage.saveAuthToken(auth.accessToken!);
      await _secureStorage.saveTokenExpiration(
        DateTime.now().add(const Duration(hours: 1)),
      );
      
      return true;
    } catch (e) {
      if (kDebugMode) {
        print('Google token refresh error: $e');
      }
      return false;
    }
  }
  
  /// Checks if Apple identity token is still valid by decoding JWT.
  Future<bool> _validateAppleToken() async {
    try {
      final token = await _secureStorage.getAuthToken();
      if (token == null) {
        return false;
      }
      
      // Decode JWT to check expiration
      final parts = token.split('.');
      if (parts.length != 3) {
        return false; // Invalid JWT format
      }
      
      // Decode the payload (second part)
      final payload = parts[1];
      final normalized = base64Url.normalize(payload);
      final decoded = utf8.decode(base64Url.decode(normalized));
      final claims = json.decode(decoded) as Map<String, dynamic>;
      
      // Check expiration claim
      final exp = claims['exp'] as int?;
      if (exp == null) {
        return false;
      }
      
      final expiration = DateTime.fromMillisecondsSinceEpoch(exp * 1000);
      
      // Apple tokens are typically valid for longer, but check anyway
      if (DateTime.now().add(_refreshBuffer).isAfter(expiration)) {
        // Token is expiring - Apple requires re-authentication
        return false;
      }
      
      return true;
    } catch (e) {
      if (kDebugMode) {
        print('Apple token validation error: $e');
      }
      return false;
    }
  }
}