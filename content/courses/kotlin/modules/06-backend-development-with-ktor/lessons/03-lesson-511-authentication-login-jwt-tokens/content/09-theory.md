---
type: "THEORY"
title: "Solution Explanation"
---


### Key Security Features

**1. Refresh Token Rotation**:
Each time you use a refresh token, it's deleted and a new one is issued. This limits the impact of stolen tokens.

**2. Hashed Storage**:
Refresh tokens are hashed before storage (like passwords). If the database is breached, tokens can't be used.

**3. Automatic Cleanup**:
Expired tokens are deleted, preventing database bloat and reducing attack surface.

**4. Per-Device Tokens**:
Users can have multiple refresh tokens (web, mobile, tablet). Logging out one device doesn't affect others.

**5. Short Access Tokens**:
Access tokens expire quickly (15 min), limiting damage if stolen. Refresh tokens handle long-term sessions.

---

