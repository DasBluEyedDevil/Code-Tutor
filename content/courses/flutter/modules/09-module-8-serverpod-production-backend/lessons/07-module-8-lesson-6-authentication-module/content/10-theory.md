---
type: "THEORY"
title: "Google Sign-In Integration"
---

Google Sign-In allows users to authenticate using their Google account. This is especially convenient for users who already have a Google account and do not want to create a new password.

**Prerequisites:**

1. **Google Cloud Console Setup:**
   - Create a project in the Google Cloud Console
   - Enable the Google Sign-In API
   - Configure OAuth consent screen
   - Create OAuth 2.0 credentials for iOS, Android, and Web

2. **Android Configuration:**
   - Add SHA-1 fingerprint to Google Cloud Console
   - Download google-services.json and place in android/app/

3. **iOS Configuration:**
   - Add iOS Bundle ID to Google Cloud Console
   - Download GoogleService-Info.plist and add to Xcode project
   - Add URL scheme to Info.plist

**The OAuth Flow:**

1. User taps 'Sign in with Google' button
2. Google's SDK opens a sign-in prompt
3. User authenticates with Google
4. Google returns an ID token to your app
5. Your app sends this token to your Serverpod server
6. Server verifies the token with Google
7. Server creates or retrieves the user account
8. Server creates a session and returns session info

This flow is secure because the ID token is verified server-side, preventing someone from faking a Google sign-in.

