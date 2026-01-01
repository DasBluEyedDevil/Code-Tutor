---
type: "ANALOGY"
title: "Understanding the Difference"
---

Imagine you're entering a corporate office building. At the front entrance, there's a security guard who checks your ID badge. This is AUTHENTICATION - the guard is verifying WHO you are. "Are you really John Smith from the Marketing department?" They check your photo, your badge number, maybe even scan it against their system. If your credentials check out, you're allowed into the building.

But getting through the front door doesn't mean you can go anywhere! Once inside, different floors and rooms have different access levels. The CEO's office? Locked. The server room? Requires a special keycard. The break room? Open to everyone. This is AUTHORIZATION - determining WHAT you're allowed to do based on who you are.

In web applications, the same two-step process applies:

STEP 1 - AUTHENTICATION ("Who are you?")
- User provides credentials (username/password, OAuth token, API key)
- System verifies the credentials are valid
- If valid, user gets an identity (like an ID badge)
- If invalid, access denied - can't even enter the building!

STEP 2 - AUTHORIZATION ("What can you do?")
- System checks the user's identity against access rules
- Different users have different permissions
- Admin can delete products, regular users can only view
- Even authenticated users may be forbidden from certain actions

The key insight: Authentication ALWAYS comes first. You can't check what someone is allowed to do until you know who they are. That's why in ASP.NET Core, UseAuthentication() middleware must come before UseAuthorization().