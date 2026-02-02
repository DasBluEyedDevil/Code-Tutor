---
type: "THEORY"
title: "Protecting Endpoints with session.isSignedIn"
---

Not all endpoints should be accessible to everyone. Most of your API endpoints should require authentication. Serverpod makes this easy with the session object.

**The Session Object:**

Every endpoint method receives a Session object as its first parameter. This session contains:

- Authentication information (is user signed in, who are they)
- Database connection
- Logging methods
- Access to other Serverpod features

**Checking Authentication:**

To protect an endpoint, check if the user is authenticated at the start of your method. If `session.auth.authenticatedUserId` returns null, the user is not signed in and you should throw an exception or return an error.

**Getting User Information:**

Once authenticated, you can get the user's ID with `await session.auth.authenticatedUserId` or the full user info with `await session.auth.authenticatedUser`.

By checking authentication at the start of your endpoint methods, you ensure that only authorized users can access sensitive functionality.

