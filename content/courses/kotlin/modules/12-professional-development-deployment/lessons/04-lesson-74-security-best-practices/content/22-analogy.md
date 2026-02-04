---
type: "ANALOGY"
title: "Security as Building Locks"
---

Application security is like securing a building with multiple layers of protection.

**Input validation is the front gate guard**—checking everyone who enters, verifying they have valid ID (data format), and refusing suspicious characters (SQL injection attempts). Never assume visitors are friendly just because they claim to be.

**Authentication is your keycard system**—users must prove who they are (login credentials) before gaining access. Weak passwords are like using "1234" for the building entrance code.

**Authorization is room access control**—having a keycard (authentication) doesn't mean you can access every room. The CEO's office, server room, and janitor's closet have different access levels. In apps, this means users can only access their own data, not everyone's.

**Encryption (HTTPS) is like armored transport trucks**—when moving valuable items (user data) between buildings (client-server), use armored vehicles that protect contents even if intercepted. Plain HTTP is like moving cash in unmarked vans.

**Certificate pinning is verifying the building you're entering**—ensuring you're actually visiting the real bank, not a fake building that looks identical. Prevents man-in-the-middle attacks where imposters intercept communication.

Multi-layered security means if one layer fails (gate guard asleep), other layers (room keycards, cameras, safes) still protect assets. Never rely on single-layer security.
