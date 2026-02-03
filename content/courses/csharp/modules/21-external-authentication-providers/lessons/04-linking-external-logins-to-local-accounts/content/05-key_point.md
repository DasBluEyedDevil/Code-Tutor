---
type: "KEY_POINT"
title: "Account Linking Strategies"
---

## Key Takeaways

- **Link external logins to local accounts by email** -- when a user signs in with Google and a local account with that email exists, link them. The user can then sign in with either method.

- **Handle the "no email" case** -- some providers do not guarantee email access (GitHub private emails). Prompt the user to provide an email and verify it before linking.

- **Allow multiple external logins per account** -- a user should be able to link Google, Microsoft, and GitHub to the same local account. Use Identity's `AddLoginAsync()` and `RemoveLoginAsync()` for management.
