---
type: "THEORY"
title: "Defense in Depth: Layered Security"
---

**Defense in Depth** means implementing multiple layers of security so that if one layer fails, others still protect your system:

**Layer 1: Network Security**
- Firewalls and network segmentation
- WAF (Web Application Firewall)
- TLS/SSL everywhere

**Layer 2: Application Security**
- Input validation
- Output encoding
- Secure session management

**Layer 3: Data Security**
- Encryption at rest
- Password hashing
- Secure key storage

**Layer 4: Monitoring & Response**
- Logging all security events
- Intrusion detection
- Incident response plan

**Think of it like a castle:**
- Moat (firewall)
- Outer wall (WAF)
- Inner wall (authentication)
- Guards (authorization)
- Vault (encryption)
- Watchmen (monitoring)