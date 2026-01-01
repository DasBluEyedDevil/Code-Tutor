---
type: "THEORY"
title: "Why Use Serverpod's Built-in Auth?"
---

Building authentication from scratch is one of the most dangerous things a developer can do. Security vulnerabilities in auth systems lead to data breaches, account takeovers, and legal liabilities. Serverpod's auth module solves this problem.

**The Risks of Rolling Your Own Auth:**

1. **Password Storage Mistakes**: Storing passwords in plain text, using weak hashing algorithms, or not using salts properly. Serverpod uses bcrypt with proper salt handling.

2. **Session Hijacking**: Poor session token generation or management. Serverpod generates cryptographically secure session tokens with proper expiration.

3. **Timing Attacks**: Comparing passwords in a way that leaks information through response time differences. Serverpod uses constant-time comparison.

4. **SQL Injection**: Improperly sanitized queries in auth code. Serverpod's ORM handles parameterization automatically.

5. **CSRF Vulnerabilities**: Missing cross-site request forgery protection. Serverpod's session management includes CSRF protection.

**What Serverpod Auth Provides:**

- Secure password hashing with bcrypt
- Cryptographically secure session tokens
- Automatic session expiration and renewal
- OAuth integration (Google, Apple, Firebase)
- Email verification workflows
- Password reset capabilities
- User profile management
- Session management across devices

Using the built-in auth module is not just easier - it is significantly more secure than most custom implementations.

