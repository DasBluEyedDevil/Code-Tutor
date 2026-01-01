---
type: "WARNING"
title: "Apple Sign-In Gotchas"
---

Apple Sign-In has some unique behaviors that can cause bugs if you are not aware of them:

**1. Email Only On First Sign-In**

Apple provides the user's email ONLY on the first authentication. After that, subsequent sign-ins only include the user identifier. You MUST store the email when you first receive it.

**2. Hide My Email Creates Relay Addresses**

Users can choose to hide their email. Apple generates a private relay address like `abc123@privaterelay.appleid.com`. This address:
- Still receives emails you send to it
- Forwards to the user's real email
- Is unique per app (prevents cross-app tracking)

**3. Name Only On First Sign-In (And May Be Empty)**

Like email, the name is only provided on first sign-in. Additionally, users can decline to share their name entirely.

**4. Testing Is Difficult**

To re-test the first sign-in flow, users must:
1. Go to Settings > Apple ID > Password & Security > Apps Using Apple ID
2. Find your app and tap 'Stop Using Apple ID'
3. This resets the sign-in state

**5. App Store Requirement**

If your app offers ANY third-party sign-in (Google, Facebook, etc.), you MUST also offer Apple Sign-In. This is an App Store requirement, not optional.

