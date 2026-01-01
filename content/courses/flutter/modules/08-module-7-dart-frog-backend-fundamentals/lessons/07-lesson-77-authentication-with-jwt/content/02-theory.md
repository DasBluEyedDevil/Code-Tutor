---
type: "THEORY"
title: "JWT Authentication Workflow"
---


### The Complete Flow

```
┌──────────────┐                      ┌──────────────┐
│  Flutter App │                      │ Dart Frog API│
└──────────────┘                      └──────────────┘
       │                                     │
       │  1. POST /login                     │
       │  {email, password}                  │
       │ ─────────────────────────────────►  │
       │                                     │
       │  2. Verify credentials              │
       │     (check email/password)          │
       │                                     │
       │  3. Return JWT token                │
       │  {"token": "eyJhbGciOiI..."}        │
       │  ◄─────────────────────────────────│
       │                                     │
       │  4. Store token securely            │
       │     (flutter_secure_storage)        │
       │                                     │
       │  5. GET /api/profile                │
       │  Authorization: Bearer eyJhbGciOiI..│
       │ ─────────────────────────────────►  │
       │                                     │
       │  6. Validate token                  │
       │     (check signature + expiration)  │
       │                                     │
       │  7. Return protected data           │
       │  {"name": "John", ...}              │
       │  ◄─────────────────────────────────│
       │                                     │
```

### Step-by-Step Breakdown

**Step 1: Login Request**
The Flutter app sends user credentials to `/login`.

**Step 2: Credential Verification**
The server checks if email exists and password matches (hashed).

**Step 3: Token Generation**
If valid, server creates a JWT containing user ID and email, signed with a secret key.

**Step 4: Token Storage**
The Flutter app stores the token securely (NEVER in SharedPreferences - use flutter_secure_storage).

**Step 5: Authenticated Request**
For protected routes, the app sends the token in the `Authorization` header:
```
Authorization: Bearer eyJhbGciOiI...
```

**Step 6: Token Validation**
The server extracts the token, verifies the signature, and checks expiration.

**Step 7: Response**
If valid, the server processes the request using the user info from the token.

