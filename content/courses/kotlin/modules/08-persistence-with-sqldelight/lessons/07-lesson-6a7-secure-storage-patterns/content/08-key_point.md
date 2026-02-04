---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Never store sensitive data in plain-text SQLite databases**—on Android, any rooted device can access database files. Use SQLCipher for encrypted databases or platform secure storage APIs for credentials.

**Encrypt database files with SQLCipher by using a different driver**—SQLCipherSqliteDriver wraps encryption around SQLite with minimal API changes. Your SQL queries remain identical.

**Balance security and usability when choosing encryption keys**—biometric-protected keys provide security without user burden; password-derived keys require users to remember passwords and handle key rotation.
