---
type: "ANALOGY"
title: "Understanding Sessions: The Hotel Key Card Analogy"
---

Think of authentication like checking into a hotel.

**Check-In (Login):**
When you arrive at a hotel, you show your ID (email/password) at the front desk. The receptionist verifies your identity and gives you a key card. This key card is your session token.

**The Key Card (Session Token):**
- It proves you are a verified guest
- It has an expiration date (checkout time)
- It only works for your specific room
- You can have multiple cards (multiple device sessions)
- If you lose it, you get a new one (session renewal)

**Using the Key Card (Making Authenticated Requests):**
Every time you want to enter your room (access protected endpoints), you swipe your card. The system checks:
1. Is this a valid card? (Valid session token)
2. Has it expired? (Session still active)
3. Does it belong to this room? (Correct user permissions)

**Check-Out (Logout):**
When you check out, the hotel deactivates your key card. Even if someone finds it later, it will not work. This is exactly what happens when you call signOut() - the session token is invalidated server-side.

**Lost Your ID? (Password Reset):**
If you forget your password, you go through a verification process (email verification) to prove you are who you claim to be, then you get a new password set up.

This analogy helps you understand why sessions have expiration times, why we invalidate sessions on logout, and why we need secure token generation.

