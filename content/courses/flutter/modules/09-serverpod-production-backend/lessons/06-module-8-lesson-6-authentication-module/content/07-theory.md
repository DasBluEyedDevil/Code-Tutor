---
type: "THEORY"
title: "Email and Password Authentication"
---

Email/password authentication is the most common authentication method. Users register with an email address and password, then log in using those credentials.

**The Registration Flow:**

1. User enters email and password in your Flutter app
2. Client calls the createAccount endpoint
3. Server validates the email format and password strength
4. Server hashes the password using bcrypt
5. Server creates a user record and sends a validation email
6. User clicks the link or enters the code to verify their email
7. User can now sign in

**The Login Flow:**

1. User enters email and password
2. Client calls the signIn endpoint
3. Server retrieves the user by email
4. Server compares the provided password with the stored hash
5. If valid, server creates a session and returns session info
6. Client stores the session key for future requests
7. All subsequent requests include the session key

**Password Security:**

Serverpod uses bcrypt for password hashing. Bcrypt is designed specifically for passwords and includes:
- Automatic salt generation (prevents rainbow table attacks)
- Configurable work factor (makes brute force expensive)
- Constant-time comparison (prevents timing attacks)

Never log, display, or transmit passwords in plain text. The server only ever sees the password during registration and login - it is immediately hashed and the plain text is discarded.

