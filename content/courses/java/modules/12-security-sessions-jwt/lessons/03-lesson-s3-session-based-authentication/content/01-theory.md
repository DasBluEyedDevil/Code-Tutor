---
type: "THEORY"
title: "How Sessions Work"
---

HTTP is stateless - each request is independent. Sessions add state:

1. USER LOGS IN:
POST /login
Body: username=john&password=secret

2. SERVER CREATES SESSION:
- Validates credentials
- Creates session object with unique ID
- Stores user info in session (server-side)
- Returns session ID in cookie

Set-Cookie: JSESSIONID=ABC123; HttpOnly; Secure

3. SUBSEQUENT REQUESTS:
GET /dashboard
Cookie: JSESSIONID=ABC123

4. SERVER LOOKS UP SESSION:
- Finds session by ID
- Retrieves stored user info
- Knows who is making request

KEY POINT: Session DATA is stored SERVER-SIDE.
The cookie only contains the session ID (a random string).
Client never sees password, user details, or permissions.