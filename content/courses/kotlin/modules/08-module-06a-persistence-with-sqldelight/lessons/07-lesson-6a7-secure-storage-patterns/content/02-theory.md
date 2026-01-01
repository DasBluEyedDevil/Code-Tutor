---
type: "THEORY"
title: "What Needs Protection"
---

### Data Classification

| Category | Examples | Storage Approach |
|----------|----------|------------------|
| **Public** | App settings, cached content | Regular SQLite |
| **Internal** | User preferences, local data | Regular SQLite |
| **Sensitive** | User tokens, session data | Encrypted storage |
| **Highly Sensitive** | Passwords, API keys | Keychain/Keystore |

### What NOT to Store

**Never store in your database:**
- Plain text passwords
- Full credit card numbers
- API secret keys (use build config)
- Biometric data

**Store these server-side:**
- Payment information
- Sensitive personal data
- Private encryption keys