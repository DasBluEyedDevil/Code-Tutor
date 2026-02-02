---
type: "ANALOGY"
title: "The Fingerprint Scanner"
---

A fingerprint scanner doesn't store your actual fingerprint - it stores a mathematical representation that can verify you later. If someone steals the database, they can't recreate your fingerprint.

Password hashing works the same way. Bun.password stores a hash, not the password itself. Even if attackers get the hash, they can't reverse it to get the original password.