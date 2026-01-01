---
type: "WARNING"
title: "Account Linking Security"
---

## Critical Security Considerations for Account Linking

Account linking seems straightforward but is actually one of the most security-sensitive features in an authentication system. Improper implementation can lead to account takeover, where an attacker gains full access to a victim's account. Here are the essential security measures you must implement.

**Verify Ownership Before Linking**

The cardinal rule of account linking: NEVER allow an external login to be linked to an account unless the user has FIRST proven they own that account. This means the user must be authenticated via an EXISTING method before they can add a new one. If a user is logged in with their password and then initiates Google linking, you can be confident they own both the password and the Google account.

Never auto-link based solely on matching email addresses during initial authentication. An attacker could create a Google account with the victim's email (if Google does not verify it, or through other means) and then claim to be that user. Always require the user to prove they can access the existing account first.

**Handle Unverified Emails with Extreme Caution**

Some OAuth providers do not guarantee email verification. GitHub, for example, allows users to add any email to their account without verification. Even Google has edge cases where email_verified might be false. Before trusting an email claim for linking or merging accounts, explicitly check the email_verified claim. If the email is not verified, treat it as untrusted - either reject the linking attempt or require additional verification.

**Prevent Account Takeover via Linking**

Consider this attack scenario: An attacker knows the victim's email address. The victim has a ShopFlow account with that email. The attacker creates a malicious OAuth account (at a provider with weak email verification) using the victim's email. The attacker then uses that OAuth account to try to authenticate with ShopFlow. If ShopFlow auto-links based on email, the attacker now has access to the victim's account.

Defenses:
1. Never auto-link based on email from unverified sources
2. Require multi-factor verification for sensitive operations
3. Send email notifications when new login methods are added
4. Allow users to review and remove linked accounts

**Rate Limiting and Monitoring**

Implement rate limiting on linking attempts. An attacker trying to enumerate which emails have accounts could abuse the linking flow. Log all linking attempts (successful and failed) with relevant details for security monitoring. Alert on patterns like multiple failed link attempts to the same target email or rapid linking attempts from the same IP address.

**Unlinking Considerations**

Allow users to unlink external providers, but with safeguards:
1. Never allow unlinking the LAST authentication method - the user would be locked out
2. If the user only has external logins and unlinks one, require them to either add a password or link another provider first
3. Consider requiring re-authentication before unlinking for security
4. Send notification emails when providers are unlinked

**Token Revocation on Unlink**

When a user unlinks an external provider, consider whether you need to revoke any stored tokens. If you saved the user's access token to call provider APIs, delete it when they unlink. The user has explicitly told you they no longer want this connection - respect that completely.