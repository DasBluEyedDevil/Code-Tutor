---
type: "KEY_POINT"
title: "Understanding AuthConfig Options"
---

The AuthConfig class has many options that control how authentication works in your application. Here are the most important ones:

**Email Configuration:**
- `sendValidationEmail`: Function called when a user needs to verify their email. You must integrate with an email service (SendGrid, Mailgun, AWS SES) in production.
- `sendPasswordResetEmail`: Function called when a user requests a password reset.
- `validationCodeLength`: Length of the validation code (default: 8 characters).

**Password Requirements:**
- `minPasswordLength`: Minimum password length (default: 8). Never set this below 8.
- `allowUnsecureRandom`: If true, uses less secure random generation. Always set to false in production.

**User Profile Settings:**
- `userCanEditUserName`: Allow users to change their username.
- `userCanEditFullName`: Allow users to change their display name.
- `userCanEditUserImage`: Allow users to upload a profile image.

**Session Settings:**
- `sessionTimeout`: How long a session lasts before requiring re-authentication.
- `enableUserImages`: Whether to support profile images.

**Callbacks:**
- `onUserCreated`: Called after a new user account is created. Use this for welcome emails, default settings, analytics.
- `onUserWillLogin`: Called before a user logs in. Return false to block the login (useful for banned users).
- `onUserUpdated`: Called when user information changes.

These callbacks give you hooks to integrate with your application's specific requirements without modifying the core auth logic.

