---
type: "KEY_POINT"
title: "Defense in Depth"
---

Security isn't one thing - it's LAYERS:

LAYER 1: NETWORK
- Firewalls, HTTPS, WAF (Web Application Firewall)
- Block attacks before they reach your app

LAYER 2: APPLICATION
- Input validation, output encoding
- Spring Security, authentication/authorization

LAYER 3: DATA
- Encryption at rest and in transit
- Password hashing (bcrypt, Argon2)
- Secure database access

LAYER 4: MONITORING
- Audit logs, intrusion detection
- Alert on suspicious activity

WHY LAYERS?
If one layer fails, others still protect you.

Example: Attacker bypasses firewall
-> Application validates input (blocked)
-> Even if that fails, data is encrypted
-> Even if accessed, monitoring detects it

No single control is perfect. Layers compensate for individual failures.