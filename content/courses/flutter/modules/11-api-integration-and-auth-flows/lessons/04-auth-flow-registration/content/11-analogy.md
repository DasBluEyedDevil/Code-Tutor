---
type: "ANALOGY"
title: "The Membership Card Office"
---

User registration is like signing up for a membership at a gym. You walk in, fill out a form with your name, email, and a password. The front desk checks that nobody else has already registered with that email (uniqueness validation), verifies your information looks legitimate (input validation), and then creates your member profile in their system.

But the gym does not write your password on a sticky note and tape it to your file. Instead, they create a hashed version -- like converting your password into a unique fingerprint that cannot be reversed back into the original. When you come back to log in, they hash whatever you type and compare fingerprints. If they match, you are in. If someone steals the membership files, all they get is fingerprints, not passwords.

**Registration is the one moment where trust is established.** Everything that follows -- logging in, accessing protected features, managing sessions -- depends on this initial handshake being done securely. Hash the password, validate the input, check for duplicates, and store only what you need.
