---
type: "WARNING"
title: "Common Authentication Mistakes to Avoid"
---

**1. Not Checking Authentication on Every Protected Endpoint**

Every endpoint that should be protected must explicitly check `session.auth.authenticatedUserId`. Do not assume previous endpoints verified auth.

**2. Storing Sensitive Data in UserInfo**

The UserInfo object is sent to the client. Never store sensitive data there like payment information or passwords.

**3. Not Handling Token Expiration**

Sessions expire. Your app must handle this gracefully by catching auth errors and redirecting to login.

**4. Not Validating Email Before Sending Sensitive Emails**

Before sending password reset emails, do not reveal if the email exists in your system. Always return a generic message to prevent email enumeration attacks.

