---
type: "ANALOGY"
title: "The Concept"
---


### The VIP Club Analogy

Think of protected routes like different areas in a nightclub:

**Public Areas (No Authentication)**:
- Lobby: Anyone can enter (`GET /api/health`, `POST /api/auth/register`)
- No wristband needed

**Members Area (Authentication Required)**:
- Main floor: Must show wristband (`GET /api/profile`, `PUT /api/profile`)
- Bouncer checks: "Is this wristband valid? Not expired?"

**VIP Section (Role-Based Access)**:
- VIP lounge: Must show wristband AND have VIP status
- Bouncer checks: "Valid wristband? ✅ VIP status? ❌ Sorry, no entry!"
- Only admins can access (`GET /api/admin/users`, `DELETE /api/admin/users/:id`)

Your API needs the same layered access control.

### Authentication vs Authorization

| Term | Meaning | Question Answered |
|------|---------|-------------------|
| **Authentication** | Verifying identity | "Who are you?" |
| **Authorization** | Verifying permissions | "Are you allowed to do this?" |

**Example**:
- **Authentication**: Alice proves she's Alice (with JWT token)
- **Authorization**: Check if Alice has admin role before allowing her to delete users

Both are essential for secure APIs.

---

