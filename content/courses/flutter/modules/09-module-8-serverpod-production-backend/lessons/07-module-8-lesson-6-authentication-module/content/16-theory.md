---
type: "THEORY"
title: "Session Management"
---

Sessions are the foundation of authenticated user experience. Once a user signs in, a session is created that allows them to make authenticated requests without re-entering their credentials.

**How Sessions Work in Serverpod:**

1. **Session Creation:** When a user successfully authenticates, Serverpod creates a session record in the database and returns a session key to the client.

2. **Session Storage:** The client stores this session key securely (in SharedPreferences on mobile, secure storage on desktop).

3. **Authenticated Requests:** Every API call includes the session key in the headers. Serverpod validates this key and attaches the user info to the request.

4. **Session Expiration:** Sessions have an expiration time. When expired, the user must re-authenticate.

5. **Session Renewal:** Active sessions can be renewed automatically to prevent unnecessary logouts.

**SessionManager in Flutter:**

The SessionManager class handles all session-related tasks in your Flutter app. You can check if the user is signed in with `sessionManager.isSignedIn`, get the current user with `sessionManager.signedInUser`, and listen to auth state changes with `sessionManager.addListener()`.

